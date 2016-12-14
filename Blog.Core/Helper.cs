using System;

namespace BC.Core
{
    public class Helper
    {
        public static String CleanTags(String tags)
        {
            tags = AJ.UtiliTools.Helper.UrlDecode(tags).ToLower().Replace(", ", ",");

            tags = tags.Trim();

            if (tags.EndsWith(",")) { tags = tags.Remove(tags.LastIndexOf(","), 1); }

            tags = tags.Trim();

            return tags;
        }
    }
}
