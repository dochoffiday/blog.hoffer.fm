﻿Title: jQuery UI Checkbox / Radiobutton Replacement
Published: 2011-01-18
Category: professional
---
When the jQuery UI Button widget was introduced, I was amused,  but I wasn't exactly blown-away.  It's not as exciting as Autocomplete, Accordion, or some of the other widgets, but handy nonetheless.  However, when I discovered that it supported checkboxes and radiobuttons, I started to get excited, but only because I had yet to learn that the idea was better in theory than in practice.

The state difference between 'on' and 'off' are almost impossible to differentiate on some of the themes, leaving the user unable to discern whether or not his/her checkbox is 'on' or 'off'.  The problem isn't as drastic for a radiobutton, since the user can compare with the other radiobuttons in a different state, but it still wasn't practical.

But, there was hope.  Having a good foundation, I knew that with a few slight modifications, the Button widget's potential could be fully realized.

### The Solution

I actually thought of the solution by accident.  The widget has a 'primary' and 'secondary' icon options.  I mistakenly assumed these were for the 'on' and 'off' icon states.  "Why isn't there an option for an 'on' and 'off' state for icons?"  It was a head scratcher.

To me, having an 'on' and 'off' icon seems like a pretty simple feature, which is why I was surprised when it was overlooked.  It also seemed like it would be simple to implement, and it was!

I took the Button widget, modified it, and voila: 'on' and 'off' state icons. [For simplicity, I removed the secondary state icon]

### The Options

```
options: {
    disabled: null,
    text: true,
    label: null,
    icons: {
    on: 'ui-icon-circle-check',
    off: 'ui-icon-minusthick'
    }
}
```

The only real difference is the 'icons' options, which have been renamed to 'on' and 'off' instead of 'primary' and 'secondary'.

The real beauty is that because it's basically a mod of the Button widget, there isn't a need for new stylesheets - the ones already included for the Button will work just fine.

To see some examples, go the [demo page](https://dochoffiday.github.io/jquery.ui.checkbox/) or view the [project on GitHub](https://github.com/dochoffiday/jquery.ui.checkbox)!

<a href="https://github.com/dochoffiday/jquery.ui.checkbox" class="highlight">Fork On GitHub!</a>