Title: Simulate "Application_Start" in Class Library
Published: 2013-12-30
Category: professional
---
In the past, if you needed to run startup code in a class library, you included a method call in the "Application_Start" of the Global.asax file. Thankfully, .NET 4.0 has made things easier with the [PreApplicationStartMethodAttribute](http://msdn.microsoft.com/en-us/library/system.web.preapplicationstartmethodattribute)! This helpful attribute allows us to hand-pick code to execute when the application starts without ever modifying the Global.asax file, or messier yet, creating an HttpHandler.

Let's see it in action!

### Old & Busted

First, here's a refresher of the old way. Say we have a chunk of code in our library that needs to be executed when the application starts:

```
namespace ClassLibrary
{
    public class Startup
    {
        public static void Start()
        {
            // do some awesome stuff here!
        }
    }
}
```

In the past, we had to manually add a reference to this in the "Application_Start" of our Global.asax file:

```
protected void Application_Start(object sender, EventArgs e)
{
    ClassLibrary.Startup.Start();
}
```

### New Hotness

Now that we live in the future, all we have to do is add a PreApplicationStartMethod assembly attribute (which is a part of the System.Web namespace) to our class library with parameters specifying our class and method to be executed:

```
[assembly: PreApplicationStartMethod(typeof(ClassLibrary.Startup), "Start")]
```

You can add this assembly attribute anywhere, but I find it best to either include it in the AssemblyInfo.cs file, or at the beginning of the class containing the code to be executed. Here's the Startup class in its entirety to demonstrate my meaning:

```
using System.Web;

[assembly: PreApplicationStartMethod(typeof(ClassLibrary.Startup), "Start")]

namespace ClassLibrary
{
    public class Startup
    {
        public static void Start()
        {
            // do some awesome stuff here!
        }
    }
}
```

It's as simple as that! The only downside is you need to be using the .NET Framework 4.0 or later (which is why I was not able to use this for a previous [Sharepoint solution](/professional/simulate-application-start-in-sharepoint)). But, if you are using 4.0 or later, this is a great way to keep your library code [separate](http://en.wikipedia.org/wiki/Separation_of_concerns) from your web project.

### Notes And Such

It's worth mentioning that the behaviour of the PreApplicationStartMethodAttribute is a [little different](http://stackoverflow.com/a/11800997/234132) between .NET Frameworks [4.0](http://msdn.microsoft.com/en-us/library/system.web.preapplicationstartmethodattribute(v=vs.100).aspx) and [4.5](http://msdn.microsoft.com/en-us/library/system.web.preapplicationstartmethodattribute(v=vs.110)). In 4.0, "AllowMultiple" is set to "false", so you can only include this attribute ONCE in your library - so all your code must be within one method. However, in 4.5, "AllowMultiple" is set to "true", so you may include as many startup methods as your heart desires!

### Shout-Outs

Lastly, I'd be remiss if I didn't mention [WebActivator](https://github.com/davidebbo/WebActivator). It's an excellent library that leverages the PreApplicationStartMethodAttribute but adds a little more oomph and versatility (including the ability to have startup and shutdown code!).

Now go and code!