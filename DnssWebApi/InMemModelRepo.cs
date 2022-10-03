/**
 * Web Service Development -  FH Joanneum SS22
 * Project: Rest Azure
 * @author  Matthias Pöttler
 * @version 1.0
 * @date   04/10/2022
**/

using DnssWebApi.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnssWebApi
{
    public class InMemModelRepo :  IInMemModelRepo
    {
        private readonly List<AdmaModel> models = new()
        {
            new AdmaModel
            {
                ID = 123,
                Minimum = 123,
                Maximum = 123,
                Value = 123
            }
        };

        public IEnumerable<AdmaModel> GetModels() => (models);

        public AdmaModel GetModel(int id)
        {
            var model = models.SingleOrDefault(model => model.ID == id);
            return  (model);
        }

        public void CreateModel(AdmaModel model)
        {
            models.Add(model);
        }

        public void  UpdateModel(AdmaModel model)
        {
            var index =  models.FindIndex(existingModel => existingModel.ID == model.ID);
            models[index] = model;
        }

        public void DeleteModel(int id)
        {
            var index = models.FindIndex(existingModel => existingModel.ID == id);
            models.RemoveAt(index);
        }

        public IEnumerable<AdmaModel> GetAllModels(IConfiguration configuration)
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
    }
}
