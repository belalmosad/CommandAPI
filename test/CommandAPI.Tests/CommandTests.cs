using System;
using CommandAPI.Models;
using Xunit;

namespace CommandAPI.Tests
{
    public class CommandTests : IDisposable
    {
        Command testCommand;
        public CommandTests()
        {
            testCommand = new Command {
                HowTo = "Do something awesome",
                Platform = "xunint",
                CommandLine = "dotnet test"
            };
        }

        public void Dispose() => testCommand = null;

        [Fact]
        public void CanChangeHowTo()
        {
            testCommand.HowTo = "perform unit test";
            Assert.Equal("perform unit test", testCommand.HowTo);
        }

        [Fact]

        public void CanChangePlatform()
        {
            testCommand.Platform = "xunit";
            Assert.Equal("xunit", testCommand.Platform);
        }

        [Fact]
        public void CanChangeCommandLine()
        {
            testCommand.HowTo = "dotnet test";
            Assert.Equal("dotnet test", testCommand.HowTo);
        }
    }
}