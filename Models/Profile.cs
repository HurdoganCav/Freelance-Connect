namespace Proje.Models
{
    public class Profile
    {
        //Profile Sayfasında gözükecek kisi bilgilerini gösterecek model
        public int kisi_id {  get; set; }
        public string kisi_email { get; set; }
        public string kisi_adi { get; set; }
        public string kisi_soyadi { get; set; }
        public int kisi_para { get; set; }
        //Profil Sayfasında Gözükecek ürün bilgilerinin kullanımı için kısım
        public int urun_id { get; set; }
        public string urun_adi { get; set; }
        public int urun_para { get; set; }
        public string urun_aciklamasi { get; set; }
        public string urun_type { get; set; }
        public string urun_zaman { get; set; }
        //Profil sayfasında gözükecek olan teklif bilgilerini gösteren kısım
        public int teklif_id { get; set; }
        public int teklif_para { get; set; }
        public string eksik_parametre_adi { get; set; }




    }
}
