using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TriviaAppBL.Interfaces;
using TriviaAppBL.Models;

namespace TriviaAppInfrastructure.ExternalServices
{
    public class Servicio_APITrivia : IServicio_APITrivia
    {
        private readonly HttpClient _httpClient;

        public Servicio_APITrivia(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<TriviaCategory>> GetCategories()
        {
            var url = $"https://opentdb.com/api_category.php";
            List<TriviaCategory> categories = new List<TriviaCategory>();
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var categoriesList = await response.Content.ReadAsStringAsync();
                    var triviaCategoriesResponse = JsonConvert.DeserializeObject<TriviaResponseCategory>(categoriesList);
                    foreach (var item in triviaCategoriesResponse.Trivia_Categories)
                    {
                        TriviaCategory Category = new TriviaCategory
                        {
                            Id = item.Id,
                            Name = item.Name,
                        };

                        categories.Add(Category);

                    }
                }
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching categories", ex);
            }     
        }

        public async Task<List<TriviaQuestion>> GetQuestions(TriviaConfig config)
        {
            var url = CreateURLQuestions(config.Category, config.Difficulty, config.Type);   
            var questions = new List<TriviaQuestion>();
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    questions = MapResponse(data);
                }
                return questions;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching questions", ex);
            }
        }


        private string CreateURLQuestions(string category, string difficulty, string type)
        {
            var url = $"https://opentdb.com/api.php?amount=10&category={category}";
            if (difficulty != "any")
            {
                url = $"{url}&difficulty={difficulty}";
            }
            if (type != "any")
            {
                url = $"{url}&type={type}";
            }
            return url;
        }

        private List<TriviaQuestion> MapResponse(string data)
        {
            var triviaResponse = JsonConvert.DeserializeObject<TriviaResponseQuestions>(data);
            List<TriviaQuestion> triviaQuestions = new List<TriviaQuestion>();
            foreach (var item in triviaResponse.Results)
            {
                TriviaQuestion question = new TriviaQuestion
                {
                    Question = Decode(item.Question),
                    Correct_Answer = Decode(item.Correct_Answer),
                    Incorrect_Answers = item.Incorrect_Answers.Select(answer => Decode(answer)).ToList()
                };

                triviaQuestions.Add(question);

            }

            return triviaQuestions;

        }
        private string Decode(string text)
        {
            var decodedString = WebUtility.HtmlDecode(text);
            return decodedString;
        }
    }
}
