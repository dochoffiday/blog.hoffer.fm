Title: Creating a 404 Page with Pager.js
Published: 2015-09-08
Category: professional
---
[Pager.js](https://github.com/finnsson/pagerjs) is a single-page application framework for [KnockoutJS](http://knockoutjs.com/), and it has a lot of neat things out-of-the-box. But if you're using it, you may wonder how to create a 404 page, since there's no explicit method for this purpose.

It turns out, however, that this is pretty easy! Pager.js has a built-in system for [wildcards](https://pagerjs.com/demo/#!/navigation/matching_wildcards) that provides a sort of "catch-all" for routes. So, if you create a [root page](http://pagerjs.com/demo/#!/navigation/structure) (as in a page that is not nested under any other pages), just set the id to "?" (i.e. the wildcard marker).

```
<div data-bind="page: {id: '?', title: '404 - Page Not Found', scrollToTop: true, sourceCache: true }">
    <h1>404 :'(</h1>
</div>
```

If you do this, _any_ routes that don't have matching page will fall to the root wildcard page (like what happens [here](http://pagerjs.com/demo/#!/gobbledigook))! Which you can design however you want!