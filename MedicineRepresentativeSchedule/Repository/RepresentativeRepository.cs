using MedicineRepresentativeSchedule.Data_Access_Layer;
using MedicineRepresentativeSchedule.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineRepresentativeSchedule.Repository
{
    public class RepresentativeRepository:IRepresentativeRepository
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(RepresentativeRepository));
        public RepScheduleEntity _context { get; set; }
        public RepresentativeRepository(RepScheduleEntity context)
        {
            _context = context;
        }
        public IEnumerable<RepresentativeDTO> GetRepresentativeList()
        {
            try
            {
                _log.Info("trying to retriving data from database");
                IEnumerable<RepresentativeDTO> representativesDTO = _context.Representatives.Select(R => new RepresentativeDTO { Name = R.Name });
                if (representativesDTO==null)
                {
                    _log.Error("Representative repository returned null or empty List");
                    return null;
                }
                else
                {
                    _log.Info("Representatives List returned");
                    return representativesDTO;
                }
            }
            catch(Exception exception)
            {
                _log.Error("In Representatives Repository", exception);
                throw;
            }
            
        }
    }
}
