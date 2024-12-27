﻿using Microsoft.AspNetCore.Mvc;
using BarberApp.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Net.Http.Json;

namespace BarberApp.Controllers
{
    public class AIController : Controller
    {
        private readonly string _apiKey = "AIzaSyCGxtBMZLe0AX-wrLI2gS_ARVYunQjG6nY"; // API anahtarınızı buraya girin
        private readonly HttpClient _httpClient;

        public AIController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                ViewBag.ErrorMessage = "Lütfen bir resim dosyası yükleyin.";
                return View("Index");
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();
                    string base64Image = Convert.ToBase64String(imageBytes);

                    var requestData = new
                    {
                        contents = new[]
                         {
                            new
                             {
                              parts = new object[]
                                  {
                                       new
                                       {
                                        text = "Give this person only just a haircut suggest. Just use 2 or 3  sentences."
                                       },
                                        new
                                        {
                                          inlineData = new {
                                          mimeType = imageFile.ContentType,
                                           data = base64Image
                                          }
                                       }
                                     }
                                  }
                             }
                    };
                    var jsonContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync(
                                    $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_apiKey}",
                                     jsonContent);
                    response.EnsureSuccessStatusCode();

                    var apiResponse = await response.Content.ReadFromJsonAsync<GeminiResponse>();

                    if (apiResponse != null && apiResponse.candidates != null && apiResponse.candidates.Length > 0 && apiResponse.candidates[0].content != null && apiResponse.candidates[0].content.parts.Length > 0)
                    {
                        var generatedText = apiResponse.candidates[0].content.parts[0].text;
                        return View("Analyze", new ImageViewModel { ImageText = generatedText });
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "API'den geçersiz bir yanıt alındı.";
                        return View("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Bir hata oluştu: {ex.Message}";
                return View("Index");
            }
        }
    }
    public class GeminiResponse
    {
        public Candidate[] candidates { get; set; }
    }

    public class Candidate
    {
        public Content content { get; set; }
    }

    public class Content
    {
        public Part[] parts { get; set; }
    }

    public class Part
    {
        public string text { get; set; }
    }
}