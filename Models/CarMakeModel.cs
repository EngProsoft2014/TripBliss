using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Services;

namespace TripBliss.Models
{
    // سيتم استخدامه بدلاً من CarBrandResponse
    public class CarMakeModel
    {
        public int Id { get; set; } // Make_ID من NHTSA
        public string Name { get; set; } // MakeName
        public string NameAr { get; set; } // ستظل فارغة أو يمكن ترجمتها لاحقاً

        public CarMakeModel() { }

        public CarMakeModel(NHTSAService.MakeResult result)
        {
            Id = result.Make_ID;
            Name = result.Make_Name;
            NameAr = result.Make_Name; // مؤقتاً نفس الاسم
        }
    }

    // سيتم استخدامه بدلاً من CarModelResponse
    public class CarModelData
    {
        public int Id { get; set; } // Model_ID
        public string Name { get; set; } // Model_Name
        public string NameAr { get; set; }
        public int MakeId { get; set; } // للربط مع الماركة

        public CarModelData() { }

        public CarModelData(NHTSAService.ModelResult result, int makeId)
        {
            Id = result.Model_ID;
            Name = result.Model_Name;
            NameAr = result.Model_Name;
            MakeId = makeId;
        }
    }
}
