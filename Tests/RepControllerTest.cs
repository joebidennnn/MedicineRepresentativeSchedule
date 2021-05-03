using MedicineRepresentativeSchedule.Controllers;
using MedicineRepresentativeSchedule.Models;
using MedicineRepresentativeSchedule.Provider;
using MedicineRepresentativeSchedule.Repository;
using MedicineRepresentativeSchedule.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class RepControllerTest
    {
        public Mock<IRepScheduleService> MockRepScheduleService;
        public IEnumerable<RepSchedule> _repSchedule;
        [SetUp]
        public void setup()
        {
            MockRepScheduleService = new Mock<IRepScheduleService>();
            _repSchedule = new List<RepSchedule>()
            {
            new RepSchedule{
                Name="Rep1",
                DocterName="Doc1",
                TreatmentAilment="general",
                Medicine="Gavisol",
                MettingSlot="1 pm to 2 pm",
                DateOfMetting=new DateTime(2022,2,4),
                DocterContactNumber="987654321"
                            }
            };
        }

        [TestCase("2022/11/12")]
        public void GetSchedule_OnValidInput_returnsRepresentativesSchedule(DateTime ScheduleStartDate)
        {
            //Arrange
            MockRepScheduleService.Setup(m => m.CreateRepSchedule(It.IsAny<DateTime>())).Returns(Task.FromResult(_repSchedule));
            RepScheduleController rep = new RepScheduleController(MockRepScheduleService.Object);
            //Act
            var response =rep.GetSchedule(ScheduleStartDate).Result as ObjectResult;
            //Assert
            Assert.AreEqual(200,response.StatusCode);
        }

        [TestCase("2022/11/12")]
        public void GetSchedule_NullRepositoryData_returnsNotFound (DateTime ScheduleStartDate)
        {
            //Arrange
            _repSchedule = null;
            MockRepScheduleService.Setup(m => m.CreateRepSchedule(It.IsAny<DateTime>())).Returns(Task.FromResult(_repSchedule));
            RepScheduleController rep = new RepScheduleController(MockRepScheduleService.Object);
            //Act
            var response = rep.GetSchedule(ScheduleStartDate).Result as ObjectResult;
            //Assert
            Assert.AreEqual(404,response.StatusCode);
        }

        [TestCase("2012/11/12")]
        public void GetSchedule_OnInvalidInput_returnsBadRequest(DateTime ScheduleStartDate)
        {
            //Arrange
            Mock<IDoctorRepository> doctorRepository = new Mock<IDoctorRepository>();
            Mock<IRepresentativeRepository> representativeRepository = new Mock<IRepresentativeRepository>();
            Mock<IMedicineStockProvider> medicineProvider = new Mock<IMedicineStockProvider>();
            IRepScheduleService repService = new RepScheduleService(doctorRepository.Object, representativeRepository.Object, medicineProvider.Object);
            RepScheduleController rep = new RepScheduleController(repService);
            //Act
            var response = rep.GetSchedule(ScheduleStartDate).Result as ObjectResult;
            //Assert
            Assert.AreEqual(400,response.StatusCode);
        }
    }
}
