using System;
using System.IO;
using System.Text;
using TaskIt.Dotnet.Versions.Types;

namespace TaskIt.Dotnet.Versions.Util
{
    /// <summary>
    /// Utility Class for file operations
    /// </summary>
    public static class FileUtil
    {
        /// <summary>
        /// gets filepathes for all csproj files
        /// </summary>
        /// <returns></returns>
        public static string[] GetFilepaths(string path, string filter = "*.csproj")
        {
            if (string.IsNullOrEmpty(path))
            {
                path = Environment.CurrentDirectory;
            }

            return Directory.GetFiles(path, filter, SearchOption.AllDirectories);
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

        /// <summary>
        /// Creates a Backup of the given filenamen.<br/>
        /// The filename will be extended with ".backup"
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Result CreateBackup(string filename)
        {
            Result ret = null;
            try
            {
                var target = filename + ".backup";
                File.Copy(filename, target, true);
            }
            catch (IOException e)
            {
                ret = new Result(EExitCode.INVALID_FILE, e.Message);
            }
            return ret;
        }
    }
}
