/**
 * Web Service Development -  FH Joanneum SS22
 * Project: Rest Azure
 * @author  Matthias Pöttler
 * @version 1.0
 * @date   4/10/2022
**/

using DnssWebApi.Dto;
using DnssWebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

/// <summary>
/// Extension Methods to change Model into DTO models and Not used Extensions Methods to check if the Request was sucessfull
/// </summary>
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
                Minimum = model.Minimum,
                Maximum = model.Maximum,              
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
