using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_assignment
{
    class IntQuestion
    {
        // initalize variables
        string question, attributeAnswer;
        int attribute;
         // initalize attribute array
         string[] ATTRIBUTE_NAMES = {"POSITON","TEAM","COMPETITION","AGE","BIRTHYEAR","MATCHES PLAYED","MATCHES STARTED","MINUTES PLAYED","MINUTUES PER 90","GOALS","SHOTS","SHOTS ON TARGET","SHOTS ON TARGET PERCENTAGE","GOALS PER SHOT","GOARLS PER SHOT ON TARGET","SHOT DISTANCE","SHOT FREEKICK","GOALS FROM PENALTIES","PENALTIES ATTEMPTED","PASS COMPLETETION"};
        public IntQuestion(int attribute)
        {
            // populates attribute
            this.attribute = attribute;
        }

        // used to print question
        public void makeQuestion(Dictionary<string, List<string>> dataset)
        {
            this.attributeAnswer = Convert.ToString(findAvg(dataset));

            this.question = $"is your players {ATTRIBUTE_NAMES[this.attribute - 1]} > " + this.attributeAnswer + " ?";
        }

        // displays question to screen
        public void display_Question()
        {
            Console.WriteLine(this.question + ": ");
        }

        // gets users answer and validates the input
        public int fill_Answer()
        {
            string answer = "";
            while (!(answer == "0" || answer == "1" || answer == "2"))
            {
                Console.WriteLine("enter 1 for yes, 0 for no and 2 for dont know: ");
                answer = Console.ReadLine();
            }

            return Convert.ToInt32(answer);
        }

        // finda the statistical average we used to cut dataset in the most optimized way
        public int findAvg(Dictionary<string, List<string>> dataset)
        {
            long total = 0;
            int datasetlength = 0;
            foreach (KeyValuePair<string, List<string>> entry in dataset)
            {
                try
                {
                    total += Convert.ToInt32(entry.Value[this.attribute]);
                }
                catch (Exception e) {
                    continue;
                }
                datasetlength++;
            }

            return Convert.ToInt32(total / datasetlength);
        }

        // returns the attribute answer
        public string getAttributeAnswer()
        {
            return this.attributeAnswer;
        }
    }
}
