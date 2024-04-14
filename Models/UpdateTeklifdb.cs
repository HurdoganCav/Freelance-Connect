using System.Data;
using System.Data.SqlClient;
using Proje.Models;

namespace Proje.Models
{
    public class UpdateTeklifdb
    {
        public string teklifguncelleme(UpdateTeklif teklif) //Teklif Guncelleme yapan modül
        {

            SqlConnection con = new SqlConnection("server=localhost;uid=Root;pwd=Root;database=veritabani");
            try
            {
                SqlCommand com = new SqlCommand("teklifGuncelle", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlParameter = com.Parameters.AddWithValue("teklif_id", teklif.teklif_id);
                SqlParameter sqlParameter1 = com.Parameters.AddWithValue("teklif_para", teklif.teklif_para); //Kodun databaseye stored procedure ile veri aktardığı kısım
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                return ("OK");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();                                       //Hata olduğunda Ekrana mesaj yazdıran kısım
                }
                return (ex.Message.ToString());
            }


        }
    }
}
