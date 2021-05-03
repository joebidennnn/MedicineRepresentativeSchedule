using MedicineRepresentativeSchedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineRepresentativeSchedule.Provider
{
    public interface IMedicineStockProvider
    {
        Task<List<MedicineStock>> GetMedicineStock();
    }
}
