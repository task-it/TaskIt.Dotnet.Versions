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
        static public EExitCode WriteFile(string fileName, string[] content)
        {
            File.WriteAllLines(fileName, content, Encoding.UTF8);
            return EExitCode.SUCCESS;
        }


        /// <summary>
        /// Reads input
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        static public EExitCode ReadFile(string fileName, out string[] content)
        {
            EExitCode ret = EExitCode.SUCCESS;
            content = null;
            try
            {
                content = System.IO.File.ReadAllLines(@fileName);
            }
            catch (Exception)
            {
                ret = EExitCode.INVALID_FILE;
            }

            return ret;
        }
    }
}
