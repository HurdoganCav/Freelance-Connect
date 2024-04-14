using Google.Protobuf.WellKnownTypes;
using Proje.Models;

namespace Proje.Models
{
    public class PostJob //Job modulü için gereken model
    {
        public int urun_id { get; set; }
        public string urun_adi {  get; set; }
        public string urun_aciklamasi { get; set; }
        public string urun_type { get; set; }
        public string urun_status { get; set; }
        public string urun_zaman { get; set; }
        public int urun_para { get; set; }
        public int teklif_para { get; set; }

    }
}
