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
using CommandAPI.Dtos;

namespace CommandAPI.Tests
{
    public class CommandsControllerTests : IDisposable
    {
        Mock<ICommandAPIRepo> mockRepo;
        CommandsProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;

        public CommandsControllerTests()
        {
            mockRepo =  mockRepo = new Mock<ICommandAPIRepo>();
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));
            realProfile = new CommandsProfile();
            configuration = new MapperConfiguration(cfg => cfg.
            AddProfile(realProfile));
            mapper = new Mapper(configuration);

        }

        public void Dispose()
        {
                mockRepo = null;
                mapper = null;
                configuration = null;
                realProfile = null;
        }

        [Fact]
        public void GetCommandItems_Returns200OK_WhenDBIsEmpty()
        {
            var controller = new CommandsController(mockRepo.Object, mapper);
            //Act
            var result = controller.GetAllCommands();
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnOneResource_WhenDBHasOnlyOneItem()
        {
            mockRepo.Setup(o => o.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);
            //Act
            var result = controller.GetAllCommands();
            //Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandReadDto>;
            Assert.Single(commands);
        }

        [Fact]
        public void GetAllCommands_Return200Ok_WhenDBHasOneResource()
        {
            mockRepo.Setup(o => o.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);

            var res = controller.GetAllCommands();

            Assert.IsType<OkObjectResult>(res.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnCorrectType_WhenOneResourceInDB()
        {
            mockRepo.Setup(o => o.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);
            var res = controller.GetAllCommands();

            Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(res);

        }

        [Fact]
        public void GetItemByID_Return404NotFound_WhenNoItemExist()
        {
            mockRepo.Setup(o => o.GetCommandById(0)).Returns(() => null);
            var controller = new CommandsController(mockRepo.Object, mapper);
            var res = controller.GetCommandById(1);

            Assert.IsType<NotFoundResult>(res.Result);
        }

        [Fact]
        public void GetCommandByID_Return200ok_WhenCommandFound()
        {
            mockRepo.Setup(o => o.GetCommandById(0)).Returns(new Command {
                Id = 0,
                HowTo = "mock",
                Platform = "mock",
                CommandLine = "mock"
            });
            var controller = new CommandsController(mockRepo.Object, mapper);

            var res = controller.GetCommandById(0);

            Assert.IsType<OkObjectResult>(res.Result);
            
        }

         [Fact]
        public void GetCommandByID_CorrectType_WhenCommandFound()
        {
            mockRepo.Setup(o => o.GetCommandById(0)).Returns(new Command {
                Id = 0,
                HowTo = "mock",
                Platform = "mock",
                CommandLine = "mock"
            });
            var controller = new CommandsController(mockRepo.Object, mapper);

            var res = controller.GetCommandById(0);

            Assert.IsType<ActionResult<CommandReadDto>>(res);
            
        }

        [Fact]
        public void CreateNewCommand_ReturnCorrectType_WhenValidCommandIsSubmitted()
        {
            var controller = new CommandsController(mockRepo.Object, mapper);

            var res = controller.CreateComment(new CommandCreateDto {});

            Assert.IsType<ActionResult<CommandReadDto>>(res);
        }

        [Fact]
        public void CreateNewCommand_201StatusOk_WhenValidCommandIsSubmitted()
        {
            var controller = new CommandsController(mockRepo.Object, mapper);

            var res = controller.CreateComment(new CommandCreateDto {});

            Assert.IsType<CreatedAtRouteResult>(res.Result);
        }

        /*
            The same idea with other operationd:

                    * Delete
                    * Update
                    * Partial Update
        */



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