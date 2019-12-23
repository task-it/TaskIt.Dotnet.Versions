using System;
using System.IO;
using System.Text;
using TaskIt.Dotnet.Versions.Types;

namespace TaskIt.Dotnet.Versions
{
    internal static class FileUtil
    {
        /// <summary>
        /// gets filepathes for all csproj files
        /// </summary>
        /// <returns></returns>
        public static string[] GetCsprojFilepaths(string path)
        {
            string root = Path.GetDirectoryName(path);
            return Directory.GetFiles(root, "*.csproj", SearchOption.AllDirectories);
        }

        /// <summary>
        /// Writes the output
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        static public Result WriteFile(string fileName, string[] content)
        {
            Result ret = null;
            try
            {
                File.WriteAllLines(fileName, content, Encoding.UTF8);
            }
            catch (Exception)
            {
                ret = new Result(EExitCode.INVALID_FILE, fileName);
            }
            return ret;
        }


        /// <summary>
        /// Reads input
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        static public Result ReadFile(string fileName, out string[] content)
        {
            Result ret = null;
            content = null;
            try
            {
                content = System.IO.File.ReadAllLines(@fileName);
            }
            catch (Exception)
            {
                ret = new Result(EExitCode.INVALID_FILE, fileName);
            }

            return ret;
        }
    }
}
