using MedicineRepresentativeSchedule.Models;
using MedicineRepresentativeSchedule.Provider;
using MedicineRepresentativeSchedule.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineRepresentativeSchedule.Service
{
    public class RepScheduleService:IRepScheduleService
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(RepScheduleService));
        public List<DoctorDTO> doctorDTOList { get; set; }
        public List<RepresentativeDTO> representativeDTOList { get; set; }
        public List<MedicineStock> medicineStockList { get; set; }
        public IDoctorRepository _doctors { get; set; }
        public IRepresentativeRepository _representatives { get; set; }
        public IMedicineStockProvider _medicines { get; set; }
        public RepScheduleService(IDoctorRepository doctors,IRepresentativeRepository representatives,IMedicineStockProvider medicines)
        {
            _doctors = doctors;
            _representatives = representatives;
            _medicines = medicines;
        }

        public async Task<IEnumerable<RepSchedule>> CreateRepSchedule(DateTime ScheduleStartDate)
        {
            try
            {
                List<RepSchedule> repSchedules = new List<RepSchedule>();
                if (!IsValid(ScheduleStartDate))
                {
                    throw new ArgumentException("InValid date");
                }
                else
                { 
                    medicineStockList = await _medicines.GetMedicineStock();
                    doctorDTOList = _doctors.GetDoctorDTOList().ToList();
                    representativeDTOList = _representatives.GetRepresentativeList().ToList();
                    if (medicineStockList == null || doctorDTOList == null || representativeDTOList == null||
                        medicineStockList.Count == 0 || doctorDTOList.Count == 0 || representativeDTOList.Count == 0)
                    {
                        return null;
                    }
                    else
                    {

                        for (int i = 0; i < 5; i++)
                        {
                            DateTime MeetingDate;
                            if (ScheduleStartDate.DayOfWeek == DayOfWeek.Sunday)
                            {
                                ScheduleStartDate = ScheduleStartDate.AddDays(1);
                                MeetingDate = ScheduleStartDate;
                            }
                            else
                            {
                                MeetingDate = ScheduleStartDate;
                            }
                            var MedicinesByAilment = medicineStockList.Where(m => m.TargetAilment == doctorDTOList[i % doctorDTOList.Count].TreatingAilment).Select(m => m.Name);
                            string MedicineNames = string.Join(", ", MedicinesByAilment);
                            RepSchedule singleSchedule = new RepSchedule
                            {
                                Name = representativeDTOList[i % representativeDTOList.Count].Name,
                                DocterName = doctorDTOList[i % doctorDTOList.Count].Name,
                                MettingSlot = "1 PM To 2 PM",
                                TreatmentAilment = doctorDTOList[i % doctorDTOList.Count].TreatingAilment,
                                DocterContactNumber = doctorDTOList[i % doctorDTOList.Count].ContactNumber,
                                DateOfMetting = MeetingDate,
                                Medicine = MedicineNames
                            };
                            repSchedules.Add(singleSchedule);
                            ScheduleStartDate = ScheduleStartDate.AddDays(1);
                        }
                        return repSchedules;
                    }
                }
            }
            catch(ArgumentException argumentException)
            {
                _log.Error(argumentException);
                throw;
            }
            catch (Exception e)
            {
                _log.Error(e);
                throw;
            }
        }

        public bool IsValid(DateTime scheduleStartDate)
        {
            if (scheduleStartDate >= DateTime.Today)
                return true;
            return false;
        }
    }
}
