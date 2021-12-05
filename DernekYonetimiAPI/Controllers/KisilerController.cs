using DernekYonetimiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DernekYonetimiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KisilerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<KisilerController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public KisilerController(ILogger<KisilerController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select ki.*,ko.KodAdi as KisiTipiKodDeger from DernekDB.dbo.Kisiler ki inner join DernekDB.dbo.Kodlar ko on ki.KisiTipiKod = ko.KodDeger and ko.KodGrup='KisiTipiKod'";
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
        public JsonResult Post(Kisi kisi)
        {
            string query = @"
                    insert into DernekDB.dbo.Kisiler (KisiAdi,KisiSoyadi,TCNo,KisiTipiKod,UyeNo,KisiFotoAdi) 
                    values('" + kisi.KisiAdi + @"','" + kisi.KisiSoyadi + @"','" + kisi.TCNo + @"','" + kisi.KisiTipiKod + @"','" + kisi.UyeNo + @"','" + kisi.KisiFotoAdi + @"')
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
            return new JsonResult("Uye Eklendi!");
        }

        [HttpPut]
        public JsonResult Put(Kisi kisi)
        {
            string query = @"
                    Update DernekDB.dbo.Kisiler set KisiAdi = '" + kisi.KisiAdi + @"',KisiSoyadi = '" + kisi.KisiSoyadi + @"',TCNo='" + kisi.TCNo + @"',KisiTipiKod='" + kisi.KisiTipiKod + @"',UyeNo='" + kisi.UyeNo + @"',KisiFotoAdi='" + kisi.KisiFotoAdi + @"'where KisiId = '" + kisi.KisiId + @"'
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
            return new JsonResult("Uye Guncellendi!");
        }

        [HttpDelete("{uyeId}")]
        public JsonResult Delete(int uyeId) //Delete methodunu duzelt daha sonra kisiyi silmeden sadece pasife cek!
        {
            string query = @"
                    Delete from DernekDB.dbo.Kisiler where KisiId = '" + uyeId + @"'
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
            return new JsonResult("Uye Silindi!");
        }

        [Route("SaveFile")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<JsonResult> SaveFileAsync()
        {
            try
            {
                //Changed sync to async since big sized file uploads were causing error-exceptions like 'Microsoft.AspNetCore.Server.Kestrel: Reading is already in progress.'
                //var httpRequest = Request.Form;
                //var postedFile = httpRequest.Files[0];
                var formCollection = await Request.ReadFormAsync();
                var postedFile = formCollection.Files.First();

                string fileName = postedFile.FileName;
                var physicalPath = _webHostEnvironment.ContentRootPath + "/Photos/" + fileName;
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(fileName);
            }
            catch (Exception)
            {
                return new JsonResult("Foto Kaydedilmedi!");
            }
        }

        [Route("GetAllKisiTipleri")]
        [HttpGet]
        public JsonResult GetAllKisiTipleri()
        {
            string query = @"select * from DernekDB.dbo.Kodlar where KodGrup='KisiTipiKod'";
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

        [Route("GetAllEgitmenler")]
        [HttpGet]
        public JsonResult GetAllEgitmenler()
        {
            string query = @"select * from DernekDB.dbo.Kisiler where Egitmenmi=1";
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
    }
}
