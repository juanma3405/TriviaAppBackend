using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppBL.Models;

namespace TriviaAppBL.Interfaces
{
    public interface IServicio_APITrivia
    {
        Task<List<TriviaCategory>> GetCategories();
        Task<List<TriviaQuestion>> GetQuestions(TriviaConfig config);
    }
}
