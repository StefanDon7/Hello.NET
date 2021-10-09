using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using Hello.NET.Domain.Models;
namespace Hello.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LetController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LetController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("get/all")]
        public JsonResult GetAll()
        {
            string query = @"SELECT l.letid, m1.naziv as mestoPolaska,m2.naziv as MestoDolaska,l.brojPresedanja,l.datumPolaska,l.brojMesta,l.otkazan
                            FROM let l JOIN mesto m1 ON(l.mestoPolaska=m1.mestoID) JOIN mesto m2 ON(l.mestoDolaska=m2.mestoID)";

            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MySqlConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(dataTable);
        }
        [HttpGet]
        [Route("get/all/by/{mesto1?}/{mesto2?}/{datum?}")]
        public JsonResult GetAllByPlace1Place2Date(string? mesto1,string? mesto2,string? datum) {
            string query = @"SELECT  l.letid,m1.naziv AS mestoPolaska,m2.naziv AS MestoDolaska,l.brojPresedanja,l.datumPolaska,l.brojMesta,SUM(CASE WHEN r.rezervacijaID!='NULL' THEN 1 ELSE 0 END)  as brojRezervacija
                            FROM rezervacija r RIGHT JOIN let l ON(r.letid=l.letid) JOIN mesto m1 ON(l.mestoPolaska=m1.mestoID) JOIN mesto m2 ON(l.mestoDolaska=m2.mestoID)
                             WHERE m1.naziv='" + mesto1+"' AND m2.naziv='"+mesto2+"' AND l.datumPolaska LIKE '"+datum+ "%' AND otkazan=0 GROUP BY letid";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MySqlConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource)) {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon)) {
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(dataTable);
        }
        [HttpGet]
        [Route("get/all/by/{mesto1?}/{mesto2?}/{datum?}/notransfer")]
        public JsonResult GetAllByPlace1Place2DateNoTransfer(string? mesto1, string? mesto2, string? datum) {
            string query = @"SELECT  l.letid,m1.naziv AS mestoPolaska,m2.naziv AS MestoDolaska,l.brojPresedanja,l.datumPolaska,l.brojMesta,SUM(CASE WHEN r.rezervacijaID!='NULL' THEN 1 ELSE 0 END) as brojRezervacija
                            FROM rezervacija r RIGHT JOIN let l ON(r.letid=l.letid) JOIN mesto m1 ON(l.mestoPolaska=m1.mestoID) JOIN mesto m2 ON(l.mestoDolaska=m2.mestoID)
                             WHERE l.brojPresedanja=0 AND m1.naziv='" + mesto1 + "' AND m2.naziv='" + mesto2 + "' AND l.datumPolaska LIKE '" + datum + "%' AND otkazan=0 GROUP BY letid";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MySqlConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource)) {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon)) {
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(dataTable);
        }

        [HttpPut]
        [Route("cancel")]
        public JsonResult Put(Let let)
        {
            string query = @"update let set otkazan=1 where letID=@LetID";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MySqlConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@LetID", let.LetID);

                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from mesto where mestoID=@MestoID";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MySqlConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MestoID", id);
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }

        [HttpPost]
        [Route("add")]
        public JsonResult Post(Let let) {
            string query = @"insert into let (mestoPolaska,mestoDolaska,brojPresedanja,datumPolaska,brojMesta) values(@mesto1,@mesto2,@brojPresedanja,@datum,@brojMesta)";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MySqlConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource)) {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon)) {
                    myCommand.Parameters.AddWithValue("@mesto1", let.MestoPolaskaId);
                    myCommand.Parameters.AddWithValue("@mesto2", let.MestoDolaskaId);
                    myCommand.Parameters.AddWithValue("@brojPresedanja", let.BrojPresedanje);
                    myCommand.Parameters.AddWithValue("@datum", let.DatumPolaska);
                    myCommand.Parameters.AddWithValue("@brojMesta", let.BrojMesta);
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

    }
}
