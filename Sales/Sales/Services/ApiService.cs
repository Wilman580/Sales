using Newtonsoft.Json;
using Sales.Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Sales.Services
{
    
    public class ApiService
    {
        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();//Captura el valor retornado
                if (!response.IsSuccessStatusCode) //Pregunta si tuvo algun error
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }
                //deserealizar
                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response
                {
                    IsSuccess=true,
                    Result=list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }

            
        }
    }
}
