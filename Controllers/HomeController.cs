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

        public HomeController(ILogger<HomeController> logger) //Sql ve database i�in taslak modellerin atamalar�n� yapt�k.
        {
            com = new MySqlCommand();
            con = new MySqlConnection();
            kisibilgileri = new List<Profile>();
            job = new List<PostJob>();
            _logger = logger;
        }

        public IActionResult Index()  //Ana sayfay� getiren mod�l
        {
            return View();
        }
        public IActionResult applicantPick()
        {
            return View();
        }
        //Mysql ba�lant�s� i�in olu�turulan kodlar
        MySqlCommand com;
        MySqlDataReader dr;
        MySqlConnection con;
        //Veritaban�na taslak ba�lant� olarak kullan�lmak i�in kullan�lan modeller
        List<Profile> kisibilgileri;
        List<PostJob> job;


        //Kullan�c� Profil ekran�n� getiren view
        public IActionResult Profile()
        {
            FetchPersonData();
            FetchTeklifData();
            return View();
        }
        private void FetchPersonData() //Kisi Bilgilerini ekrana yans�tan mod�l
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT kisi_id,email,kisi_adi,kisi_soyadi,kisi_para FROM aspnetusers WHERE";
                dr = com.ExecuteReader();                  //Kisi Bilgilerini databaseden �ekip okumas�n� sa�layan k�s�m
                while (dr.Read())
                {
                    kisibilgileri.Add(new Profile()
                    {
                        kisi_id = (int)dr["kisi_id"],
                        kisi_email = dr["email"].ToString(),
                        kisi_adi = dr["kisi_adi"].ToString(),
                        kisi_soyadi = dr["kisi_soyadi"]?.ToString(),        //Kisi Bilgilerini databaseden verilere aktar�p ekrana g�stermeyi sa�layan yer
                        kisi_para = (int)dr["kisi_para"]
                    });
                }
                con.Close();
            }
            catch (Exception ex)  //Hata olmas� durumunda patlamamas� i�in exception
            {

                throw ex;
            }

        }

        private void FetchTeklifData()  //Teklifleri G�rd���m�z yer
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT urun_id,urun_adi,urun_para,kisi_id,teklif_adi,teklif_para FROM view_teklifler";
                dr = com.ExecuteReader();  //Teklifleri databaseden �ekip okumas�n� sa�layan k�s�m
                while (dr.Read())
                {
                    kisibilgileri.Add(new Profile()
                    {
                        urun_id = (int)dr["urun_id"],
                        urun_adi = dr["urun_adi"].ToString(),
                        urun_para = (int)dr["urun_para"],             //Teklifleri databaseden verilere aktar�p ekrana g�stermeyi sa�layan yer
                        kisi_id = (int)dr["kisi_id"],
                        teklif_id = (int)dr["teklif_adi"],
                        teklif_para = (int)dr["teklif_para"]
                    });
                }
                con.Close();
            }
            catch (Exception ex)     //Hata olmas� durumunda patlamamas� i�in exception
            {
                throw ex;
            }

        }
        void connectionString()
        {
            con.ConnectionString = "server=localhost;uid=root;pwd=root;database=veritabani;";
        }
        private void FetchsatinalinanurunData()  //Satin Alinan urunleri g�steren k�s�m
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
                        urun_adi = dr["urun_adi"].ToString(),                               //Verileri databaseden string veya integer de�erlere yazd�rma
                        urun_aciklamasi = dr["urun_aciklamasi"].ToString(),
                        teklif_id = (int)dr["teklif_id"],
                        teklif_para = (int)dr["teklif_para"]
                    });
                }
                con.Close();
            }
            catch (Exception ex)      //Hata olmas� durumunda patlamamas� i�in exception
            {
                throw ex;
            }

        }
        private void FetchData()                       //Showjob mod�l� i�in gerekli olan ekrana yans�tmay� sa�layan veri �ekme mod�l�
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT urun_id,urun_adi,urun_aciklamasi,urun_type,urun_zaman,urun_para FROM urunler";
                dr = com.ExecuteReader();
                while (dr.Read())     //Yukar�da se�ilen verileri okuyan k�s�m
                {
                    job.Add(new PostJob()
                    {
                        urun_id = (int)dr["urun_id"],
                        urun_adi = dr["urun_adi"].ToString(),
                        urun_aciklamasi = dr["urun_aciklamasi"].ToString(),     //Verileri databaseden string veya integer de�erlere yazd�rma
                        urun_type = dr["urun_type"].ToString(),
                        urun_zaman = dr["urun_zaman"].ToString(),
                        urun_para = (int)dr["urun_para"]
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                                 //Hata olmas� durumunda exception ve msg throwluyor
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
