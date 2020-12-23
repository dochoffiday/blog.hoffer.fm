Title: Introducing "ScheduleMagic" - A Simple Task Scheduler for ASP.NET
Published: 2013-12-03
Category: professional
---
Creating background tasks in .NET has always been a bit of a pain. You want to create something localized to your project solution that doesn't have to deal with scheduled tasks or other kinds of tomfoolery. Well, thankfully, those days are far behind us! All we need is a little... magic!

Leveraging the caching on the web server, ScheduleMagic allows you to schedule any task you want on any schedule you want. Simply implement the `ISchedule` and `IScheduledTask` to create schedules and tasks. Register those tasks on the application start and voila! ... MAGIC!

To see some working examples, or to download the library for your own use, head on over to the project, hosted on GitHub!

<a href="https://github.com/dochoffiday/ScheduleMagic" class="highlight">Fork On GitHub!</a>