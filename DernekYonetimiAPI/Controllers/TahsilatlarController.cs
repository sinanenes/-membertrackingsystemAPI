using DernekYonetimiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DernekYonetimiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TahsilatlarController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<TahsilatlarController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public TahsilatlarController(ILogger<TahsilatlarController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select th.*,ki.KisiAdi + ' ' + ki.KisiSoyadi as KisiAdiSoyadi, kodthtur.KodAdi as TahsilatTurKodDeger, kdonemay.KodAdi as DonemAyKodDeger, kdonemyil.KodAdi as DonemYilKodDeger, kb.KasaBankaAdi as KasaBankaAdi from Tahsilatlar th left join Kisiler ki on th.KisiId = ki.KisiId left join Kodlar kodthtur on th.TahsilatTurKod = kodthtur.KodDeger and kodthtur.KodGrup='OdemeTurKod' left join Kodlar kdonemay on th.DonemAyKod=kdonemay.KodDeger and kdonemay.KodGrup = 'DonemAyKod' left join Kodlar kdonemyil on th.DonemYilKod=kdonemyil.KodDeger and kdonemyil.KodGrup = 'DonemYilKod' left join KasaBankalar kb on th.KasaBankaId = kb.KasaBankaId";
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
        public JsonResult Post(Tahsilat tahsilat)
        {
            string query = @"
                    insert into DernekDB.dbo.Tahsilatlar (KisiId,TahsilatTarih,DonemAyKod,DonemYilKod,TahsilatTurKod,TahsilatTutar,KasaBankaId,Aciklama) 
                    values('" + tahsilat.KisiId + @"','" + tahsilat.TahsilatTarih + @"','" + tahsilat.DonemAyKod + @"','" + tahsilat.DonemYilKod + @"','" + tahsilat.TahsilatTurKod + @"','" + tahsilat.TahsilatTutar + @"','" + tahsilat.KasaBankaId + @"','" + tahsilat.Aciklama + @"')
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
            return new JsonResult("Tahsilat Eklendi!");
        }

        [HttpPut]
        public JsonResult Put(Tahsilat tahsilat)
        {
            string query = @"
                    Update DernekDB.dbo.Tahsilatlar set KisiId = '" + tahsilat.KisiId + @"',TahsilatTarih='" + tahsilat.TahsilatTarih + @"',DonemAyKod='" + tahsilat.DonemAyKod + @"',DonemYilKod='" + tahsilat.DonemYilKod + @"',TahsilatTurKod='" + tahsilat.TahsilatTurKod + @"',TahsilatTutar='" + tahsilat.TahsilatTutar + @"',KasaBankaId='" + tahsilat.KasaBankaId + @"',Aciklama='" + tahsilat.Aciklama + @"'where TahsilatId = '" + tahsilat.TahsilatId + @"'
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
            return new JsonResult("Tahsilat Guncellendi!");
        }

        [HttpDelete("{tahsilatId}")]
        public JsonResult Delete(int tahsilatId)
        {
            try
            {
                string query = @"
                    Delete from DernekDB.dbo.Tahsilatlar where TahsilatId = '" + tahsilatId + @"'
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
                return new JsonResult("Tahsilat Silindi!");
            }
            catch (Exception)
            {
                return new JsonResult("Silme Hatası!");
            }
            
        }
    }
}
