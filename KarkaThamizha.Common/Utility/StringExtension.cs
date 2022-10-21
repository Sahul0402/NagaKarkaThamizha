using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace KarkaThamizha.Common.Utility
{
    public static class StringExtension
    {
        /// <summary>
        /// Returns part of a string up to the specified number of characters, while maintaining full words
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length">Maximum characters to be returned</param>
        /// <returns>String</returns>
        public static string Chop(this string text, int length)
        {
            text = StripTagsCharArray(text);

            if (String.IsNullOrEmpty(text))
                return "விரைவில் ஆசிரியரைப் பற்றிய தகவல்கள் ஏற்றப்படும் ";

            var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words[0].Length > length)
                return words[0];
            var sb = new StringBuilder();

            foreach (var word in words)
            {
                if ((sb + word).Length > length)
                    return string.Format("{0}...", sb.ToString().TrimEnd(' '));
                sb.Append(word + " ");
            }
            return string.Format("{0}...", sb.ToString().TrimEnd(' ')) + "மேலும்...";
        }

        //string s = "I like green, red, and yellow!";
        //string s2 = s.Truncate(12, true, false);

        public static string Truncate(this string s, int length, bool atWord, bool addEllipsis)
        {
            // Return if the string is less than or equal to the truncation length
            if (s == null || s.Length <= length)
                return s;

            // Do a simple tuncation at the desired length
            string s2 = s.Substring(0, length);

            // Truncate the string at the word
            if (atWord)
            {
                // List of characters that denote the start or a new word (add to or remove more as necessary)
                List<char> alternativeCutOffs = new List<char>() { ' ', ',', '.', '?', '/', ':', ';', '\'', '\"', '\'', '-' };

                // Get the index of the last space in the truncated string
                int lastSpace = s2.LastIndexOf(' ');

                // If the last space index isn't -1 and also the next character in the original
                // string isn't contained in the alternativeCutOffs List (which means the previous
                // truncation actually truncated at the end of a word),then shorten string to the last space
                if (lastSpace != -1 && (s.Length >= length + 1 && !alternativeCutOffs.Contains(s.ToCharArray()[length])))
                    s2 = s2.Remove(lastSpace);
            }

            // Add Ellipsis if desired
            if (addEllipsis)
                s2 += "...";

            return s2;
        }

        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }


        public static string RemoveString(string value)
        {
            if (value.Length > 0)
            {
                string[] Listofwords = { "http://", "https://", "//", "/", "www.", "blogspot", "wordpress", "facebook", "twitter", "youtube.com/watch" };

                for (int i = 0; i < Listofwords.Length; i++)
                {
                    if (value.Contains(Listofwords[i]))
                    {
                        value = value.Replace(Listofwords[i], "");
                    }
                }

                return value;
            }
            return value.Trim();
        }

        public static string TitleCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(input);
        }
    }
}
