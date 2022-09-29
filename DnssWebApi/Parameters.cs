using ADMA_StartStop;
using DnssWebApi.Interfaces;
using Nancy.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DnssWebApi
{
    public class Parameters : IParameters
    {
        private readonly ADMAWeb admaStart = new(IPAddress.Parse(Addresses.IpAddress));

        /// <summary>
        /// Starts the ADMA for the Controll
        /// </summary>
        public void StartMessuarementAsync()
        {
            admaStart.StartMeasurement();
        }

        /// <summary>
        /// Stops the communication with the ADMA 
        /// </summary>
        /// <returns>bool</returns>
        public bool StopMessuarementAsync()
        {
            return SetVariable(true, Addresses.StopMesurementPath);

        }

        /// <summary>
        /// Saves the setting in the Adma Only use as last after all wanted changes are made or code will slow down
        /// </summary>
        public void SaveSettingsAsync()
        {
            admaStart.StartMeasurement();
        }

        public async Task<bool> SetMountingAngleAsync(float angleZ)
        {
            return await Task.FromResult(SetParamBool(Addresses.SetMountingAnglePath, angleZ));
        }

        public async Task<bool> SetMountingOffsetAsync(float x, float y, float z)
        {
            return await Task.FromResult(SetVariable(x, Addresses.SetMountingOffsetPathX) &&
                         SetVariable(y, Addresses.SetMountingOffsetPathY) &&
                         SetVariable(z, Addresses.SetMountingOffsetPathZ));
        }

        public async Task<bool> SetPositionPrimaryAntennaAsync(float x, float y, float z)
        {
            return await Task.FromResult(SetVariable(x, Addresses.SetPositionPrimaryAntennaPathX) &&
                         SetVariable(y, Addresses.SetPositionPrimaryAntennaPathY) &&
                         SetVariable(z, Addresses.SetPositionPrimaryAntennaPathZ));
        }

        public async Task<bool> SetPositionSecondayAntennaAsync(float x, float y, float z)
        {
            return await Task.FromResult(SetVariable(x, Addresses.SetPositionSecondayAntennaPathX) &&
                         SetVariable(y, Addresses.SetPositionSecondayAntennaPathY) &&
                         SetVariable(z, Addresses.SetPositionSecondayAntennaPathZ));
        }


        /// <summary>
        /// Sets The Position Points and Name based on the Index. The possible Indexes are between 1-8.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public async Task<bool> SetPOIAsync(int index, string name, float x, float y, float z)
        {
            if (index < 1 || index > 8)
            {
                throw new Exception("Index isn't allowed to be smaller then 1 and bigger then 8");
            }
            if (name == null)
            {
                throw new Exception("name is null");
            }

            //Local function to create the Addres for Poi raning from 1-8
            static string AddressBasedOnIndex(string xyz, int index)
            {
                return Addresses.SetPOIWithoutIndex + index.ToString() + "_" + xyz + ".json"; ;
            }

            return await Task.FromResult(SetVariable(name, AddressBasedOnIndex("name", index)) &&
                        SetVariable(x, AddressBasedOnIndex("x", index)) &&
                        SetVariable(y, AddressBasedOnIndex("y", index)) &&
                        SetVariable(z, AddressBasedOnIndex("z", index)));
        }

        /// <summary>
        /// Creates a WebRequest in JsonFormat, also a Timeout time can be used
        /// Timeout Value is 1000ms
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="relativePath"></param>
        /// <param name="method"></param>
        /// <returns>HttpWebRequest</returns>
        private static HttpWebRequest CreateWebRequest(Uri pathUri, ENUMS.WebRequest method)
        {
            if (pathUri == null)
            {
                throw new Exception("Uri Path is Null");
            }

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(pathUri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = $"{method}";
            httpWebRequest.Timeout = 1000;
            return httpWebRequest;
        }

        /// <summary>
        /// Creates an absulute URI based on base URI and Relative URI
        /// </summary>
        /// <param name="uriBase"></param>
        /// <param name="uriRelative"></param>
        /// <returns>URI</returns>
        private static Uri CreateAbsolutUri(string uriBase, string uriRelative)
        {
            if (null == uriBase || uriRelative == null)
            {
                throw new NullReferenceException("One or Both paths are null");
            }
            if (!uriRelative.Contains(@"\"))
            {
                uriRelative = uriRelative.Replace(@"\", "/");
            }
            if (!uriRelative.StartsWith("/"))
            {
                uriRelative = "/" + uriRelative;
            }
            if (!uriBase.Contains("http://") && !uriBase.StartsWith("http://"))
            {
                uriBase = "http://" + uriBase;
            }

            try
            {
                var baseUri = new Uri(uriBase, UriKind.Absolute);
                // create the full uri by combining the base and relative 
                return new Uri(baseUri, uriRelative);
            }
            catch (UriFormatException ex)
            {
                throw new UriFormatException($"URI formatting error: {ex}");
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentException($"URI string object is a null reference: {ex}");
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Argument Exception: {ex}");
            }
        }



        /// <summary>
        /// Sets a Parameter into the JSON - FILE 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="relativePath"></param>
        /// <returns>HttpWebResponse</returns>
        private static bool SetParamBool<T>(string relativePath, T val)
        {
            if (relativePath == null)
            {
                throw new ArgumentNullException(nameof(relativePath));
            }

            if (val == null)
            {
                throw new ArgumentNullException(nameof(val));
            }

            try
            {
                var httpWebRequest = CreateWebRequest(CreateAbsolutUri(IPAddress.Parse(Addresses.IpAddress).ToString(), relativePath), ENUMS.WebRequest.POST);
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(new JavaScriptSerializer().Serialize(new { value = val }));
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    streamReader.ReadToEnd();
                }

                return httpResponse.IsResponseSuccesful();
            }
            catch (Exception ex)
            {
                throw new ADMAException("Unable to apply parameter to ADMA", ex);
            }
        }


        /// <summary>
        /// Set the Variable it is important that StartVairable is used after the variables are Set.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool SetVariable(float x, string path) => SetParamBool(path, x);

        private static bool SetVariable(string x, string path) => SetParamBool(path, x);

        private static bool SetVariable(bool x, string path) => SetParamBool(path, x);

    }
}
