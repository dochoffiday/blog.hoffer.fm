Title: Using Regex and Yield to Recursively Find Multiple Controls
Published: 2011-06-08
Category: professional
---
This is a simple extension method that finds all child controls whose ID matches the regex pattern.

### The Code

<script src="https://gist.github.com/dochoffiday/0b04a92fc58f304de4184db39cf9df00.js"></script>

### Demonstration

```
foreach (Control control in pnlExample.FindControlsRecursive(@"[^QuestionID]"))
{
    // do something
}
```