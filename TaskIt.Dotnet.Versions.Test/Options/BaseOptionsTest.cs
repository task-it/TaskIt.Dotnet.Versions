using System;
using TaskIt.Dotnet.Versions.Options;
using Xunit;

namespace TaskIt.Dotnet.Versions.Test.Options
{
    public class BaseOptionsTest
    {
        [Fact]
        public void TestConstruction()
        {
            var result = new BaseOptions();
            Assert.Equal(Environment.CurrentDirectory, result.Filename);


        }
    }
}
