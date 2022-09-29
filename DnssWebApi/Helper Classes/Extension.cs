using DnssWebApi.Dto;
using DnssWebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DnssWebApi
{
    public static class Extension
    {
        public static AdmaModel AsDtoAdmaModel(this AdmaModel model)
        {
            if (model == null) throw new NullReferenceException();

            return new AdmaModel
            {
                ID = model.ID,
                Default = model.Default,
                Inaktiv = model.Inaktiv,
                Minimum = model.Minimum,
                Maximum = model.Maximum,
                Propertiers = model.Propertiers,
                Type = model.Type,
                Resolution = model.Resolution,
                Unit = model.Unit,
                Value = model.Value
            };
        }

        public static AdmaModelSendingJustValueDto AsSendingJustValueDto(this AdmaModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Model is null");
            }

            return new AdmaModelSendingJustValueDto()
            {
                Value = model.Value
            };
        }

        public static bool IsResponseSuccesful(this HttpWebResponse httpWebResponse)
        {
            if (httpWebResponse == null)
            {
                throw new Exception("WebResponse is null");
            }

            if ((int)httpWebResponse.StatusCode != 200)
            {
                throw new Exception("Not Succesful Statuscode");
            }
            else
            {
                return true;
            }
        }

        public static bool IsWebRequestStatusCodeSuccessful(this Task<HttpWebResponse> httpWebResponse)
        {
            if (httpWebResponse == null)
            {
                throw new Exception("WebResponse is null");
            }

            if ((int)httpWebResponse.Result.StatusCode != 200)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}
