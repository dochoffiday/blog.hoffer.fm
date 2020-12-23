Title: Simulate "Application_Start" in Sharepoint
Published: 2013-11-25
Category: professional
---
If you've worked within a SharePoint project, you may have noticed the absense of the "Global.asax" file. The SharePoint infrastructure doesn't include this file, so all methods within it, including the "Application_Start", are also unavailable. However, with some tricky maneuvering, we are able to simulate it.

Instead of doing a line-by-line re-creation, I'll share with you a library I've created on GitHub and outline the basic premise.

1. Create a static class with one public static method that returns "true" the first time it's called, otherwise it returns "false".
2. Every time you visit the main master page, call the method in the static class.
3. If it returns "false", do nothing.
4. If it returns "true", execute any code which you would like run when the application starts.

That's it from a logic point of view! To view the actual source code, or to just download the binaries, head on over to the GitHub project!

<a href="https://github.com/dochoffiday/Sharepoint.Startup" class="highlight">Fork On GitHub!</a>