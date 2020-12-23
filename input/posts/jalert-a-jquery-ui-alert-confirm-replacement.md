Title: jAlert - A jQuery UI Alert / Confirm Replacement
Published: 2011-02-04
Category: professional
---
A themeroller ready replacement for the standard Alert and Confirm dialog boxes.

### The Motivation

The standard dialog boxes are notoriously user unfriendly.  They left developers unable to tweak their functionaly, and designers unable to tweak their design.  When all is said and done, they are pretty crude devices.

By having a jQuery UI plugin, you leverage the themability of the UI framework.  And, by making it configurable, more control is provided than just the message displayed to the user.

### The Foundation

At its core, jAlert is a jQuery UI dialog plugin, set-up to specifically mimic [and improve upon] the standard dialog boxes.  Also, while many of the options for the underlying UI dialog are pre-set, all of the UI dialog options may be overriden by the optional 'options' parameter.

```
$.jAlert([message], [highlight level], function() {
    //callback
}, [options]);

$.jConfirm([message], [highlight-level], function(result) {
    //callback - result is either 'true' of 'false'
}, [options]);
```

- [message] : The message displayed to the user
- [highlight level] : The 'highlight level' of the dialog [values: 'highlight', 'error', '']
- [options] : optional parameter - additional options for the underlying UI dialog window

To see some examples, go the [demo page](https://dochoffiday.github.io/jquery.ui.alert/) or view the [project on GitHub](https://github.com/dochoffiday/jquery.ui.alert)!

<a href="https://github.com/dochoffiday/jquery.ui.alert" class="highlight">Fork On GitHub!</a>