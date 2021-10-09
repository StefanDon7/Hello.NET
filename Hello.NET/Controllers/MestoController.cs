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
using Hello.NET.Repository.Repository.Demo.DAL;
using Hello.NET.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hello.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MestoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MestoController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("get/all")]
        public JsonResult Get()
        {
            
            string query = @"select mestoID,naziv from mesto";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MySqlConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(MySqlCommand myCommand=new MySqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
           
            return new JsonResult(dataTable);
        }
        [HttpPost]
        public JsonResult Post(Mesto mesto )
        {
            string query = @"insert into mesto (naziv) values(@Naziv)";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MySqlConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Naziv", mesto.Naziv);
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }
        [HttpPut]
        public JsonResult Put(Mesto mesto)
        {
            string query = @"update mesto set naziv=@Naziv where mestoID=@MestoID";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MySqlConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MestoID", mesto.MestoID);
                    myCommand.Parameters.AddWithValue("@Naziv", mesto.Naziv);
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
    }
}
