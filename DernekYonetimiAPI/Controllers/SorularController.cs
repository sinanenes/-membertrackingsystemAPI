using DernekYonetimiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DernekYonetimiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SorularController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<SorularController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public SorularController(ILogger<SorularController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select so.*,kusor.KullaniciAdi as SoranKullanici, kucev.KullaniciAdi as CevaplayanKullanici from Sorular so left join Kullanicilar kusor on so.KullaniciId = kusor.KullaniciId left join Kullanicilar kucev on so.CevaplayanId = kucev.KullaniciId";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DernekDBCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }
            return new JsonResult(dataTable);
        }

        [HttpPost]
        public JsonResult Post(Soru soru)
        {
            string query = @"
                    insert into DernekDB.dbo.Sorular (KullaniciId,SoruTarih,CevapTarih,SoruMetni,CevapMetni,CevaplayanId) 
                    values('" + soru.KullaniciId + @"','" + soru.SoruTarih + @"','" + soru.CevapTarih + @"','" + soru.SoruMetni + @"','" + soru.CevapMetni + @"','" + soru.CevaplayanId+ @"')
                    ";
            //DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DernekDBCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    //dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }
            return new JsonResult("Soru Eklendi!");
        }

        [HttpPut]
        public JsonResult Put(Soru soru)
        {
            string query = @"
                    Update DernekDB.dbo.Sorular set KullaniciId = '" + soru.KullaniciId + @"',SoruTarih = '" + soru.SoruTarih + @"',CevapTarih='" + soru.CevapTarih + @"',SoruMetni='" + soru.SoruMetni + @"',CevapMetni='" + soru.CevapMetni + @"',CevaplayanId='" + soru.CevaplayanId + @"'where SoruId = '" + soru.SoruId + @"'
                    ";
            //DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DernekDBCon");
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    //dataTable.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }
            return new JsonResult("Soru Guncellendi!");
        }

        [HttpDelete("{soruId}")]
        public JsonResult Delete(int soruId) //Delete methodunu duzelt daha sonra kisiyi silmeden sadece pasife cek!
        {
            try
            {
                string query = @"
                    Delete from DernekDB.dbo.Sorular where SoruId = '" + soruId + @"'
                    ";
                //DataTable dataTable = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("DernekDBCon");
                SqlDataReader sqlDataReader;
                using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlDataReader = sqlCommand.ExecuteReader();
                        //dataTable.Load(sqlDataReader);
                        sqlDataReader.Close();
                        sqlConnection.Close();
                    }
                }
                return new JsonResult("Soru Silindi!");
            }
            catch (Exception)
            {
                return new JsonResult("Silme Hatası!");
            }
            
        }

    }
}
