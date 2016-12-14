using System;
using System.Text.RegularExpressions;

namespace AJ.UtiliTools
{
    public class UtiliDation
    {
        public static int MAX = 2147483647;
        public static int SMALL = 50;
        public static int LARGE = 512;
        public static int AVG = 256;

        public static String REGEX_EMAIL = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

        public static void ValidateMaximumFloat(ref String originalMessage, String value, float maximum)
        {
            ValidateMaximumFloat(ref originalMessage, value, maximum, "This field exceeds its maximum value!");
        }

        public static void ValidateMaximumFloat(ref String originalMessage, String value, float maximum, String message)
        {
            if (originalMessage.IsNullOrEmpty())
            {
                if (!value.IsNullOrEmpty())
                {
                    double result = 0;

                    if (Double.TryParse(value, out result))
                    {
                        if (result > maximum)
                        {
                            originalMessage = message;
                        }
                    }
                    else
                    {
                        originalMessage = message;
                    }
                }
            }
        }

        public static void ValidateMinimumFloat(ref String originalMessage, String value, float minimum)
        {
            ValidateMinimumFloat(ref originalMessage, value, minimum, "This field exceeds its maximum value!");
        }

        public static void ValidateMinimumFloat(ref String originalMessage, String value, float minimum, String message)
        {
            if (originalMessage.IsNullOrEmpty())
            {
                if (!value.IsNullOrEmpty())
                {
                    double result = 0;

                    if (Double.TryParse(value, out result))
                    {
                        if (result < minimum)
                        {
                            originalMessage = message;
                        }
                    }
                    else
                    {
                        originalMessage = message;
                    }
                }
            }
        }

        public static void ValidateInteger(ref String originalMessage, String value)
        {
            ValidateInteger(ref originalMessage, value, "This field is not a valid integer!");
        }

        public static void ValidateInteger(ref String originalMessage, String value, String message)
        {
            int code = 0;
            originalMessage = Helper.IIF<String>(!Int32.TryParse(value, out code) && !value.IsNullOrEmpty() && originalMessage.IsNullOrEmpty(), 
                                                message, 
                                                originalMessage);
        }

        public static void ValidateDate(ref String originalMessage, String value)
        {
            ValidateDate(ref originalMessage, value, "This field is not a valid date!");
        }

        public static void ValidateDate(ref String originalMessage, String value, String message)
        {
            DateTime date = DateTime.Now;
            originalMessage = Helper.IIF<String>(!DateTime.TryParse(value, out date) && !value.IsNullOrEmpty() && originalMessage.IsNullOrEmpty(),
                                                message,
                                                originalMessage);
        }

        public static void ValidateRequired(ref String originalMessage, String value)
        {
            ValidateRequired(ref originalMessage, value, 0, "This field is required!");
        }

        public static void ValidateRequired(ref String originalMessage, String value, String message)
        {
            ValidateRequired(ref originalMessage, value, 1, message);
        }

        public static void ValidateRequired(ref String originalMessage, String value, int minLength, String message)
        {
            if (minLength >= 0)
            {
                if (value == null)
                {
                    originalMessage = message;
                }
                else
                {
                    if (value.Length < minLength)
                    {
                        originalMessage = message;
                    }
                }
            }
        }

        public static void ValidateLength(ref String originalMessage, String value, int maxLength)
        {
            ValidateLength(ref originalMessage, value, maxLength, "This field exceeds its maximum length!");
        }

        public static void ValidateLength(ref String originalMessage, String value, int maxLength, String message)
        {
            if (originalMessage.IsNullOrEmpty())
            {
                if (!value.IsNullOrEmpty())
                {
                    if (value.Length > maxLength)
                        originalMessage = message;
                }
            }
        }

        public static void ValidateRegex(ref String originalMessage, String value, String regularExpression, String message)
        {
            if (originalMessage.IsNullOrEmpty() && !value.IsNullOrEmpty())
            {
                if (!Regex.IsMatch(value, regularExpression))
                {
                    originalMessage = message;
                }
            }
        }
    }
}