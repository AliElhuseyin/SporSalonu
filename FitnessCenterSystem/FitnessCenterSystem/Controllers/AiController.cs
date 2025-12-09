using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FitnessCenterSystem.Controllers
{
    public class AiController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _openAiApiKey = "YOUR_OPENAI_API_KEY"; // Kendi API key'inizi ekleyin

        public AiController()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_openAiApiKey}");
        }

        public IActionResult ExerciseRecommendation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetRecommendation(string bodyType, double? height, double? weight, string goal)
        {
            try
            {
                var prompt = $"Vücut tipi: {bodyType}, Boy: {height} cm, Kilo: {weight} kg, Hedef: {goal}. " +
                            "Bu kişi için haftalık egzersiz programı ve beslenme önerileri ver.";

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "system", content = "Sen bir fitness koçusun." },
                        new { role = "user", content = prompt }
                    },
                    max_tokens = 500
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<JsonElement>(responseString);
                    var recommendation = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

                    ViewBag.Recommendation = recommendation;
                    ViewBag.BodyType = bodyType;
                    ViewBag.Height = height;
                    ViewBag.Weight = weight;
                    ViewBag.Goal = goal;
                }
                else
                {
                    ViewBag.Error = "AI servisine bağlanılamadı.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Hata: {ex.Message}";
            }

            return View("ExerciseRecommendation");
        }
    }
}
