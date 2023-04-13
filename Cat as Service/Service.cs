using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RestSharp;
using Cat_as_Service.Models;
using System.Web;
using System.Text.Json.Nodes;

namespace Cat_as_Service
{
    internal class Service
    {
        public static Service inst = new Service();

        const string apiKey = "live_iejaDX23M5zAS4CybXhHRTIOkCRIeFg2sPbfGrxIuxQwQg2dldPRNdK46p0Gcumz";

        public Breed[] GetBreeds()
        {
            var client = new RestClient("https://api.thecatapi.com/v1/breeds");
            var response = client.Execute(new RestRequest("", Method.Get));

            if(response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<Breed[]>(response.Content);
            }
            else
            {
                Console.WriteLine(response.ErrorException);
                return null;
            }
        }

        public string GetImageId(string breedId)
        {
            var client = new RestClient($"https://api.thecatapi.com/v1/images/search?breed_ids={breedId}");
            var response = client.Execute(new RestRequest("", Method.Get));

            if (response.IsSuccessStatusCode)
            {
                var id = JsonDocument.Parse(response.Content).RootElement[0].GetProperty("id");
                return JsonSerializer.Deserialize<string>(id);
            }
            else
            {
                Console.WriteLine(response.ErrorException);
                return null;
            }
        }

        public string PostFavorite(string imageId, string breedId)
        {
            string json = $@"{{""image_id"": ""{imageId}"", ""sub_id"": ""{breedId}"" }}";

            var client = new RestClient($"https://api.thecatapi.com/v1/favourites");
            var request = new RestRequest(json, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-api-key", apiKey);

            var response = client.Execute(request);

            Debug.WriteLine(response.ErrorMessage);
            return response.ErrorMessage;
        }
    }
}
