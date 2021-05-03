using MedicineRepresentativeSchedule.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MedicineRepresentativeSchedule.Provider
{
    public class MedicineStockProvider : IMedicineStockProvider
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(MedicineStockProvider));
        public async Task<List<MedicineStock>> GetMedicineStock()
        {
            List<MedicineStock> medicineStockList;
            try
            {
                using(HttpClient Client=new HttpClient())
                {
                    _log.Info("Calling Medicine Stock Api");
                    var Response = await Client.GetAsync("https://localhost:5001/api/MedicineStockInformation");
                    if (Response.IsSuccessStatusCode)
                    {
                        var ApiResponse =await Response.Content.ReadAsStringAsync();
                        medicineStockList = JsonConvert.DeserializeObject<List<MedicineStock>>(ApiResponse);
                        _log.Info("Medicine Stock List Returned");
                        return medicineStockList;
                    }
                    else
                    {
                        _log.Error("Medicine Stock List Could not be Retrived, returned null");
                        return null;
                    }
                }
            }
            catch(HttpRequestException httpException)
            {
                _log.Error("exception In Medicine Stock Provider, could not connect to api",httpException);
                return null;
            }
            catch(Exception exception)
            {
                _log.Error(exception);
                throw;
            }
        }
    }
}
