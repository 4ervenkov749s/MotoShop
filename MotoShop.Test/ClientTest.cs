using MotoShop.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using MotoShop.BL.Interfaces;
using MotoShop.BL.Services;
using MotoShop.Controllers;
using MotoShop.DL.Interfaces;
using MotoShop.Extensions;
using MotoShop.Models.Requests;
using MotoShop.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using Xunit;

namespace MotoShop.Test
{
    public class ClientTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly IClientService _clientService;
        private readonly ClientController _clientController;

        private IList<Client> Clients = new List<Client>()
        {
            {new Client() {Id = 1, Name = "TestName", Discount = 30}},
            {new() {Id = 2, Name = "TestName2", Discount = 25}}
        };

        public ClientTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            _mapper = mockMapper.CreateMapper();

            _clientRepository = new Mock<IClientRepository>();

            var logger = new Mock<ILogger>();

            _clientService = new ClientService(_clientRepository.Object, logger.Object);

            _clientController = new ClientController(_clientService, _mapper);
        }

        [Fact]
        public void Client_GetAll_Count_Check()
        {
            var expectedCount = 2;

            var mockedService = new Mock<IClientService>();

            mockedService.Setup(x => x.GetAll()).Returns(Clients);

            var controller = new ClientController(mockedService.Object, _mapper);

            var result = controller.GetAll();

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var clients = okObjectResult.Value as IEnumerable<Client>;
            Assert.NotNull(clients);
            Assert.Equal(expectedCount, clients.Count());
        }

        [Fact]
        public void Client_GetById_NameCheck()
        {
            var clientId = 2;
            var expectedName = "TestName2";

            _clientRepository.Setup(x => x.GetById(clientId))
                .Returns(Clients.FirstOrDefault(t => t.Id == clientId));

            var result = _clientController.GetById(clientId);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var response = okObjectResult.Value as ClientResponse;
            var client = _mapper.Map<Client>(response);

            Assert.NotNull(client);
            Assert.Equal(expectedName, client.Name);
        }

        [Fact]
        public void Client_GetById_DiscountCheck()
        {
            var clientId = 2;
            var expectedDiscount = 9;

            _clientRepository.Setup(x => x.GetById(clientId))
                .Returns(Clients.FirstOrDefault(t => t.Id == clientId));

            var result = _clientController.GetById(clientId);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var response = okObjectResult.Value as ClientResponse;
            var client = _mapper.Map<Client>(response);

            Assert.NotNull(client);
            Assert.Equal(expectedDiscount, client.Discount);
        }


        [Fact]
        public void Client_GetById_NotFound()
        {
            var clientId = 3;

            _clientRepository.Setup(x => x.GetById(clientId))
                .Returns(Clients.FirstOrDefault(t => t.Id == clientId));

            var result = _clientController.GetById(clientId);

            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.StatusCode, (int)HttpStatusCode.NotFound);

            var response = (int)notFoundObjectResult.Value;
            Assert.Equal(clientId, response);
        }

        [Fact]
        public void Client_Update_ClientName()
        {
            var clientId = 1;
            var expectedName = "Updated Client Name";

            var client = Clients.FirstOrDefault(x => x.Id == clientId);
            client.Name = expectedName;

            _clientRepository.Setup(x => x.GetById(clientId))
                .Returns(Clients.FirstOrDefault(t => t.Id == clientId));
            _clientRepository.Setup(x => x.Update(client))
                .Returns(client);

            var clientUpdateRequest = _mapper.Map<ClientUpdateRequest>(client);
            var result = _clientController.Update(clientUpdateRequest);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var val = okObjectResult.Value as Client;
            Assert.NotNull(val);
            Assert.Equal(expectedName, val.Name);

        }

        [Fact]
        public void Client_Delete_ExistingClient()
        {
            var clientId = 1;

            var client = Clients.FirstOrDefault(x => x.Id == clientId);

            _clientRepository.Setup(x => x.Delete(clientId)).Callback(() => Clients.Remove(client)).Returns(client);

            var result = _clientController.Delete(clientId);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var val = okObjectResult.Value as Client;
            Assert.NotNull(val);
            Assert.Equal(1, Clients.Count);
            Assert.Null(Clients.FirstOrDefault(x => x.Id == clientId));
        }

        [Fact]
        public void Client_Delete_NotExisting_Client()
        {
            var clientId = 3;

            var client = Clients.FirstOrDefault(x => x.Id == clientId);

            _clientRepository.Setup(x => x.Delete(clientId)).Callback(() => Clients.Remove(client)).Returns(client);

            var result = _clientController.Delete(clientId);

            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.StatusCode, (int)HttpStatusCode.NotFound);

            var response = (int)notFoundObjectResult.Value;
            Assert.Equal(clientId, response);
        }

        [Fact]
        public void Client_CreateClient()
        {
            var client = new Client()
            {
                Id = 3,
                Name = "Name 3"
            };

            _clientRepository.Setup(x => x.GetAll()).Returns(Clients);

            _clientRepository.Setup(x => x.Create(It.IsAny<Client>())).Callback(() =>
            {
                Clients.Add(client);
            }).Returns(client);

            var result = _clientController.CreateClient(_mapper.Map<ClientRequest>(client));

            var okObjectResult = result as OkObjectResult;

            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);
            Assert.NotNull(Clients.FirstOrDefault(x => x.Id == client.Id));
            Assert.Equal(3, Clients.Count);

        }

    }
}
