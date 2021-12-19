using DernekYonetimiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DernekYonetimiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorclarController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<BorclarController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public BorclarController(ILogger<BorclarController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT * FROM DernekDB.dbo.Borclar";
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
        public JsonResult Post(Borc borc)
        {
            string query = @"
                    insert into DernekDB.dbo.Borclar (KisiId,BorcTarih,DonemAyKod,DonemYilKod,BorcTurKod,BorcTutar,Aciklama) 
                    values('" + borc.KisiId + @"','" + borc.BorcTarih+ @"','" + borc.DonemAyKod + @"','" + borc.DonemYilKod + @"','" + borc.BorcTurKod + @"','" + borc.BorcTutar + @"','" + borc.Aciklama + @"')
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
            return new JsonResult("Borc Eklendi!");
        }

        [HttpPut]
        public JsonResult Put(Borc borc)
        {
            string query = @"
                    Update DernekDB.dbo.Borclar set KisiId = '" + borc.KisiId + @"',BorcTarih='" + borc.BorcTarih + @"',DonemAyKod='" + borc.DonemAyKod + @"',DonemYilKod='" + borc.DonemYilKod + @"',BorcTurKod='" + borc.BorcTurKod + @"',BorcTutar='" + borc.BorcTutar + @"',Aciklama='" + borc.Aciklama + @"'where BorcId = '" + borc.BorcId + @"'
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
            return new JsonResult("Borc Guncellendi!");
        }

        [HttpDelete("{borcId}")]
        public JsonResult Delete(int borcId)
        {
            try
            {
                string query = @"
                    Delete from DernekDB.dbo.Borclar where BorcId = '" + borcId + @"'
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
                return new JsonResult("Borc Silindi!");
            }
            catch (Exception)
            {
                return new JsonResult("Silme Hatası!");
            }
            
        }
    }
}
