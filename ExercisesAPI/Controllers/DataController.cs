using System;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using ExercisesAPI.DAL;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.DAL;

namespace ExercisesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        AppDbContext _ctx;
        public DataController(AppDbContext context) // injected here
        {
            _ctx = context;
        }

        private async Task<String> getMenuItemJsonFromWebAsync()
        {
            string url = "https://raw.githubusercontent.com/elauersen/info3067/master/mcdonalds.json";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        public async Task<ActionResult<String>> Index()
        {
            DataUtility util = new DataUtility(_ctx);
            string payload = "";
            var json = await getMenuItemJsonFromWebAsync();
            try
            {
                  payload = (util.loadNutritionInfoFromWebToDb(json)) ? "tables loaded" : "problem loading tables";
            }
            catch (Exception ex)
            {
                payload = ex.Message;
            }
            return JsonSerializer.Serialize(payload);
        }


    }
}