Title: Adding Up Rounded Percentages to Equal 100%
Published: 2017/08/17
Category: professional
---
When working with rounded percentages, it's actually not that easy to make sure the percentages add up to 100%. Take the following example:

* 40.5%
* 20.6%
* 38.9%

If you use basic rounding, you'll get 41%, 21%, and 39%... which adds up to 101%!

So, what can we do?

### The Solution

The logic behind the solution is fairly straightforward and is just a spin-off of the [largest remainder method](https://en.wikipedia.org/wiki/Largest_remainder_method):

1. Instead of rounding to the closest integer, round everything *down*
2. Take the sum of those numbers, and subtract it from 100 to determine the difference
3. Re-allocate the difference by adding *1* to the numbers with the highest decimal

Going back to our example:

1. We round the numbers to 40, 20, and 38
2. We subtract the sum of those (98) from 100, which equals **2**
3. We add *1* to the top **2** highest decimals, which gives us 40, 21, and 39... the sum of which equals 100!

That was the *easy* part. The real trick is duplicating that in code. Thankfully, it's already been done! Read the next section for a class that automatically rounds these numbers, while still preserving their original order.

### The Code

<script src="https://gist.github.com/dochoffiday/333a22e937f7503cd770ed70a429df23.js"></script>

To see the full gist, [click here](https://gist.github.com/dochoffiday/333a22e937f7503cd770ed70a429df23).