using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpEntropy;
using OpenNLP;

namespace GraduateWorkTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var paragraph = "Mr. & Mrs. Smith is a 2005 American romantic comedy action film. The film stars Brad Pitt and Angelina Jolie as a bored upper-middle class married couple. They are surprised to learn that they are both assassins hired by competing agencies to kill each other.";
            var modelPath = "path/to/EnglishSD.nbin";
            var sentenceDetector = EnglishMaximumEntropySentenceDetector(modelPath);
            var sentences = sentenceDetector.SentenceDetect(paragraph);
            /* 
             * sentences = ["Mr. & Mrs. Smith is a 2005 American romantic comedy action film.", 
             * "The film stars Brad Pitt and Angelina Jolie as a bored upper-middle class married couple.", 
             * "They are surprised to learn that they are both assassins hired by competing agencies to kill each other."]
             */
        }
    }
}
