using System.Linq;
using System.Text.RegularExpressions;

namespace TaskIt.Dotnet.Versions.Util
{
    internal static class RegexUtil
    {
        /// <summary>
        /// Replaces a match in a String with the replacement
        /// </summary>
        /// <param name="input"></param>
        /// <param name="index"></param>
        /// <param name="replacement"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static string ReplaceMatch(string input, int index, string replacement, Match match)
        {
            if (string.IsNullOrEmpty(replacement))
            {
                return input;
            }
            string result = match.Value;
            Capture capture = match.Groups[index].Captures.FirstOrDefault();
            result = result.Remove(capture.Index - match.Index, capture.Length);
            result = result.Insert(capture.Index - match.Index, replacement);

            return result;
        }

        /// <summary>
        /// Gets a Tag from an xml
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tag"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static bool GetTag(string input, string tag, out Match match)
        {
            string pattern = $"<{tag}>(.*)<\\/{tag}>";
            Regex regex = new Regex(pattern);
            match = regex.Match(input);
            return match.Success;
        }




    }
}
