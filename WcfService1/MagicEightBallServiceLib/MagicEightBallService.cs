using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEightBallServiceLib
{
    public class MagicEightBallService : IEightBall
    {
        public MagicEightBallService()
        {
            Console.WriteLine("enter question...");
        }

        public string ObtianAnswerToQuestion(string question)
        {
            return $"no anser for: {question}";
        }
    }
}
