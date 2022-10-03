/**
 * Web Service Development -  FH Joanneum SS22
 * Project: Rest Azure
 * @author  Matthias Pöttler
 * @version 1.0
 * @date   04/10/2022
**/

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