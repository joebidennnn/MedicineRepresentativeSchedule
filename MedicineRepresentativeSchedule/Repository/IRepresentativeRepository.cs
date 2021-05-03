using MedicineRepresentativeSchedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineRepresentativeSchedule.Repository
{
    public interface IRepresentativeRepository
    {
        IEnumerable<RepresentativeDTO> GetRepresentativeList();
    }
}
