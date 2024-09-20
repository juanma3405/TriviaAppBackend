using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaAppBL.Models
{
    public class TriviaResponseQuestions
    {
        public int ResponseCode { get; set; }
        public List<TriviaQuestion> Results { get; set; }
    }
}
