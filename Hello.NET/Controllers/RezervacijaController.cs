using Hello.NET.Domain.Models;
using Hello.NET.Models;
using Hello.NET.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private IHubContext<RezervacijeHub, IHubClient> _hubContext;

        public RezervacijaController(IConfiguration configuration,IHubContext<RezervacijeHub,IHubClient> hubContext) {
            _configuration = configuration;
           _hubContext = hubContext;
        }

        [HttpGet]
        [Route("get")]
        public JsonResult get() {

            string query = @"SELECT  r.rezervacijaID,l.letID,m1.naziv AS mestoPolaska,m2.naziv AS mestoDolaska,l.brojpresedanja,l.datumPolaska,l.brojMesta,
                            (CASE WHEN r.odobreno=1 THEN 'Potvrdjeno' ELSE 'U fazi cekanja' END) AS status,  (CASE WHEN r.agentID!='NULL' THEN CONCAT(CONCAT(k.ime,' '),k.prezime)  ELSE '' END)      AS agent        
                            FROM rezervacija r JOIN let l ON(r.letID=l.letID) JOIN mesto m1 ON(l.mestoPolaska=m1.mestoID) JOIN mesto m2 ON(l.mestoDolaska=m2.mestoID) LEFT JOIN korisnik k ON(r.agentID=k.korisnikID)";
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
            /*
            Rezervacija r = new Rezervacija();
            r.AgentID = 5;
            r.KorisnikID = 6;
            r.LetID = 7;
       


            try {
                _hubContext.Clients.All.BroadcastMessage(r);
            } catch(Exception e) {
                throw e;
            }
            */
            
            return new JsonResult(dataTable);
        }

            [HttpGet]
        [Route("get/all")]
        public JsonResult getAll() {

            string query = @"SELECT  r.rezervacijaID,l.letID,m1.naziv AS mestoPolaska,m2.naziv AS mestoDolaska,l.brojpresedanja,l.datumPolaska,l.brojMesta,
                            (CASE WHEN r.odobreno=1 THEN 'Potvrdjeno' ELSE 'U fazi cekanja' END) AS status,  (CASE WHEN r.agentID!='NULL' THEN CONCAT(CONCAT(k.ime,' '),k.prezime)  ELSE '' END)      AS agent        
                            FROM rezervacija r JOIN let l ON(r.letID=l.letID) JOIN mesto m1 ON(l.mestoPolaska=m1.mestoID) JOIN mesto m2 ON(l.mestoDolaska=m2.mestoID) LEFT JOIN korisnik k ON(r.agentID=k.korisnikID)";
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

        [HttpPost]
        [Route("get/all/by/userID")]
        public JsonResult GetAllByUser(Korisnik korisnik) {

            string query = @"SELECT  r.rezervacijaID,l.letID,m1.naziv AS mestoPolaska,m2.naziv AS mestoDolaska,l.brojpresedanja,l.datumPolaska,l.brojMesta,
(CASE WHEN r.odobreno=1 THEN 'Potvrdjeno' ELSE 'U fazi cekanja' END) AS status,  (CASE WHEN r.agentID!='NULL' THEN CONCAT(CONCAT(k.ime,' '),k.prezime)  ELSE '' END)      AS agent        
                            FROM rezervacija r JOIN let l ON(r.letID=l.letID) JOIN mesto m1 ON(l.mestoPolaska=m1.mestoID) JOIN mesto m2 ON(l.mestoDolaska=m2.mestoID) LEFT JOIN korisnik k ON(r.agentID=k.korisnikID)
                            WHERE r.rezervacijaid IN(SELECT rezervacijaid FROM rezervacija WHERE korisnikID=@korisnikID)";
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
        [HttpGet]
        [Route("get/all/by/agent")]
        public JsonResult GetAllByAgentID() {

            string query = @"SELECT r.agentID,k.ime,k.prezime,SUM(CASE WHEN r.agentID!='NULL' THEN 1 ELSE 0 END)  AS brojRezervacija
                            FROM rezervacija r JOIN korisnik k ON (r.agentID=k.korisnikID)
                            GROUP BY  r.agentID";
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
