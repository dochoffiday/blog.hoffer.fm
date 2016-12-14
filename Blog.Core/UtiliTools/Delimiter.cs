using System;
using System.Collections.Generic;
using System.Text;

namespace AJ.UtiliTools
{
    public class Delimiter : List<String>
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