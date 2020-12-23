Title: C# Delimiter
Published: 2011-01-13
Category: professional
---
This is a handy little class that I’ve used quit a bit since it’s creation, so I thought I’d share it with the world.

### The Motivation

I grew tired of manually outputting delimiter strings, or, even worse, trying to maintain a list stored as one big string with embedded delimiters. So I created a class to do the dirty work for me. Also, feeling sorry for other characters who were sick of seeing the ',' as the default delimiter, this class has the ability to use any string value as its delimiter.

### The Code

```
using System;
using System.Collections.Generic;
using System.Text;

namespace AJ.UtiliTools
{
    public class Delimiter : List
    {
        public String Separator { get; set; }
        public String Replacement { get; set; }

        public Delimiter(String separator)
        {
            Separator = separator;
            Replacement = " ";
        }

        public Delimiter(String separator, String replacement)
        {
            Separator = separator;
            Replacement = replacement;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int r = 0; r < this.Count; r++)
            {
                sb.Append(this[r].Replace(Separator, Replacement));

                if (r < (this.Count - 1))
                    sb.Append(Separator);
            }

            return sb.ToString();
        }
    }
}
```

### In Practice

```
Delimiter d = new Delimiter("~");
       
d.Add("1");
d.Add("2");
d.Add("3");
d.Add("4");
d.Add("5");

Debug.WriteLine(d.ToString());

Delimiter d2 = new Delimiter("~", ",");

d2.Add("1");
d2.Add("2");
d2.Add("3~4");
d2.Add("5");

Debug.WriteLine(d2.ToString());
```

:: which will output the following:

<blockquote>
<p>1~2~3~4~5
1~2~3,4~5</p>
</blockquote>