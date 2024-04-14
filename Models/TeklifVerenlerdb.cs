using System.Data;
using MySql.Data.MySqlClient;

namespace Proje.Models
{
    public class TeklifVerenlerdb  //TeklifVerenler modülünün database model taslak kısmı
    {
        MySqlConnection con = new MySqlConnection("server=localhost;uid=root;pwd=root;database=veritabani");  //MySql Connectionstring
        public string TeklifAdd(TeklifVerenler urun)  //Teklif eklemek için taslak model
        {
            try
            {
                MySqlCommand com = new MySqlCommand("TeklifEkle", con); //Databasedeki TeklifEkle stored procedurunu çağıran kısım
                com.CommandType = CommandType.StoredProcedure;
                MySqlParameter urunZaman = com.Parameters.AddWithValue("urun_id", urun.urun_id);
                MySqlParameter urunPara = com.Parameters.AddWithValue("teklif_para", urun.teklif_para); //Stored Procedure Para metreleri
                con.Open();
                com.ExecuteNonQuery();
                con.Close();                                                //Sql Connection kısmı
                return ("OK");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)  //Hata olduğunda Ekrana mesaj yazdıran kısım
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
        }
        public string TeklifKabul(TeklifVerenler talep)  //Teklif Kabul Eden taslak modül
        {
            try
            {
                MySqlCommand com = new MySqlCommand();

                con.Open();
                com.Connection = con;
                com.CommandText = "CALL TalepOnayla (" + talep.teklif_id + ", 1)";     //Teklif onaylama yapan Stored Procedure kısmı
                com.ExecuteNonQuery();
                con.Close();
                return ("OK");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();                          //Hata olduğunda Ekrana mesaj yazdıran kısım
                }
                return (ex.Message.ToString());
            }
        }


    }
}
