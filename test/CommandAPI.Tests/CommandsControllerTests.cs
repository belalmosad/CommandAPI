using System;
using CommandAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using CommandAPI.Models;
using CommandAPI.Data;
using AutoMapper;
using System.Collections.Generic;
using CommandAPI.Profiles;

namespace CommandAPI.Tests
{
    public class CommandsControllerTests
    {
        [Fact]
        public void GetCommandItems_Returns200OK_WhenDBIsEmpty()
        {
            var mockRepo = new Mock<ICommandAPIRepo>();
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));

            var realProfile = new CommandsProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            IMapper mapper = new Mapper(config);
            var controller = new CommandsController(mockRepo.Object, mapper);
        }

        private List<Command> GetCommands(int n)
        {
            List<Command> res = new List<Command>();
            if(n > 0)
            {
                res.Add(new Command {
                    Id = 0,
                    HowTo = "How to generate a migration",
                    CommandLine = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".Net Core EF"
                });
            }
            return res;
        }
    }
}