using DnssWebApi;
using DnssWebApi.Controllers;
using DnssWebApi.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmaTest
{
    class ItestService : IInMemModelRepo
    {
        public IInMemModelRepo _repsoitory { get; set; }
        public ILogger<AdmaController> logger { get; set; }
        public IConfiguration configuration { get; set; }



        public ItestService(IInMemModelRepo repository, ILogger<AdmaController> logger, IConfiguration configuration)
        {
            _repsoitory = repository;
            this.logger = logger;
            this.configuration = configuration;
        }

        Random rnd = new Random();
        private List<AdmaModel> listOfModels ;

        public ItestService()
        {
            listOfModels = CreateTestModels();   
        }

        public void CreateModel(AdmaModel model)
        {
            model.ID = 42;
            model.Maximum = 20;
            model.Minimum = 1;
            model.Value = 18;
            listOfModels.Add(model);
        }

        public void DeleteModel(int id)
        {
            var existing = listOfModels.First(a => a.ID == id);
            listOfModels.Remove(existing);
        }

        public IEnumerable<AdmaModel> GetAllModels(IConfiguration configuration)
        {
            return listOfModels;
        }

        public AdmaModel GetModel(int id)
        {
            return listOfModels.Where(a => a.ID == id).FirstOrDefault();
        }

        public void UpdateModel(AdmaModel model)
        {
            throw new NotImplementedException();
        }

        private AdmaModel CreateRandomModel()
        {
            return new()
            {
                ID = rnd.Next(1, 100),
                Minimum = rnd.Next(1, 10),
                Maximum = rnd.Next(11, 100),
                Value = rnd.Next(10, 100)
            };
        }

        private List<AdmaModel> CreateTestModels()
        {
            var testModelList = new List<AdmaModel>();
            testModelList.Add(CreateRandomModel());
            testModelList.Add(CreateRandomModel());
            testModelList.Add(CreateRandomModel());
            testModelList.Add(CreateRandomModel());
            return testModelList;
        }
    }
}
