using System.Text.RegularExpressions;
using TaskIt.Dotnet.Versions.Types;

namespace TaskIt.Dotnet.Versions.Util
{
    internal static class ContentUtil
    {




        /// <summary>
        /// sets all occurances of the tag content to newValue
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="newValue"></param>
        /// <param name="source"></param>
        public static void ReplaceTag(string[] source, string tag, Modifier modifier, bool isSemanticVersion)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (RegexUtil.GetTag(source[i], tag, out var match))
                {
                    var newVersion = new ProjectVersion(match.Groups[1].Value);
                    modifier.Overwrite(newVersion, isSemanticVersion);
                    source[i] = Regex.Replace(source[i], match.Groups[1].Value, newVersion.FullVersion);
                }
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="newValue"></param>
        /// <param name="source"></param>
        public static void ModifyTag(string[] source, string tag, Modifier modifier, bool isSemanticVersion)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (RegexUtil.GetTag(source[i], tag, out var match))
                {
                    var newVersion = new ProjectVersion(match.Groups[1].Value);
                    modifier.Modify(newVersion, isSemanticVersion);
                    source[i] = Regex.Replace(source[i], match.Groups[1].Value, newVersion.FullVersion);
                }
            }
        }


    }
}
