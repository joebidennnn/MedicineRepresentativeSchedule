using MedicineRepresentativeSchedule.Controllers;
using MedicineRepresentativeSchedule.Models;
using MedicineRepresentativeSchedule.Service;
using MedicineRepresentativeSchedule.Repository;
using MedicineRepresentativeSchedule.Provider;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    class RepServiceTest
    {
        Mock<IDoctorRepository> doctorRepository;
        Mock<IRepresentativeRepository> representativeRepository;
        Mock<IMedicineStockProvider> medicineProvider;
        List<DoctorDTO> doctors;
        List<MedicineStock> stock;
        List<RepresentativeDTO> representatives;
        
        [SetUp]
        public void Setup()
        {
            doctors = new List<DoctorDTO>()
             {
               new DoctorDTO { Name = "doc1",ContactNumber="0987654321" , TreatingAilment="Orthopaedics"},
               new DoctorDTO { Name = "doc2",ContactNumber="0987654321" , TreatingAilment="General"},
               new DoctorDTO { Name = "doc3",ContactNumber="0987654321" , TreatingAilment="Gynecology"},
               new DoctorDTO { Name = "doc4",ContactNumber="0987654321" , TreatingAilment="Orthopaedics"}
            };
            representatives = new List<RepresentativeDTO>()
            {
                new RepresentativeDTO{Name= "rep1" },
                new RepresentativeDTO{Name= "rep2" },
                new RepresentativeDTO{Name= "rep3" }
            };
            stock = new List<MedicineStock>()
            { new MedicineStock
            {
                Name = "Medicine1",
                ChemicalComposition ="chemical1, chemical2",
                TargetAilment = "Orthopaedics",
                DateOfExpiry = DateTime.Parse("10-10-2021"),
                NumberOfTabletsInStock = 50
            },
            new MedicineStock
            {
                Name = "Medicine2",
                ChemicalComposition ="chemical3, chemical2",
                TargetAilment = "General",
                DateOfExpiry = DateTime.Parse("10-09-2021"),
                NumberOfTabletsInStock = 50
            },
            new MedicineStock
            {
                Name = "Medicine3",
                ChemicalComposition ="chemical1, chemical2",
                TargetAilment = "Gynecology",
                DateOfExpiry = DateTime.Parse("10-10-2021"),
                NumberOfTabletsInStock = 50
            }
            };
            representativeRepository = new Mock<IRepresentativeRepository>();
            medicineProvider = new Mock<IMedicineStockProvider>();
            doctorRepository = new Mock<IDoctorRepository>();
            doctorRepository.Setup(m => m.GetDoctorDTOList()).Returns(doctors);
            representativeRepository.Setup(m => m.GetRepresentativeList()).Returns(representatives);
            medicineProvider.Setup(m => m.GetMedicineStock()).Returns(Task.FromResult(stock));
        }

        [TestCase("2022/02/02")]
        public void CreateRepSchedule_OnProvidingValidObject_ReturnsRepScheduleList(DateTime ScheduleStartDate)
        {
            //Arrange
            IRepScheduleService repService = new RepScheduleService(doctorRepository.Object, representativeRepository.Object,medicineProvider.Object);
            //Act
            var response = repService.CreateRepSchedule(ScheduleStartDate).Result;
            //Assert
            Assert.IsNotNull(response);
        }

        [TestCase("2021/02/02")]
        public void CreateRepSchedule_onInvalidInput_ThrowsArgumentException(DateTime ScheduleStartDate)
        {
            //Arrange
            IRepScheduleService repService = new RepScheduleService(doctorRepository.Object, representativeRepository.Object, medicineProvider.Object);
            //Act
            var result =Assert.ThrowsAsync<ArgumentException>(()=>repService.CreateRepSchedule(ScheduleStartDate));
            //Assert
            Assert.That(result.Message, Is.EqualTo("InValid date"));
        }

        [TestCase("2022/02/02", true)]
        public void IsValid_OnValidDate_ReturnsTrue(DateTime ScheduleStartDate, bool ExpectedResult)
        {
            //Arrange
            IRepScheduleService repService = new RepScheduleService(doctorRepository.Object, representativeRepository.Object, medicineProvider.Object);
            //Act
            var response = repService.IsValid(ScheduleStartDate);
            //Assert
            Assert.AreEqual(response,ExpectedResult);
        }

        [TestCase("2021/2/5", false)]
        public void IsValid_OnInValidDate_ReturnsFalse(DateTime ScheduleStartDate, bool ExpectedResult)
        {
            //Arrange
            IRepScheduleService repService = new RepScheduleService(doctorRepository.Object, representativeRepository.Object, medicineProvider.Object);
            //Act
            var response = repService.IsValid(ScheduleStartDate);
            //Assert
            Assert.AreEqual(response, ExpectedResult);
        }
    }
}
