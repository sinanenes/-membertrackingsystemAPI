using DernekYonetimiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DernekYonetimiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdemelerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<OdemelerController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public OdemelerController(ILogger<OdemelerController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT * FROM DernekDB.dbo.Odemeler";
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
        public JsonResult Post(Odeme odeme)
        {
            string query = @"
                    insert into DernekDB.dbo.Odemeler (KullaniciId,KisiId,OdemeTutar,OdemeTarih,OdemeTurKod,DonemAyKod,DonemYilKod,KasaBankaId,Aciklama) 
                    values('" + odeme.KullaniciId + @"','" + odeme.KisiId + @"','" + odeme.OdemeTutar+ @"','" + odeme.OdemeTarih + @"','" + odeme.OdemeTurKod + @"','" + odeme.DonemAyKod + @"','" + odeme.DonemYilKod + @"','" + odeme.KasaBankaId + @"','" + odeme.Aciklama + @"')
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
            return new JsonResult("Odeme Eklendi!");
        }

        [HttpPut]
        public JsonResult Put(Odeme odeme)
        {
            string query = @"
                    Update DernekDB.dbo.Odemeler set KullaniciId = '" + odeme.KullaniciId + @"',KisiId='" + odeme.KisiId + @"',OdemeTutar='" + odeme.OdemeTutar + @"',OdemeTarih='" + odeme.OdemeTarih + @"',OdemeTurKod='" + odeme.OdemeTurKod + @"',DonemAyKod='" + odeme.DonemAyKod + @"',DonemYilKod='" + odeme.DonemYilKod + @"',KasaBankaId='" + odeme.KasaBankaId + @"',Aciklama='" + odeme.Aciklama + @"'where OdemeId = '" + odeme.OdemeId + @"'
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
            return new JsonResult("Odeme Guncellendi!");
        }

        [HttpDelete("{odemeId}")]
        public JsonResult Delete(int odemeId)
        {
            try
            {
                string query = @"
                    Delete from DernekDB.dbo.Odemeler where OdemeId = '" + odemeId + @"'
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
                return new JsonResult("Odeme Silindi!");
            }
            catch (Exception)
            {
                return new JsonResult("Silme Hatası!");
            }
            
        }
    }
}
