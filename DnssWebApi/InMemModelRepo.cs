using DnssWebApi.Model;
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
                Default = 123,
                ID = 123,
                Inaktiv = true,
                Minimum = 123,
                Maximum = 123,
                Propertiers = "",
                Type = "word",
                Resolution = 123,
                Unit = "word",
                Value = 123
            }
        };

        public async Task <IEnumerable<AdmaModel>> GetModels() => await Task.FromResult(models);

        public async Task<AdmaModel> GetModel(int id)
        {
            var model = models.SingleOrDefault(model => model.ID == id);
            return await Task.FromResult(model);
        }

        public async Task<AdmaModel> GetVersion(string type)
        {
            var model = models.Where(model => model.Type == type).SingleOrDefault();
            return await Task.FromResult(model);
        }

        public async Task CreateModel(AdmaModel model)
        {
            models.Add(model);
            await Task.CompletedTask;
        }

        public async Task UpdateModel(AdmaModel model)
        {
            var index =  models.FindIndex(existingModel => existingModel.ID == model.ID);
            models[index] = model;
            await Task.CompletedTask;
        }

        public async Task DeleteModel(int id)
        {
            var index = models.FindIndex(existingModel => existingModel.ID == id);
            models.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}
