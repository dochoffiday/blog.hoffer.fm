Title: Orchard CMS - Errors When Pushing Changes in the Migrations Files
Published: 2013-02-07
Category: professional
---
I really love the [Orchard CMS](http://www.orchardproject.net/) (a CMS engine built upon the .NET MVC Framework), but it isn't without its quirks. I've found that often times, when I try to delete a column from an existing table in the migrations file, the site inexplicably starts throwing errors. After digging through the error log, I found that the error usually is along the following lines:

>  Error when updating module: "A tenant could not be started: Default NHibernate.PropertyNotFoundException: Could not find a getter for property '{property}' in class '{className}'" 

Basically, even though the column was deleted in the migrations file, Orchard is still looking for that column.

### The Solution

This is actually a pretty easy fix. In your App_Data folder, there is a "Sites" folder, and within that there should be a folder matching the name of your site (usually "Default"). From here, simply _delete the "mappings.bin"_ file.

This will clear out the mappings and create new ones from scratch. From there, you should be good to go.