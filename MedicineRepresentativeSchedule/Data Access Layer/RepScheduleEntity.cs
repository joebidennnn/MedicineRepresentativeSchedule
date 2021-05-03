using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicineRepresentativeSchedule.Models;
using Microsoft.EntityFrameworkCore;
namespace MedicineRepresentativeSchedule.Data_Access_Layer
{
    public class RepScheduleEntity:DbContext
    {
        public RepScheduleEntity(DbContextOptions<RepScheduleEntity> options) : base(options)
        {
            if (!Doctors.Any()&&!Representatives.Any())
            {
                Doctors.AddRange(
                    new Doctor { Id = 1, Name = "Doctor1", ContactNumber = "987654321", TreatingAilment = "Orthopaedics" },
                    new Doctor { Id = 2, Name = "Doctor2", ContactNumber = "987654321", TreatingAilment = "General" },
                    new Doctor { Id = 3, Name = "Doctor3", ContactNumber = "987654321", TreatingAilment = "Gynaecology" },
                    new Doctor { Id = 4, Name = "Doctor4", ContactNumber = "987654321", TreatingAilment = "General" },
                    new Doctor { Id = 5, Name = "Doctor5", ContactNumber = "987654321", TreatingAilment = "Gynaecology" });

                Representatives.AddRange(
                    new Representative { Id = 1, Name = "Adam" },
                    new Representative { Id = 2, Name = "Charlie" },
                    new Representative { Id = 3, Name = "Matthew" }
                );
                SaveChanges();
            }
        }
        
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Representative> Representatives { get; set; }
        
    }
}
