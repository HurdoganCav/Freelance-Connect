using Microsoft.AspNetCore.Mvc;
using Proje.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;

namespace Proje.Controllers
{
    //Postjob showjob ve teklif verme modüllerinin olduğu controller
    public class JobController : Controller
    {
        //Mysql bağlantısı için oluşturulan kodlar
        MySqlCommand com;
        MySqlDataReader dr;
        MySqlConnection con;

        //Veritabanına taslak bağlantı olarak kullanılmak için kullanılan modeller
        List<PostJob> job;
        List<TeklifVerenler> Teklif;
        PostJobdb dbop;
        TeklifVerenlerdb dbo;
        private readonly ILogger<JobController> _logger;
        
        public IActionResult PostJob()   //Postjob ekranını her türlü başta değişkensiz getirmesi için 
        {
            return View();
        }
        [HttpPost]
        public ActionResult TeklifSend([Bind] TeklifVerenler teklif) //Teklif göndermek için
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string res = dbo.TeklifAdd(teklif);   //TeklifVerenlerdb deki komutu çalışıyor
                    TempData["msg"] = res;
                }

            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;             //Hata olması durumunda exception ve msg throwluyor
            }
            return View(ShowJob(null));
        }
        [HttpPost]
        public IActionResult PostJob([Bind] PostJob job)  //Postjob kısmında değerler girince Postjobdb ile veritabanına veri aktaran modül
        {
            try
            {
                if(ModelState.IsValid)
                {
                    string res = dbop.AddJob(job);       //Postjobdb modülünü burada kullanıyoruz
                    TempData["msg"] = res;
                }

            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;           //Hata olması durumunda exception ve msg throwluyor
            }
            return View();
        }
        private void FetchData()                       //Showjob modülü için gerekli olan ekrana yansıtmayı sağlayan veri çekme modülü
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT urun_id,urun_adi,urun_aciklamasi,urun_type,urun_zaman,urun_para FROM urunler WHERE urun_status='1'";
                dr = com.ExecuteReader();
                while (dr.Read())     //Yukarıda seçilen verileri okuyan kısım
                {
                    job.Add(new PostJob()
                    {
                        urun_id = (int)dr["urun_id"],
                        urun_adi = dr["urun_adi"].ToString(),
                        urun_aciklamasi = dr["urun_aciklamasi"].ToString(),     //Verileri databaseden string veya integer değerlere yazdırma
                        urun_type = dr["urun_type"].ToString(),
                        urun_zaman = dr["urun_zaman"].ToString(),
                        urun_para = (int)dr["urun_para"]
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                                 //Hata olması durumunda exception ve msg throwluyor
            }

        }
        private void FetchTeklifData()                                         //Teklif Datayı alması için yazılan modül
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT urun_id,urun_adi,kisi_id,kisi_adi,teklif_id,teklif_para FROM view_teklifler";
                dr = com.ExecuteReader();
                while (dr.Read())         //View_Teklifler kısmından çekilen kısmı okuyan yer
                {
                    Teklif.Add(new TeklifVerenler()
                    {
                        urun_id = (int)dr["urun_id"],
                        urun_adi = dr["urun_adi"].ToString(),
                        urun_para = (int)dr["urun_para"],        //Verileri databaseden değerlere ekrana yazdırmak için aktardığmıız yer
                        kisi_id = (int)dr["kisi_id"],
                        teklif_id = (int)dr["teklif_id"],
                        teklif_para = (int)dr["teklif_para"]
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);                                          //Hata olması durumunda exception ve msg throwluyor
            }

        }
        void connectionString()
        {
            con.ConnectionString = "server=localhost;uid=root;pwd=root;database=veritabani;";      //Connection string için yaptığımız modül
        }
        public IActionResult ShowJob(string searchString)                //Arama modülü
        {

            FetchData();
            if (!String.IsNullOrEmpty(searchString)) {       //Boş ise arama kısmının yapılmadığı yer
                return View(job);
            }
            else {
                var SearchByJobName = job.Where(p  => p.urun_adi == searchString).Select(p => p.urun_adi);  //Arama kısmının yapıldığı yer
                return View(job);
            }
             
        }
        public JobController(ILogger<JobController> logger)
        { 
            com = new MySqlCommand();
            con = new MySqlConnection();
            job = new List<PostJob>();
            Teklif = new List<TeklifVerenler>();             //Sql ve database için taslak modellerin atamalarını yaptık.
            dbop = new PostJobdb();
            _logger = logger;
            connectionString();
        }
    }
}
