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
    public class PartTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPartRepository> _partRepository;
        private readonly IPartService _partService;
        private readonly PartController _partController;

        private IList<Part> Parts = new List<Part>()
        {
            new Part()
            {
                Id = 1,
                Name = "TestPart",
                PartNumber = "Test",
                Price = 1
            },

            new Part()
            {
                Id=2,
                Name = "TestPart2",
                PartNumber = "Test2",
                Price = 2
            }
        };

        public PartTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            _mapper = mockMapper.CreateMapper();

            _partRepository = new Mock<IPartRepository>();

            var logger = new Mock<ILogger>();

            _partService = new PartService(_partRepository.Object, logger.Object);

            _partController = new PartController(_partService, _mapper);
        }

        [Fact]
        public void Part_GetAll_Count_Check()
        {
            var expectedCount = 2;

            var mockedService = new Mock<IPartService>();
            mockedService.Setup(x => x.GetAll()).Returns(Parts);

            var controller = new PartController(mockedService.Object, _mapper);

            var result = controller.GetAll();

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var parts = okObjectResult.Value as IEnumerable<Part>;
            Assert.NotNull(parts);
            Assert.Equal(expectedCount, parts.Count());
        }

        [Fact]
        public void Part_GetById_NameCheck()
        {

            var partId = 2;
            var expectedName = "TestPart2";

            _partRepository.Setup(x => x.GetById(partId))
                .Returns(Parts.FirstOrDefault(t => t.Id == partId));

            var result = _partController.GetById(partId);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var response = okObjectResult.Value as PartResponse;
            var part = _mapper.Map<Part>(response);

            Assert.NotNull(part);
            Assert.Equal(expectedName, part.Name);
        }

        [Fact]
        public void Part_GetById_PartNrCheck()
        {

            var partId = 2;
            var expectedPartNr = "Test2";

            _partRepository.Setup(x => x.GetById(partId))
                .Returns(Parts.FirstOrDefault(t => t.Id == partId));

            var result = _partController.GetById(partId);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var response = okObjectResult.Value as PartResponse;
            var part = _mapper.Map<Part>(response);

            Assert.NotNull(part);
            Assert.Equal(expectedPartNr, part.PartNumber);
        }

        [Fact]
        public void Part_GetById_NotFound()
        {

            var partId = 3;

            _partRepository.Setup(x => x.GetById(partId))
                .Returns(Parts.FirstOrDefault(t => t.Id == partId));

            var result = _partController.GetById(partId);

            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.StatusCode, (int)HttpStatusCode.NotFound);

            var response = (int)notFoundObjectResult.Value;
            Assert.Equal(partId, response);
        }

        [Fact]
        public void Part_Update_PartName()
        {
            var partId = 1;
            var expectedName = "Updated Part Name";

            var part = Parts.FirstOrDefault(x => x.Id == partId);
            part.Name = expectedName;

            _partRepository.Setup(x => x.GetById(partId))
                .Returns(Parts.FirstOrDefault(t => t.Id == partId));
            _partRepository.Setup(x => x.Update(part))
                .Returns(part);

            var partUpdateRequest = _mapper.Map<PartUpdateRequest>(part);
            var result = _partController.Update(partUpdateRequest);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var val = okObjectResult.Value as Part;
            Assert.NotNull(val);
            Assert.Equal(expectedName, val.Name);

        }

        [Fact]
        public void Part_Delete_ExistingPart()
        {
            var partId = 1;

            var part = Parts.FirstOrDefault(x => x.Id == partId);

            _partRepository.Setup(x => x.Delete(partId)).Callback(() => Parts.Remove(part)).Returns(part);

            var result = _partController.Delete(partId);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var val = okObjectResult.Value as Part;
            Assert.NotNull(val);
            Assert.Equal(1, Parts.Count);
            Assert.Null(Parts.FirstOrDefault(x => x.Id == partId));
        }

        [Fact]
        public void Part_Delete_NotExisting_Part()
        {
            var partId = 3;

            var part = Parts.FirstOrDefault(x => x.Id == partId);

            _partRepository.Setup(x => x.Delete(partId)).Callback(() => Parts.Remove(part)).Returns(part);

            var result = _partController.Delete(partId);

            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.StatusCode, (int)HttpStatusCode.NotFound);

            var response = (int)notFoundObjectResult.Value;
            Assert.Equal(partId, response);
        }

        [Fact]
        public void Part_CreatePart()
        {
            var part = new Part()
            {
                Id = 3,
                Name = "Name 3"
            };

            _partRepository.Setup(x => x.GetAll()).Returns(Parts);

            _partRepository.Setup(x => x.Create(It.IsAny<Part>())).Callback(() =>
            {
                Parts.Add(part);
            }).Returns(part);

            var result = _partController.CreatePart(_mapper.Map<PartRequest>(part));

            var okObjectResult = result as OkObjectResult;

            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);
            Assert.NotNull(Parts.FirstOrDefault(x => x.Id == part.Id));
            Assert.Equal(3, Parts.Count);

        }


    }
}
