using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Proje.Areas.Identity.Data;

// Add profile data for application users by adding properties to the SampleUser class
public class SampleUser : IdentityUser
{
    public int kisi_id { get; set; }
    public string kisi_isim { get; set; }
    public string kisi_soyisim { get; set; }
    public int kisi_para { get; set; }
    public string kisi_type { get; set; }
}

