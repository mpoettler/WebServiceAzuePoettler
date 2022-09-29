using DnssWebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnssWebApi
{
    public interface IInMemModelRepo
    {
        Task <AdmaModel> GetModel(int id);
        Task <IEnumerable<AdmaModel>> GetModels();
        Task <AdmaModel> GetVersion(string type);
        Task CreateModel(AdmaModel model);
        Task UpdateModel(AdmaModel model);
        Task DeleteModel(int id);
    }
}