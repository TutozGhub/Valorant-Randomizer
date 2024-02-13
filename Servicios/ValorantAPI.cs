using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Security.Policy;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Data.Odbc;
using System.Data;
using Configuracion;
using System.Collections;

namespace Servicios
{
    public static class ValorantAPI
    {
        private static string PARAMETRO1 = "es-MX";
        private static string PARAMETRO2 = "true";
        public static async Task GetPersonajes()
        {
            string URL = "https://valorant-api.com/v1/agents";
            using (var client = new HttpClient())
            {
                var queryParameters = new Dictionary<string, string>
            {
                { "language", PARAMETRO1 },
                { "isPlayableCharacter", PARAMETRO2 }
            };

                var fullUrl = new UriBuilder(URL);
                fullUrl.Query = new FormUrlEncodedContent(queryParameters).ReadAsStringAsync().Result;
                HttpResponseMessage response = await client.GetAsync(fullUrl.ToString());
                string responseBody = await response.Content.ReadAsStringAsync();

                JObject jsonObject = JObject.Parse(responseBody);
                JToken DATA = jsonObject.GetValue("data");
                List<JToken> displayName = DATA.Values("displayName").ToList();
                List<JToken> displayIcon = DATA.Values("displayIcon").ToList();
                List<JToken> role = DATA.Values("role").Values("displayName").ToList();

                for (int i = 0, len = displayName.Count; i < len; i++)
                {
                    if (displayName[i].ToString() == "Sage" || displayName[i].ToString() == "Skye")
                    {
                        role[i] = "Sanador";
                    }
                    CC_Personajes.Agentes.Add(new CC_Personaje
                    {
                        Nombre = displayName[i].ToString(),
                        Imagen = displayIcon[i].ToString(),
                        Rol = role[i].ToString()
                    });
                }
            }
        }
        public static async Task GetMapas()
        {
            string URL = "https://valorant-api.com/v1/maps";
            using (var client = new HttpClient())
            {
                var queryParameters = new Dictionary<string, string>
            {
                { "language", PARAMETRO1 }
            };

                var fullUrl = new UriBuilder(URL);
                fullUrl.Query = new FormUrlEncodedContent(queryParameters).ReadAsStringAsync().Result;
                HttpResponseMessage response = await client.GetAsync(fullUrl.ToString());
                string responseBody = await response.Content.ReadAsStringAsync();

                JObject jsonObject = JObject.Parse(responseBody);
                JToken DATA = jsonObject.GetValue("data");
                List<JToken> displayName = DATA.Values("displayName").ToList();
                List<JToken> listViewIcon = DATA.Values("listViewIcon").ToList();
                List<JToken> tacticalDescription = DATA.Values("tacticalDescription").ToList();
                List<JToken> coordinates = DATA.Values("coordinates").ToList();

                for (int i = 0, len = displayName.Count; i < len; i++)
                {
                    if(tacticalDescription[i].ToString() != "")
                    {
                        CC_Mapas.Mapas.Add(new CC_Mapa
                        {
                            Nombre = displayName[i].ToString(),
                            Imagen = listViewIcon[i].ToString(),
                            Coordenadas = coordinates[i].ToString(),
                            Sites = tacticalDescription[i].ToString()
                        });
                    }
                }
                Random rdm = new Random();
                CC_Mapas.Mapas = CC_Mapas.Mapas.OrderBy(x => rdm.Next()).ToList();
            }
        }
        public static async Task<string> GetVersion()
        {
            string URL = "https://valorant-api.com/v1/version";
            using (var client = new HttpClient())
            {

                var fullUrl = new UriBuilder(URL);
                HttpResponseMessage response = await client.GetAsync(fullUrl.ToString());

                string responseBody = await response.Content.ReadAsStringAsync();

                JObject jsonObject = JObject.Parse(responseBody);
                List<JToken> version = jsonObject.GetValue("data").Values().ToList();
                string ver = version[1].ToString();
                return ver;
            }
        }
    }
}
