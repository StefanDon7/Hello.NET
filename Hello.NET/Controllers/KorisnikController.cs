using Hello.NET.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.NET.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class KorisnikController : ControllerBase {
        private readonly IConfiguration _configuration;

        public KorisnikController(IConfiguration configuration) {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("get")]
        public JsonResult PostGet(Korisnik Korisnik) {
            string query = @"select * from korisnik where email=@email And sifra=@sifra";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MySqlConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource)) {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon)) {
                    myCommand.Parameters.AddWithValue("@email", Korisnik.Email);
                    myCommand.Parameters.AddWithValue("@sifra", Korisnik.Sifra);
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(dataTable);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult PostAdd(Korisnik Korisnik) {
            string query = @"insert into korisnik (ime,prezime,email,sifra) values(@ime,@prezime,@email,@sifra)";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MySqlConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource)) {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon)) {
                    myCommand.Parameters.AddWithValue("@ime", Korisnik.Ime);
                    myCommand.Parameters.AddWithValue("@prezime", Korisnik.Prezime);
                    myCommand.Parameters.AddWithValue("@email", Korisnik.Email);
                    myCommand.Parameters.AddWithValue("@sifra", Korisnik.Sifra);
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
