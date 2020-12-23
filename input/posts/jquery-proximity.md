Title: jQuery Proximity
Published: 2011-01-14
Category: professional
---
A simple plugin that fires events based on the _proximity_ of the cursor to an element.

Assuming this particular wheel had already been invented, I hit the intrawebs looking for a valid solution. And, after a little digging, [jQuery Approach](http://srobbin.com/blog/jquery-plugins/jquery-approach/) pulled ahead as the front-runner. Unfortunately, it just wasn’t quite granular enough – I needed something with a little more control.

So I wrote a simple plugin that, when tied to an element and given a ‘range’, fires events that provide feedback, particularly a percentage of how far the cursor is to the element when compared to the total range.

When the cursor is at distance greater than or equal to the ‘range’, a percentage of ’1' is given. If the cursor is hovering over the element, a percentage of ’0' is given. Anywhere else will give a number between ’0' and ’1' depending on the proximity of the cursor to the element.

To see some examples, go the [demo page](https://dochoffiday.github.io/jquery.proximity/) or view the [project on GitHub](https://github.com/dochoffiday/jquery.proximity)!

<a href="https://github.com/dochoffiday/jquery.proximity" class="highlight">Fork On GitHub!</a>