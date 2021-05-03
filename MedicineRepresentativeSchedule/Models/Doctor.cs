using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineRepresentativeSchedule.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string TreatingAilment { get; set; }
    }
}
