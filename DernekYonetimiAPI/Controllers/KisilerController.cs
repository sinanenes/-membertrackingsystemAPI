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
            string query = @"select ki.*,ko.KodAdi as KisiTipiKodDeger ,case when isnull((sqlborc.borctoplam - sqltah.tahsilattoplam),0) <0 then 0 else isnull((sqlborc.borctoplam - sqltah.tahsilattoplam),0) end as borcdurum from DernekDB.dbo.Kisiler ki inner join DernekDB.dbo.Kodlar ko on ki.KisiTipiKod = ko.KodDeger and ko.KodGrup='KisiTipiKod' left join (select bo.KisiId,sum(bo.BorcTutar) as borctoplam from DernekDB.dbo.Borclar bo group by bo.KisiId) sqlborc
on ki.KisiId = sqlborc.KisiId
left join (select ta.KisiId,sum(ta.TahsilatTutar) as tahsilattoplam from Tahsilatlar ta group by ta.KisiId) sqltah
on ki.KisiId = sqltah.KisiId
left join (select od.KisiId,sum(od.OdemeTutar) as odemetoplam from Odemeler od group by od.KisiId) sqlode
on ki.KisiId = sqltah.KisiId";
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
                    values('" + kisi.KisiAdi + @"','" + kisi.KisiSoyadi + @"','" + kisi.TCNo + @"','" + kisi.KisiTipiKod + @"','" + kisi.UyeNo + @"','" + kisi.KisiFotoAdi + @"');
                    " + "select cast(scope_identity() as int)";


            string sqlDataSource = _configuration.GetConnectionString("DernekDBCon");
            //SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    //sqlDataReader = sqlCommand.ExecuteReader();
                    //sqlDataReader.Close();
                    int kisiId = (int)sqlCommand.ExecuteScalar();
                    sqlConnection.Close();

                    if (kisi.UyeNo != 0)
                    {

                        for (int i = 0; i < 13 - DateTime.Now.Month; i++)
                        {
                            Borc borc = new Borc();
                            borc.KisiId = kisiId;
                            borc.BorcTarih = DateTime.Now;
                            borc.BorcTurKod = 1;

                            borc.DonemYilKod = DateTime.Now.Year;
                            borc.Aciklama = "Uye Eklendi";
                            borc.BorcTutar = 10;
                            borc.DonemAyKod = i;
                            BorcInsert(borc);
                        }


                    }
                }
            }
            return new JsonResult("Uye Eklendi!");
        }

        private void BorcInsert(Borc borc)
        {
            string query1 = @"
                    insert into DernekDB.dbo.Borclar (KisiId,BorcTarih,DonemAyKod,DonemYilKod,BorcTurKod,BorcTutar,Aciklama) 
                    values('" + borc.KisiId + @"','" + borc.BorcTarih + @"','" + borc.DonemAyKod + @"','" + borc.DonemYilKod + @"','" + borc.BorcTurKod + @"','" + borc.BorcTutar + @"','" + borc.Aciklama + @"')
                    ";
            //DataTable dataTable = new DataTable();
            string sqlDataSource1 = _configuration.GetConnectionString("DernekDBCon");
            SqlDataReader sqlDataReader1;
            using (SqlConnection sqlConnection1 = new SqlConnection(sqlDataSource1))
            {
                sqlConnection1.Open();
                using (SqlCommand sqlCommand1 = new SqlCommand(query1, sqlConnection1))
                {
                    sqlDataReader1 = sqlCommand1.ExecuteReader();
                    //dataTable.Load(sqlDataReader);
                    sqlDataReader1.Close();
                    sqlConnection1.Close();
                }
            }
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
            try
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
            catch (Exception)
            {
                return new JsonResult("Silme Hatası!");
            }
            
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
