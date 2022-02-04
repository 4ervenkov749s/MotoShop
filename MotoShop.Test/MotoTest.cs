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
    public class MotoTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMotoRepository> _motoRepository;
        private readonly IMotoService _motoService;
        private readonly MotoController _motoController;

        private IList<Moto> Motos = new List<Moto>()
        {
            new Moto()
            {
                Id = 1,
                Brand = "TestBrand1",
                Model = "TestModel1"
            },

            new Moto()
            {
                Id=2,
                Brand = "TestBrand2",
                Model = "TestModel2"
            }
        };

        public MotoTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            _mapper = mockMapper.CreateMapper();

            _motoRepository = new Mock<IMotoRepository>();

            var logger = new Mock<ILogger>();

            _motoService = new MotoService(_motoRepository.Object, logger.Object);

            _motoController = new MotoController(_motoService, _mapper);
        }

        [Fact]
        public void Moto_GetAll_Count_Check()
        {
            var expectedCount = 2;

            var mockedService = new Mock<IMotoService>();
            mockedService.Setup(x => x.GetAll()).Returns(Motos);

            var controller = new MotoController(mockedService.Object, _mapper);

            var result = controller.GetAll();

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var motos = okObjectResult.Value as IEnumerable<Moto>;
            Assert.NotNull(motos);
            Assert.Equal(expectedCount, motos.Count());
        }

        [Fact]
        public void Moto_GetById_ModelCheck()
        {

            var motoId = 2;
            var expectedModel = "TestModel2";

            _motoRepository.Setup(x => x.GetById(motoId))
                .Returns(Motos.FirstOrDefault(t => t.Id == motoId));

            var result = _motoController.GetById(motoId);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var response = okObjectResult.Value as MotoResponse;
            var moto = _mapper.Map<Moto>(response);

            Assert.NotNull(moto);
            Assert.Equal(expectedModel, moto.Model);
        }

        [Fact]
        public void Moto_GetById_BrandCheck()
        {

            var motoId = 2;
            var expectedBrand = "TestBrand2";

            _motoRepository.Setup(x => x.GetById(motoId))
                .Returns(Motos.FirstOrDefault(t => t.Id == motoId));

            var result = _motoController.GetById(motoId);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var response = okObjectResult.Value as MotoResponse;
            var moto = _mapper.Map<Moto>(response);

            Assert.NotNull(moto);
            Assert.Equal(expectedBrand, moto.Brand);
        }

        [Fact]
        public void Moto_GetById_NotFound()
        {

            var motoId = 3;

            _motoRepository.Setup(x => x.GetById(motoId))
                .Returns(Motos.FirstOrDefault(t => t.Id == motoId));

            var result = _motoController.GetById(motoId);

            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.StatusCode, (int)HttpStatusCode.NotFound);

            var response = (int)notFoundObjectResult.Value;
            Assert.Equal(motoId, response);
        }

        [Fact]
        public void Moto_Update_MotoBrandAndModel()
        {
            var motoId = 1;
            var expectedBrand = "Updated Moto Brand";
            var expectedModel = "Updated Moto Model";

            var moto = Motos.FirstOrDefault(x => x.Id == motoId);
            moto.Brand = expectedBrand;
            moto.Model = expectedModel;

            _motoRepository.Setup(x => x.GetById(motoId))
                .Returns(Motos.FirstOrDefault(t => t.Id == motoId));
            _motoRepository.Setup(x => x.Update(moto))
                .Returns(moto);

            var motoUpdateRequest = _mapper.Map<MotoUpdateRequest>(moto);
            var result = _motoController.Update(motoUpdateRequest);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var val = okObjectResult.Value as Moto;
            Assert.NotNull(val);
            Assert.Equal(expectedBrand, val.Brand);
            Assert.Equal(expectedModel, val.Model);

        }

        [Fact]
        public void Moto_Delete_ExistingMoto()
        {
            var motoId = 1;

            var moto = Motos.FirstOrDefault(x => x.Id == motoId);

            _motoRepository.Setup(x => x.Delete(motoId)).Callback(() => Motos.Remove(moto)).Returns(moto);

            var result = _motoController.Delete(motoId);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            var val = okObjectResult.Value as Moto;
            Assert.NotNull(val);
            Assert.Equal(1, Motos.Count);
            Assert.Null(Motos.FirstOrDefault(x => x.Id == motoId));
        }

        [Fact]
        public void Moto_Delete_NotExisting_Moto()
        {
            var motoId = 3;

            var moto = Motos.FirstOrDefault(x => x.Id == motoId);

            _motoRepository.Setup(x => x.Delete(motoId)).Callback(() => Motos.Remove(moto)).Returns(moto);

            var result = _motoController.Delete(motoId);

            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.StatusCode, (int)HttpStatusCode.NotFound);

            var response = (int)notFoundObjectResult.Value;
            Assert.Equal(motoId, response);
        }

        [Fact]
        public void Moto_CreateMoto()
        {
            var moto = new Moto()
            {
                Id = 3,
                Brand = "TestBrand3",
                Model = "TestModel3"
            };

            _motoRepository.Setup(x => x.GetAll()).Returns(Motos);

            _motoRepository.Setup(x => x.Create(It.IsAny<Moto>())).Callback(() =>
            {
                Motos.Add(moto);
            }).Returns(moto);

            var result = _motoController.CreateMoto(_mapper.Map<MotoRequest>(moto));

            var okObjectResult = result as OkObjectResult;

            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);
            Assert.NotNull(Motos.FirstOrDefault(x => x.Id == moto.Id));
            Assert.Equal(3, Motos.Count);

        }
    }
}
