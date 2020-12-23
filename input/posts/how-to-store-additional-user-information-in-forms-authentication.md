Title: Improved ASP.NET MVC Sections
Published: 2014-01-25
Category: professional
---
_I'm taking a break from my [umbrella](/personal/a-yuppies-guide-to-walking-in-the-rain-like-a-man) series in an effort to contribute to society, instead of distracting it!_

I _really_ wanted to like [ASP.NET MVC Sections](http://weblogs.asp.net/scottgu/archive/2010/12/30/asp-net-mvc-3-layouts-and-sections-with-razor.aspx)... but I just couldn't. They just seem so rigid.

For instance, say I had a section right before the end of my body tag to stash all JavaScript files -- this is great, except I can only add code to this section one time, from one View. But what if my View renders other [partial] Views, all of which need to add JavaScript to the end of the body? Because of this drawback, sections became almost entirely useless to me.

[Orchard](http://www.orchardproject.net/) [a popular CMS engine I've talked about [before](/professional/orchard-cms-errors-when-pushing-changes-in-the-migrations-file)] has the [Script.Head()](http://orchard.codeplex.com/discussions/283849) and Script.Foot() methods, and these seemed like a big improvement. You could add code to these anywhere, and as many times as you liked. Unfortunately, while it is a step in the right direction, it still isn't quite dynamic enough. It only allows two sections: the head and the foot (and not to mention the application has to be built within Orchard). So yet again, even though these Orchard helpers are good, they aren't good enough.

### The Solution

Piggybacking of off some other [solutions](http://stackoverflow.com/a/5433722/234132), I was able to create something that gave me everything I wanted:

1. Unlimited sections
2. No restriction on the number of codes blocks that could be added to a section

The source code is hosted on [GitHub](https://github.com/dochoffiday/FlexibleSections), so I won't reproduce it here, but I will show you how easy it is to use.

In your layout View, or wherever you want the section to be rendered, include the following code:

```
<!DOCTYPE html>
<html lang="en">
<head>
    <title>@ViewBag.Title</title>
    @this.Section("header")
</head>
<body>
    @this.Section("prebody")

    @RenderBody()
           
    @this.Section("footer")
</body>
</html>
```

Then, to add content blocks to your sections:

```
@using(this.AddToSection("footer"))
{
    <script src="//js.stripe.com/v2/" type="text/javascript"></script>
}

@using(this.AddToSection("footer"))
{
    <div>add as many code blocks as you want!</div>
}
```

[That's it and that's all](http://www.youtube.com/watch?v=NeFI9aPZvKI#t=44)! There are some improvements that could be made, like a parameter to order the content added to a section, but even in its basic form, I hope it's helpful!

_Available as a [NuGet pacakge](https://www.nuget.org/packages/FlexibleSections/)!_

<a href="https://github.com/dochoffiday/FlexibleSections" class="highlight">Fork On GitHub!</a>