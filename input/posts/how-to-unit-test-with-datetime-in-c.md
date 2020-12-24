Title: How To Unit Test With DateTime in C#
Published: 2020-12-24
Category: professional
---
Your code has compiled. Your unit tests are running. And then you see it. A unit test has failed. After some investigation, you realize the problem: somewhere you in the code you have a dependency on something you don't control. The cuplrit? `DateTime`.

Occasionally in programs (mostly in unit tests), you'll want some control over the `DateTime` returned by the system. There are lots of ways to do this, but I'll go over a couple of ways I _don't_ like before getting to a way I _do_ like.

### Option 1: Using Inversion of Control/Dependency Injection

The first option is to use [IoC/DI](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0#overview-of-dependency-injection), where instead of relying on `DateTime.UtcNow`, you would pass in an interface, like `IDateTimer`.

```
public interface IDateTimer
{
    DateTime GetUtcNow();
}

---

public class DateTimer : IDateTimer
{
    public DateTime GetUtcNow()
    {
        return DateTime.UtcNow;
    }
}

---

public void LogDate(IDateTimer dateTime)
{
    Console.Log(dateTime.GetUtcNow().ToString());
}
```

In the above instance of `IDateTimer`, I'm just using a wrapper around the regular `DateTime` (this could be even easier in C# 8.0 with [default interface methods](https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/default-interface-methods-versions)). In unit tests, you could mock it out to return a specific `DateTime` of your choosing.

This works well, but it potentially requires a _lot_ of injection for something that seems like it should be simpler.

### Option 2: Using a static DateTime

**Warning:** The following code is _unsafe_.

```
public class DateTimer
{
    public static DateTime? _dateTimeUtc;

    public static DateTime UtcNow { get { return _dateTimeUtc ?? DateTime.UtcNow; } }

    public static void Set(DateTime dateTimeUtc) {
        _dateTimeUtc = dateTimeUtc;
    }

    public static void Reset() {
        _dateTimeUtc = null;
    }
}

---

public void LogDate()
{
    Console.Log(DateTimer.UtcNow.ToString());
}

public void LogChristmas()
{
    DateTimer.Set(new DateTime(2020, 12, 25));

    Console.Log(DateTimer.UtcNow.ToString());

    DateTimer.Reset();
}
```

This makes changing the `DateTime` simple, but it comes with its own baggage.

Mainly:

1. You have to _manually_ remember to reset the DateTime.
2. It's not thread-safe. If you change the underlying DateTime, it will do this for _all_ threads, which may lead to unintended and unexpected issues elsewhere.

### Option 3: My Preferred Way!

What I like to do has the _simplicity_ of using a static DateTime, but with some safety measures in place to mitigate unforeseen errors and provide thread-safety.

Here's the code:

```
using System;

namespace SharpTime
{
    public static class SharpTime
    {
        [ThreadStatic]
        private static DateTime? _dateTimeUtc;

        public static DateTime UtcNow
        {
            get
            {
                if (_dateTimeUtc.HasValue)
                {
                    return _dateTimeUtc.Value;
                }

                return DateTime.UtcNow;
            }
        }

        public static IDisposable UseSpecificDateTimeUtc(DateTime dateTimeUtc)
        {
            if (_dateTimeUtc.HasValue) throw new InvalidOperationException("SharpTime is already locked");

            _dateTimeUtc = dateTimeUtc;

            return new LockedDateTimeUtc();
        }

        private class LockedDateTimeUtc : IDisposable
        {
            public void Dispose()
            {
                _dateTimeUtc = null;

                GC.SuppressFinalize(this);
            }
        }
    }
}
```

As you can see, I implemented two main checks:

1. I made `_dateTimeUtc` `[ThreadStatic]`. This prevents changes in `SharpTime` from affecting other threads.
2. The only way to _override_ the DateTime is by calling a method that returns an `IDisposable`. This means that changes to the underlying `DateTime` will be limited in scope, even if you forget to reset it.

Here's what it looks like in action:

```
public void LogDate()
{
    Console.Log(SharpTime.UtcNow.ToString());
}

public void LogChristmas()
{
    using (SharpTime.UseSpecificDateTimeUtc(new DateTime(2020, 12, 25)))
    {
        Console.Log(SharpTime.UtcNow.ToString());
    }
}
```

That's it! I'm sure there are lots of ways this could be improved, so I encourage you to check out (and fork!) the code on GitHub!

**https://github.com/dochoffiday/SharpTime**