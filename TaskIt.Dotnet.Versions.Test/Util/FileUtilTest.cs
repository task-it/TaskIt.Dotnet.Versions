using System;
using System.IO;
using TaskIt.Dotnet.Versions.Util;
using Xunit;

namespace TaskIt.Dotnet.Versions.Test.Util
{
    /// <summary>
    /// Unit test for <see cref="FileUtil"/>
    /// </summary>
    public class FileUtilTest
    {
        /// <summary>
        /// unit test for <see cref="FileUtil.GetFilepaths(string, string)"/>
        /// </summary>
        /// <param name="value"></param>
        [Theory]
        [InlineData("")]
        [InlineData(@"..\..\")]
        public void TestGetFilepathsOkNone(string value)
        {
            var result = FileUtil.GetFilepaths(value);
            Assert.True(result.Length == 0);
        }

        /// <summary>
        /// unit test for <see cref="FileUtil.GetFilepaths(string, string)"/>
        /// </summary>
        /// <param name="value"></param>
        [Theory]
        [InlineData(@"..\..\..", 1)]
        [InlineData(@"..\..\..\..", 2)]
        public void TestGetFilepathsOk(string value, int expected)
        {
            var result = FileUtil.GetFilepaths(value);
            Assert.True(result.Length == expected);
        }

        /// <summary>
        /// unit test for <see cref="FileUtil.GetFilepaths(string, string)"/>
        /// </summary>
        /// <param name="value"></param>
        [Theory]
        [InlineData(@"c:\someDirectory")]
        [InlineData(@"c:\SomeDirectory\")]
        public void TestGetFilepathsNOk(string value)
        {
            //act
            Action act = () => FileUtil.GetFilepaths(value);

            //assert
            Assert.Throws<DirectoryNotFoundException>(act);


        }
    }
}
