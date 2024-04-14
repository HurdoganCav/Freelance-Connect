using Microsoft.AspNetCore.Mvc;
using Proje.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proje.Models;
using MySql.Data.MySqlClient;

namespace Proje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) //Sql ve database için taslak modellerin atamalarýný yaptýk.
        {
            com = new MySqlCommand();
            con = new MySqlConnection();
            kisibilgileri = new List<Profile>();
            job = new List<PostJob>();
            _logger = logger;
        }

        public IActionResult Index()  //Ana sayfayý getiren modül
        {
            return View();
        }
        public IActionResult applicantPick()
        {
            return View();
        }
        //Mysql baðlantýsý için oluþturulan kodlar
        MySqlCommand com;
        MySqlDataReader dr;
        MySqlConnection con;
        //Veritabanýna taslak baðlantý olarak kullanýlmak için kullanýlan modeller
        List<Profile> kisibilgileri;
        List<PostJob> job;


        //Kullanýcý Profil ekranýný getiren view
        public IActionResult Profile()
        {
            FetchPersonData();
            FetchTeklifData();
            return View();
        }
        private void FetchPersonData() //Kisi Bilgilerini ekrana yansýtan modül
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT kisi_id,email,kisi_adi,kisi_soyadi,kisi_para FROM aspnetusers WHERE";
                dr = com.ExecuteReader();                  //Kisi Bilgilerini databaseden çekip okumasýný saðlayan kýsým
                while (dr.Read())
                {
                    kisibilgileri.Add(new Profile()
                    {
                        kisi_id = (int)dr["kisi_id"],
                        kisi_email = dr["email"].ToString(),
                        kisi_adi = dr["kisi_adi"].ToString(),
                        kisi_soyadi = dr["kisi_soyadi"]?.ToString(),        //Kisi Bilgilerini databaseden verilere aktarýp ekrana göstermeyi saðlayan yer
                        kisi_para = (int)dr["kisi_para"]
                    });
                }
                con.Close();
            }
            catch (Exception ex)  //Hata olmasý durumunda patlamamasý için exception
            {

                throw ex;
            }

        }

        private void FetchTeklifData()  //Teklifleri Gördüðümüz yer
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT urun_id,urun_adi,urun_para,kisi_id,teklif_adi,teklif_para FROM view_teklifler";
                dr = com.ExecuteReader();  //Teklifleri databaseden çekip okumasýný saðlayan kýsým
                while (dr.Read())
                {
                    kisibilgileri.Add(new Profile()
                    {
                        urun_id = (int)dr["urun_id"],
                        urun_adi = dr["urun_adi"].ToString(),
                        urun_para = (int)dr["urun_para"],             //Teklifleri databaseden verilere aktarýp ekrana göstermeyi saðlayan yer
                        kisi_id = (int)dr["kisi_id"],
                        teklif_id = (int)dr["teklif_adi"],
                        teklif_para = (int)dr["teklif_para"]
                    });
                }
                con.Close();
            }
            catch (Exception ex)     //Hata olmasý durumunda patlamamasý için exception
            {
                throw ex;
            }

        }
        void connectionString()
        {
            con.ConnectionString = "server=localhost;uid=root;pwd=root;database=veritabani;";
        }
        private void FetchsatinalinanurunData()  //Satin Alinan urunleri gösteren kýsým
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT kisi_id,urun_id,urun_adi,urun_aciklamasi,teklif_id,teklif_para FROM view_satin_alinan_urunler";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    kisibilgileri.Add(new Profile()
                    {
                        kisi_id = (int)dr["kisi_id"],
                        urun_id = (int)dr["urun_id"],
                        urun_adi = dr["urun_adi"].ToString(),                               //Verileri databaseden string veya integer deðerlere yazdýrma
                        urun_aciklamasi = dr["urun_aciklamasi"].ToString(),
                        teklif_id = (int)dr["teklif_id"],
                        teklif_para = (int)dr["teklif_para"]
                    });
                }
                con.Close();
            }
            catch (Exception ex)      //Hata olmasý durumunda patlamamasý için exception
            {
                throw ex;
            }

        }
        private void FetchData()                       //Showjob modülü için gerekli olan ekrana yansýtmayý saðlayan veri çekme modülü
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT urun_id,urun_adi,urun_aciklamasi,urun_type,urun_zaman,urun_para FROM urunler";
                dr = com.ExecuteReader();
                while (dr.Read())     //Yukarýda seçilen verileri okuyan kýsým
                {
                    job.Add(new PostJob()
                    {
                        urun_id = (int)dr["urun_id"],
                        urun_adi = dr["urun_adi"].ToString(),
                        urun_aciklamasi = dr["urun_aciklamasi"].ToString(),     //Verileri databaseden string veya integer deðerlere yazdýrma
                        urun_type = dr["urun_type"].ToString(),
                        urun_zaman = dr["urun_zaman"].ToString(),
                        urun_para = (int)dr["urun_para"]
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                                 //Hata olmasý durumunda exception ve msg throwluyor
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
