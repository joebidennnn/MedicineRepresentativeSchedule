using MedicineRepresentativeSchedule.Data_Access_Layer;
using MedicineRepresentativeSchedule.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineRepresentativeSchedule.Repository
{
    public class DoctorRepository:IDoctorRepository
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(DoctorRepository));
        public RepScheduleEntity _context { get; set; }
        public DoctorRepository(RepScheduleEntity context)
        {
            _context = context;
        }
        public IEnumerable<DoctorDTO> GetDoctorDTOList()
        {
            try
            {
                _log.Info("trying to Retrieving data from database");
                IEnumerable<DoctorDTO> doctorDTOLIst= _context.Doctors.Select(D => new DoctorDTO { Name = D.Name, TreatingAilment = D.TreatingAilment, ContactNumber = D.ContactNumber });
                if(doctorDTOLIst==null)
                {
                    _log.Error("In Doctor Repository Data could not be retrived, returned null");
                    return doctorDTOLIst;
                }
                else
                {
                    _log.Info("Doctor List returned");
                    return doctorDTOLIst;
                }
            }
            catch(Exception exception)
            {
                _log.Error(exception);
                throw;
            }
        }
    }
}
