using Microsoft.AspNetCore.Mvc;
using Moq;
using PwC.OnlineAssessment.API.Controllers;
using PwC.OnlineAssessment.API.Entities;
using PwC.OnlineAssessment.API.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PwC.OnlineAssessment.Tests
{
    public class ClientsControllerTests
    {
        private readonly Mock<IClientsRepository> mockClientRepository = new();

        [Fact]
        public async Task GetClient_When_Requested_For_NonExistent_Client_Returns_NotFound()
        {
            ////Arrange
            mockClientRepository.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync((Client)null);

            var mockClientController = new ClientsController(mockClientRepository.Object);

            ////Act
            var result = await mockClientController.Get(Guid.NewGuid());

            ////Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task GetClient_When_Requested_For_Existent_Client_Returns_ExpectedClient()
        {
            ////Arrange
            var expectedClient = GetTestClients()[0];
            mockClientRepository.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync(expectedClient);

            var mockClientController = new ClientsController(mockClientRepository.Object);

            ////Act
            var result = await mockClientController.Get(Guid.NewGuid());

            ////Assert
            var actionResult = result as OkObjectResult;
            var clientObj = actionResult.Value as Client;

            Assert.Equal(expectedClient.Id, clientObj.Id);
        }

        private List<Client> GetTestClients()
        {
            var allClients = new List<Client>();
            allClients.Add(
                new Client()
                {
                    Id = System.Guid.NewGuid(),
                    Name = "Thomas Greene",
                    Email = "greene@thomascook.nz",
                    DateJoined = System.DateTime.Now
                });
            allClients.Add(
                new Client()
                {
                    Id = System.Guid.NewGuid(),
                    Name = "Adam Parore",
                    Email = "parore@circket.nz",
                    DateJoined = System.DateTime.Now
                });
            return allClients;
        }

    }
}
