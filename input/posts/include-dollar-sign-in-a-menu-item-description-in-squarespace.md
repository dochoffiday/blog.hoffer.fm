Title: Include "$" (Dollar Sign) in a Menu Item Description in Squarespace
Published: 2015-09-08
Category: professional
---
I'm using [Squarespace](http://www.squarespace.com/) to create a fairly straightforward site for a restaurant. Squarespace uses their flavour of markdown syntax when creating menu items, and for the most part it's pretty fool-proof. I did hit a small hiccup, however, when I needed to add a dollar sign ($) to a description for a menu item.

The problem is that when Squarespace sees the dollar sign, it assumes that text is for the price, and moves it to the bottom (which messes up everything else).

Here's the incorrect markdown:

> Loch Duart Scottish Salmon Salad
> Grilled sustainable salmon served a top of mixed spring greens, tomatoes and cucumber, with your choice of dressing (substitute with sesame crusted seared tuna steak for an additional $3)
> $14

### The Fix

To fix it, all you have to do is use the ASCII equivalent (&#36;3) and it will work just fine!

> Loch Duart Scottish Salmon Salad
> Grilled sustainable salmon served a top of mixed spring greens, tomatoes and cucumber, with your choice of dressing (substitute with sesame crusted seared tuna steak for an additional &#36;3)
> $14

**Update [12/20/2018]:** This post used to have images portraying the correct and incorrect way in action, but the service they were hosted on expired the images.