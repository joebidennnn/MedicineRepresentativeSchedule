using MedicineRepresentativeSchedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineRepresentativeSchedule.Service
{
    public interface IRepScheduleService
    {
        Task<IEnumerable<RepSchedule>> CreateRepSchedule(DateTime ScheduleStartDate);
        bool IsValid(DateTime scheduleStartdate);

    }
}
