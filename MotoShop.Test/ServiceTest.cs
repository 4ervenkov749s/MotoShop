using AutoMapper;
using MotoShop.BL.Interfaces;
using MotoShop.BL.Services;
using MotoShop.Controllers;
using MotoShop.DL.Interfaces;
using MotoShop.Extensions;
using MotoShop.Models.DTO;
using MotoShop.Models.Responses;
using MotoShop.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace MotoShop.Test
{
    public class ServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IServiceRepository> _serviceRepository;
        private readonly IServiceService _serviceService;
        private readonly ServiceController _serviceController;

        private IList<Service> Services = new List<Service>()
        {
            new Service()
            {
                Id = 1,
                Name = "TestService",
                Price = 1
            },

            new Service()
            {
                Id=2,
                Name = "TestService2",
                Price = 2
            }
        };

        public ServiceTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            _mapper = mockMapper.CreateMapper();

            _serviceRepository = new Mock<IServiceRepository>();

            var logger = new Mock<ILogger>();

            _serviceService = new ServiceService(_serviceRepository.Object, logger.Object);

            _serviceController = new ServiceController(_serviceService, _mapper);
        }

        [Fact]
        public void Service_GetAll_Count_Check()
        {
            var expectedCount = 2;

            var mockedService = new Mock<IServiceService>();
            mockedService.Setup(x => x.GetAll()).Returns(Services);

            var controller = new ServiceController(mockedService.Object, _mapper);

            var result = controller.GetAll();

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var services = okObjectResult.Value as IEnumerable<Service>;
            Assert.NotNull(services);
            Assert.Equal(expectedCount, services.Count());
        }

        [Fact]
        public void Service_GetById_NameCheck()
        {

            var serviceId = 2;
            var expectedName = "TestService2";

            _serviceRepository.Setup(x => x.GetById(serviceId))
                .Returns(Services.FirstOrDefault(t => t.Id == serviceId));

            var result = _serviceController.GetById(serviceId);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var response = okObjectResult.Value as ServiceResponse;
            var service = _mapper.Map<Service>(response);

            Assert.NotNull(service);
            Assert.Equal(expectedName, service.Name);
        }

        [Fact]
        public void Service_GetById_NotFound()
        {

            var serviceId = 3;

            _serviceRepository.Setup(x => x.GetById(serviceId))
                .Returns(Services.FirstOrDefault(t => t.Id == serviceId));

            var result = _serviceController.GetById(serviceId);

            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.StatusCode, (int)HttpStatusCode.NotFound);

            var response = (int)notFoundObjectResult.Value;
            Assert.Equal(serviceId, response);
        }

        [Fact]
        public void Service_Update_ServiceName()
        {
            var serviceId = 1;
            var expectedName = "Updated Service Name";

            var service = Services.FirstOrDefault(x => x.Id == serviceId);
            service.Name = expectedName;

            _serviceRepository.Setup(x => x.GetById(serviceId))
                .Returns(Services.FirstOrDefault(t => t.Id == serviceId));
            _serviceRepository.Setup(x => x.Update(service))
                .Returns(service);

            var serviceUpdateRequest = _mapper.Map<ServiceUpdateRequest>(service);
            var result = _serviceController.Update(serviceUpdateRequest);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var val = okObjectResult.Value as Service;
            Assert.NotNull(val);
            Assert.Equal(expectedName, val.Name);

        }

        [Fact]
        public void Service_Delete_ExistingService()
        {
            var serviceId = 1;

            var service = Services.FirstOrDefault(x => x.Id == serviceId);

            _serviceRepository.Setup(x => x.Delete(serviceId)).Callback(() => Services.Remove(service)).Returns(service);

            var result = _serviceController.Delete(serviceId);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var val = okObjectResult.Value as Service;
            Assert.NotNull(val);
            Assert.Equal(1, Services.Count);
            Assert.Null(Services.FirstOrDefault(x => x.Id == serviceId));
        }

        [Fact]
        public void Service_Delete_NotExisting_Service()
        {
            var serviceId = 3;

            var service = Services.FirstOrDefault(x => x.Id == serviceId);

            _serviceRepository.Setup(x => x.Delete(serviceId)).Callback(() => Services.Remove(service)).Returns(service);

            var result = _serviceController.Delete(serviceId);

            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.StatusCode, (int)HttpStatusCode.NotFound);

            var response = (int)notFoundObjectResult.Value;
            Assert.Equal(serviceId, response);
        }

        [Fact]
        public void Service_CreateService()
        {
            var service = new Service()
            {
                Id = 3,
                Name = "Name 3",
                Price = 3
            };

            _serviceRepository.Setup(x => x.GetAll()).Returns(Services);

            _serviceRepository.Setup(x => x.Create(It.IsAny<Service>())).Callback(() =>
            {
                Services.Add(service);
            }).Returns(service);

            var result = _serviceController.CreateService(_mapper.Map<ServiceRequest>(service));

            var okObjectResult = result as OkObjectResult;

            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);
            Assert.NotNull(Services.FirstOrDefault(x => x.Id == service.Id));
            Assert.Equal(3, Services.Count);

        }


    }
}
