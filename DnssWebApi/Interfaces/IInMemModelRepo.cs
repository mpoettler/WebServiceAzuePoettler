using DnssWebApi.Model;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnssWebApi
{
    public interface IInMemModelRepo
    {
        AdmaModel GetModel(int id);
        void CreateModel(AdmaModel model);
        void UpdateModel(AdmaModel model);
        void DeleteModel(int id);

        public IEnumerable<AdmaModel> GetAllModels(IConfiguration configuration);
    }
}