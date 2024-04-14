using System.Data;
using MySql.Data.MySqlClient;

namespace Proje.Models
{
    public class PostJobdb  //Postjob Modelinin database kısmıyla iletişime geçen modül
    {
        MySqlConnection con = new MySqlConnection("server=localhost;uid=root;pwd=root;database=veritabani");
        public string AddJob(PostJob urun)  //Databaseye addjob yapan kısım
        {
            try
            {
                MySqlCommand com = new MySqlCommand("UrunEkle", con);  //Databasede UrunEkle Stored Procedure çalışıyor
                com.CommandType = CommandType.StoredProcedure;
                MySqlParameter urunAdi = com.Parameters.AddWithValue("p_urun_adi", urun.urun_adi);
                MySqlParameter urunAciklamasi = com.Parameters.AddWithValue("p_urun_aciklamasi", urun.urun_aciklamasi);
                MySqlParameter urunType = com.Parameters.AddWithValue("p_urun_type", urun.urun_type);    //Database urun bilgilerini aktaran kısım
                MySqlParameter urunZaman = com.Parameters.AddWithValue("p_urun_zaman", urun.urun_zaman);
                MySqlParameter urunPara = com.Parameters.AddWithValue("p_urun_para", urun.urun_para);
                con.Open();
                com.ExecuteNonQuery();    //Komut çalıştırılan kısım
                con.Close();
                return ("OK");
            }
            catch (Exception ex)     //Hata olması durumunda uygulamanın patlamaması için exception
            {
                if (con.State== ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
            

        }
        

    }
}
