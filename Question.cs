using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_assignment
{
    class NonIntQuestion
    {
        // initalize variables
        string question, attributeAnswer;
        int attribute;
        // inizialize attribute array
        string[] ATTRIBUTE_NAMES = {"POSITON","TEAM","COMPETITION","AGE","BIRTHYEAR","MATCHES PLAYED","MATCHES STARTED","MINUTES PLAYED","MINUTUES PER 90","GOALS","SHOTS","SHOTS ON TARGET","SHOTS ON TARGET PERCENTAGE","GOALS PER SHOT","GOARLS PER SHOT ON TARGET","SHOT DISTANCE","SHOT FREEKICK","GOALS FROM PENALTIES","PENALTIES ATTEMPTED","PASS COMPLETETION"};

        // gets the attribute in question
        public NonIntQuestion(int attribute) {
            this.attribute = attribute;
        }

        // generates the question
        public void makeQuestion(Dictionary<string, List<string>> dataset)
        {
            this.attributeAnswer = findMode(dataset);

            this.question = $"is your players {ATTRIBUTE_NAMES[this.attribute - 1]}: " + this.attributeAnswer + " ?";
        }

        // prints question to the screen
        public void display_Question() {
            Console.WriteLine(this.question + ": ");
        }

        // gets users answer and validates the input
        public int fill_Answer() {
            string answer = "";
            while (!(answer == "0" || answer == "1" || answer=="2")) {
                Console.WriteLine("enter 1 for yes, 0 for no and 2 for dont know: ");
                answer = Console.ReadLine();
            }

            return Convert.ToInt32(answer);
        }

        // finds the statistcal average we will use to cut data set in the most optimized way
        public string findMode(Dictionary<string, List<string>> dataset) {
            List<string> templist = new List<string>();
            Dictionary<string, int> attributeCount = new Dictionary<string, int>();
            foreach (KeyValuePair<string, List<string>> entry in dataset)
            {
                templist = entry.Value;
                if (attributeCount.ContainsKey(templist[this.attribute]))
                {
                    attributeCount[templist[this.attribute]] += 1;
                }
                else {
                    attributeCount.Add(templist[this.attribute], 1);
                }
            }

            // found at: https://stackoverflow.com/questions/2805703/good-way-to-get-the-key-of-the-highest-value-of-a-dictionary-in-c-sharp
            return attributeCount.OrderByDescending(x => x.Value).FirstOrDefault().Key;
        }

        // returns attribute answer
        public string getAttributeAnswer() {
            return this.attributeAnswer;
        }


    }
}
