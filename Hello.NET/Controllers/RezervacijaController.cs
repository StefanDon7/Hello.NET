using Hello.NET.Domain.Models;
using Hello.NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hello.NET.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RezervacijaController : ControllerBase {

        private readonly IConfiguration _configuration;

        public RezervacijaController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("get/all/by/userID")]
        public JsonResult GetAllByUser(Korisnik korisnik) {

            string query = @"SELECT  r.rezervacijaID,l.letID,m1.naziv AS mestoPolaska,m2.naziv AS MestoDolaska,l.brojpresedanja,l.datumPolaska,l.brojMesta,l.otkazan
                            FROM let l JOIN mesto m1 ON(l.mestoPolaska=m1.mestoID) JOIN mesto m2 ON(l.mestoDolaska=m2.mestoID) JOIN rezervacija r ON(r.letID=l.letID)
                            WHERE l.letID IN(SELECT letID FROM rezervacija WHERE korisnikID=@korisnikID)";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MySqlConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource)) {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon)) {
                    myCommand.Parameters.AddWithValue("@korisnikID", korisnik.KorisnikID);
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(dataTable);
        }
        [HttpPost]
        [Route("get/all/by/agentID")]
        public JsonResult GetAllByAgentID(Agent Agent) {

            string query = @"SELECT  r.rezervacijaID,l.letID,m1.naziv AS mestoPolaska,m2.naziv AS MestoDolaska,l.brojpresedanja,l.datumPolaska,l.brojMesta,l.otkazan
                            FROM let l JOIN mesto m1 ON(l.mestoPolaska=m1.mestoID) JOIN mesto m2 ON(l.mestoDolaska=m2.mestoID) JOIN rezervacija r ON(r.letID=l.letID)
                            WHERE l.letID IN(SELECT letID FROM rezervacija WHERE agentID=@AgentID)";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MySqlConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource)) {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon)) {
                    myCommand.Parameters.AddWithValue("@AgentID", Agent.AgentID);
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
        public JsonResult Post(Rezervacija rezervacija) {
            string query = @"insert into rezervacija (letID,korisnikID) values(@letID,@korisnikID)";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MySqlConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource)) {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon)) {
                    myCommand.Parameters.AddWithValue("@letID", rezervacija.LetID);
                    myCommand.Parameters.AddWithValue("@korisnikID", rezervacija.KorisnikID);
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }
        [HttpPut]
        [Route("update")]
        public JsonResult Put(Rezervacija rezervacija) {
            string query = @"update rezervacija set agentID=@AgentID,odobreno=1 where rezervacijaID=@RezervacijaID";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MySqlConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource)) {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon)) {
                    myCommand.Parameters.AddWithValue("@AgentID", rezervacija.AgentID);
                    myCommand.Parameters.AddWithValue("@RezervacijaID", rezervacija.RezervacijaID);
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
