using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Conexion;
using System.Data.SqlClient;
using System.Data;
using Logica.Servicios;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Logica.ConexionServicios
{
    public class ConexionServicios
    {
        public async Task InsertarRegistrosAsync(string uri,object tipo) {

            var myContent = JsonConvert.SerializeObject(tipo);
            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
            string url = "https://localhost:44308/";
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
            HttpResponseMessage res = await client.PostAsync(uri, stringContent);
            if (res.IsSuccessStatusCode)
            {
                var empResponse = res.Content.ReadAsStringAsync().Result;
            }
        }
        public async Task EiminarRegistroAsync(string uri,object dato)
        {


            var myContent = JsonConvert.SerializeObject(dato);
            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
            string url = "https://localhost:44308/";
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
            HttpResponseMessage res = await client.DeleteAsync(uri+dato);
            if (res.IsSuccessStatusCode)
            {
                var empResponse = res.Content.ReadAsStringAsync().Result;
            }

        }

    }
}
