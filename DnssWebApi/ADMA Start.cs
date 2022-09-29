// <copyright file="ADMAWeb.cs" company="GeneSys Elektronik GmbH">
//     Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>Jonathan Brookins</author>
// <summary>Erlaubt die Steuerung einer ADMA per json-Objekten.</summary>

namespace ADMA_StartStop
{
    using DnssWebApi;
    using Nancy.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Security;
    using System.Web;
    

    /// <summary>
    /// Erlaubt die Steuerung einer ADMA per json-Objekten.
    /// </summary>
    public class ADMAWeb
    {
        #region Constructor

        /// <summary>
        /// Erzeugt eine Instanz zur Steuerung einer ADMA über die angegebene IP-Adresse.
        /// </summary>
        /// <param name="ip">IP-Adresse der ADMA</param>
        public ADMAWeb(IPAddress ip)
        {
            IP = ip;
            try
            {
                Name = getParam<string>("/api/v1/config/system/info/config_adma_name.json", "value");
            }
            catch
            {
                Name = "---";
            }
        }

        #endregion

        #region Enums

        /// <summary>
        /// Senden oder Empfangen der Daten.
        /// </summary>
        private enum WebRequestMethod
        {
            /// <summary>
            /// GET/Empfang von Daten.
            /// </summary>
            GET,
            /// <summary>
            /// POST/Senden von Daten.
            /// </summary>
            POST
        }

        #endregion

        #region Properties

        /// <summary>
        /// Ruft die IP-Adresse der ADMA ab.
        /// </summary>
        public IPAddress IP { get; }

        /// <summary>
        /// Ruft den Namen der ADMA ab.
        /// </summary>
        public string Name { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Startet den Messmodus der ADMA.
        /// Vorab wird die Montagerichtung entsprechend des Wertes konfiguriert.
        /// </summary>
        /// <param name="mountingAngleZ">Montagerichtung der ADMA in Z.</param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ADMAException" />
        public void StartMeasurement(float mountingAngleZ)
        {
            setParam("/api/v1/config/parameter/mounting/config_mounting_angle_adma_z.json", mountingAngleZ);
            StartMeasurement();
        }

        /// <summary>
        /// Startet den Messmodus der ADMA.
        /// Spezialität: Enthält der Dateiname des Programms "gpgga", wird dieser Modus vorab in der ADMA aktiv geschaltet.
        /// </summary>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ADMAException" />
        public void StartMeasurement()
        {
            if (Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location).ToLower().Contains("gpgga"))
                setParam("/api/v1/config/aux_systems/gps/config_gps_gpgga_active.json", true);
            setParam("/api/v1/cmd/cmd_measurement_start.json", true);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Ruft den Inhalt einer json-Datei der ADMA ab. Schlägt der Zugriff fehl, wird eine entsprechende Exception geworfen.
        /// </summary>
        /// <param name="relativePath">Relativer Pfad zu der json-Datei. Netzwerkadresse (http://ip-adresse/) wird automatisch angefügt.</param>
        /// <returns>Inhalt der json-Datei als Dictionary</returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ADMAException" />
        private T getParam<T>(string relativePath, string paramName)
        {
            if (relativePath == null)
                throw new ArgumentNullException(nameof(relativePath));
            if (paramName == null)
                throw new ArgumentNullException(nameof(paramName));

            try
            {
                var httpWebRequest = createWebRequest(relativePath, WebRequestMethod.GET);
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var json = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(result);
                    return (T)json[paramName];
                }
            }
            catch(Exception ex)
            {
                throw new ADMAException("Unable to retrieve parameter from ADMA", ex);
            }
        }

        /// <summary>
        /// Sendet einen Parameter per json-Objekt (serialisiert) an die ADMA.
        /// </summary>
        /// <param name="relativePath">Relativer Pfad zur json-Datei. Netzwerkadresse (http://ip-adresse/) wird automatisch angefügt.</param>
        /// <param name="val">Zu übermittelnder Wert</param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ADMAException" />
        private void setParam<T>(string relativePath, T val)
        {
            if (relativePath == null)
                throw new ArgumentNullException(nameof(relativePath));
            if (val == null)
                throw new ArgumentNullException(nameof(val));

            try
            {
                var httpWebRequest = createWebRequest(relativePath, WebRequestMethod.POST);
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    streamWriter.Write(new JavaScriptSerializer().Serialize(new { value = val }));

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new ADMAException("Unable to apply parameter to ADMA", ex);
            }
        }            
        /// <summary>
        /// Erzeugt einen HTTP-Webrequest mit einem festen Timeout von 500ms für json-Daten.
        /// </summary>
        /// <param name="relativePath">Relativer Pfad zur json-Datei. Netzwerkadresse (http://ip-adresse/) wird automatisch angefügt.</param>
        /// <param name="method">Senden: POST, Empfangen: GET</param>
        /// <returns>HTTP-Webrequest</returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="NotSupportedException" />
        /// <exception cref="SecurityException" />
        /// <exception cref="UriFormatException" />
        private HttpWebRequest createWebRequest(string relativePath, WebRequestMethod method)
        {
            if (!relativePath.StartsWith("/"))
                relativePath = "/" + relativePath;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://{IP}{relativePath}");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = $"{method}";
            httpWebRequest.Timeout = 500;
            return httpWebRequest;
        }
        #endregion
    }

    /// <summary>
    /// Generelle ADMA-Exception, zur Zusammenfassung vielfältiger Ausnahmen, 
    /// die beim Senden/Empfangen von Parametern der ADMA auftreten können.
    /// Details können über die InnerException abgerufen werden.
    /// </summary>
    public class ADMAException : Exception
    {
        /// <summary>
        /// Erzeugt ein Objekt zur Zusammenfassung vielfältiger Ausnahmen,
        /// die beim Senden/Empfangen von Parametern der ADMA auftreten können.
        /// </summary>
        /// <param name="message">Zusätzliche Fehlermeldung</param>
        /// <param name="innerException">Ursprüngliche Exception.</param>
        public ADMAException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
