/**
 * Web Service Development -  FH Joanneum SS22
 * Project: Rest Azure
 * @author  Matthias Pöttler
 * @version 1.0
 * @date   04/10/2022
**/

using DnssWebApi.Dto;
using DnssWebApi.Interfaces;
using DnssWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace DnssWebApi.Controllers
{

    [ApiController]
    [Route("api")]
    public class AdmaController : ControllerBase
    {
        private readonly IInMemModelRepo _repsoitory;
        private readonly ILogger<AdmaController> logger;
        private readonly IConfiguration configuration;


        public AdmaController(IInMemModelRepo repository, ILogger<AdmaController> logger, IConfiguration configuration)
        {
            _repsoitory = repository;
            this.logger = logger;
            this.configuration = configuration;
        }

        /// <summary>
        /// Method to retrun all the Models from the Azure Database and show it in swagger
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<AdmaModel> GetModels()
        {
            return GetAllModels();
        }

        /// <summary>
        /// Method that Updates the Adma Model in the Azure Database
        /// </summary>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public ActionResult UpdateModel(int id,UpdateModelDto modelDto)
        {
            AdmaModel updatedItem = new()
            {
                Value = modelDto.Value
            };

            using (var connection = new SqlConnection(configuration.GetConnectionString("WebServicePoettler")))
            {
                var sql = $"UPDATE dbo.ModelTable SET Value = {updatedItem.Value} WHERE id = {id}";
                connection.Open();
                using SqlCommand command = new SqlCommand(sql, connection);
                int i = command.ExecuteNonQuery();
            };
            return NoContent();
        }

        /// <summary>
        /// Deletes an Model from the SQL Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteModel(int id)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("WebServicePoettler")))
            {
                var sql = $"DELETE FROM dbo.ModelTable WHERE ID = {id}";
                connection.Open();
                using SqlCommand command = new SqlCommand(sql, connection);
                int i = command.ExecuteNonQuery();
            }
            return NoContent();
        }

        /// <summary>
        /// GEt every Model from the Azure database
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AdmaModel> GetAllModels()
        {
            var models = new List<AdmaModel>();

            using (var connection = new SqlConnection(configuration.GetConnectionString("WebServicePoettler")))
            {
                var sql = "SELECT id, Minimum, Maximum, Value from dbo.ModelTable";
                connection.Open();
                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var model = new AdmaModel()
                    {
                        ID = (int)reader["Id"],
                        Minimum = (int)reader["Minimum"],
                        Maximum = (int)reader["Maximum"],
                        Value = (int)reader["Value"]
                    };

                    models.Add(model);
                }
            }
            return models;
        }

        /// <summary>
        /// Creates a new MOdel and saves it in the AZure Database
        /// </summary>
        /// <param name="modelDto"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ActionResult CreateModel(CreateModelDto modelDto)
        {
            if (modelDto == null)
            {
                throw new ArgumentNullException("AdmaModel is null");
            }

            AdmaModel model = new()
            {
                ID = modelDto.ID,
                Minimum = modelDto.Minimum,
                Maximum = modelDto.Maximum,
                Value = modelDto.Value
            };

            using (var connection = new SqlConnection(configuration.GetConnectionString("WebServicePoettler")))
            {
                var sql = $"INSERT INTO dbo.ModelTable(id, minimum, maximum, value) VALUES(@id, @minimum, @maximum, @value);";
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    
                    command.Parameters.AddWithValue("@id", modelDto.ID);
                    command.Parameters.AddWithValue("@minimum", modelDto.Minimum);
                    command.Parameters.AddWithValue("@maximum", modelDto.Maximum);
                    command.Parameters.AddWithValue("@value", modelDto.Value);

                    int i = command.ExecuteNonQuery();
                }
            }
            return CreatedAtAction(nameof(GetModel), new { id = model.ID }, model.AsDtoAdmaModel());
        }

        /// <summary>
        /// Get a Spezific model from the Azure Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public AdmaModel GetModel(int id)
        {
            AdmaModel model = new AdmaModel();
            using (var connection = new SqlConnection(configuration.GetConnectionString("WebServicePoettler")))
            {
                var sql = $"SELECT * FROM dbo.ModelTable WHERE id = @id";
                using SqlCommand command = new SqlCommand(sql, connection);
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model.ID = (int)reader["id"];
                            model.Minimum = (int)reader["minimum"];
                            model.Maximum = (int)reader["maximum"];
                            model.Value = (int)reader["value"];
                        }
                    }
                }  
                //int i = command.ExecuteNonQuery();
            }
            return model;
        }
    }
}

