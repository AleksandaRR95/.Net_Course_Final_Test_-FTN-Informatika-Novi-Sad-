using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceApp.Controllers;
using ServiceApp.Models;
using ServiceApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceAppTest.Controllers
{
    public class PaketiControllerTest
    {

        [Fact]
        public void GetPaket_ValidId_ReturnsObject()
        {
            // Arrange
            Paket paket = new Paket()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1,
                CenaPostarine = 520,
                KurirId = 3,
                Kurir = new Kurir { Id = 3, Ime = "Viktor Pavlov", GodinaRodjenja = 1987 }
            };

            PaketDTO paketDTO = new PaketDTO()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1,
                CenaPostarine = 520,
                KurirId = 3,
                KurirIme = "Viktor Pavlov"

            };

            var mockRepository = new Mock<IPaketRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(paket);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.GetPaket(1) as Microsoft.AspNetCore.Mvc.OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            PaketDTO dtoResult = (PaketDTO)actionResult.Value;
            Assert.Equal(paket.Id, dtoResult.Id);
            Assert.Equal(paket.Posiljalac, dtoResult.Posiljalac);
            Assert.Equal(paket.Primalac, dtoResult.Primalac);
            Assert.Equal(paket.Tezina, dtoResult.Tezina);
            Assert.Equal(paket.Kurir.Ime, dtoResult.KurirIme);
            Assert.Equal(paket.Kurir.Id, dtoResult.KurirId);
        }
        [Fact]
        public void PutPaket_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            Paket paket = new Paket()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1,
                CenaPostarine = 520,
                KurirId = 3,
                Kurir = new Kurir { Id = 3, Ime = "Viktor Pavlov", GodinaRodjenja = 1987 }
            };

            var mockRepository = new Mock<IPaketRepository>();

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.PutPaket(24, paket) as BadRequestResult;

            // Assert
            Assert.NotNull(actionResult);
        }
        [Fact]
        public void DeletePaket_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IPaketRepository>();

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.DeletePaket(12) as NotFoundResult;

            // Assert
            Assert.NotNull(actionResult);
        }
        [Fact]
        public void PostPaketPretraga_ReturnsCollection()
        {
            List<Paket> paketi = new List<Paket>() {
                 new Paket()
                 {
                     Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1,
                CenaPostarine = 520,
                KurirId = 3,
                Kurir = new Kurir { Id = 3, Ime = "Viktor Pavlov", GodinaRodjenja = 1987 }
                 },
                  new Paket()
                 {
                   Id = 2, Posiljalac = "Galerija doo", Primalac = "Salon Centar", Tezina = 0.9, CenaPostarine = 340, KurirId = 1,
                   Kurir = new Kurir{Id = 1, Ime = "Marko Petrov", GodinaRodjenja = 1987}
                 },
                   new Paket()
                 {
                    Id = 2, Posiljalac = "Galerija doo", Primalac = "Salon Centar", Tezina = 0.9, CenaPostarine = 340, KurirId = 1,
                    Kurir = new Kurir{ Id = 1, Ime = "Marko Petrov", GodinaRodjenja = 1987}
                 }
            };



            List<PaketDTO> zaposleniDto = new List<PaketDTO>() {
                 new PaketDTO()
                 {
                    Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1,
                CenaPostarine = 520,
                KurirId = 3,
                KurirIme = "Viktor Pavlov"
                 },
                  new PaketDTO()
                 {
                   Id = 2, Posiljalac = "Galerija doo", Primalac = "Salon Centar", Tezina = 0.9, CenaPostarine = 340, KurirId = 1,
                   KurirIme = "Marko Petrov"
                 },
                   new PaketDTO()
                 {
                    Id = 2, Posiljalac = "Galerija doo", Primalac = "Salon Centar", Tezina = 0.9, CenaPostarine = 340, KurirId = 1,
                    KurirIme = "Marko Petrov"
                 }
            };

            TezinaPretraga searchDTO = new TezinaPretraga()
            {
                Najmanje = 1,
                Najvise = 5,
            };
            var mockRepository = new Mock<IPaketRepository>();
            mockRepository.Setup(x => x.PretragaTezina(searchDTO.Najmanje, searchDTO.Najvise)).Returns(paketi.AsQueryable());

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            var actionResult = controller.Pretraga(searchDTO) as OkObjectResult;

            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            List<PaketDTO> dtoResult = (List<PaketDTO>)actionResult.Value;
            Assert.Equal(zaposleniDto[0], dtoResult[0]);
            Assert.Equal(zaposleniDto[1], dtoResult[1]);
            Assert.Equal(zaposleniDto[2], dtoResult[2]);


        }
    }
}
