using System;
using System.Collections.Generic;
using System.IO;
// used for certain dictionary operations
using System.Linq;

namespace AI_assignment
{
    class Program
    {

        static Dictionary<string, List<string>> readData(Dictionary<string, List<string>> dataset)
        {
            // creates array lines in csv dataset
            string[] lineArray;
            // creates a templist for player attributes
            List<string> templist = new List<string>();
            // file variable for dataset
            string file = @"data.csv";
            // opens the file
            File.OpenRead(file);
            // used to iterate through the file
            using (StreamReader streamReader = new StreamReader(file))
            {
                // used to store each line of the file
                string line = streamReader.ReadLine();

                // used to check if file has more lines
                while ((line = streamReader.ReadLine()) != null)
                {
                    // splits data using ; for player name and attributes
                    lineArray = line.Split(";");
                    // loops through the array
                    for (int i = 2; i < lineArray.Length; i++)
                    {
                        // adds the attribute to the array
                        templist.Add(lineArray[i]);
                    }

                    // checks if player is already a key in the dictionary
                    if (!dataset.ContainsKey(lineArray[1])){
                        // if not adds them and uses a new list for memory reference
                        dataset.Add(lineArray[1], new List<string>(templist));
                    }

                    // clears the temporary list
                    templist.Clear();

                }
            }

            // returns processed dataset dictionary
            return dataset;
        }

        // used to cut data out of our dataset based off of a decision tree modell
        static Dictionary<string, List<string>> partitionNonIntData(Dictionary<string, List<string>> dataset, int colNumber, int answer, string attributeAnswer)
        {
            // checks what our answer is
            if (answer == 1)
            {
                // loops through the data in our dataset
                foreach (KeyValuePair<string, List<string>> entry in dataset)
                {
                    // cuts data if attribute is certain to meet condition of answer
                    if (entry.Value[colNumber] != attributeAnswer)
                    {
                        dataset.Remove(entry.Key);
                    }
                }
                // returns dataset
                return dataset;
            }
            // if user answers dont know leaves the dataset as is
            else if (answer == 2) {
                return dataset;
            }

            // if answer is not cuts data from decision tree in other branch and direction
            foreach (KeyValuePair<string, List<string>> entry in dataset)
            {
                if (entry.Value[colNumber] == attributeAnswer)
                {
                    dataset.Remove(entry.Key);
                }
            }
            // returns dataset afterwards
            return dataset;
        }

        // does the same as last function for integer numbers
        static Dictionary<string, List<string>> partitionIntData(Dictionary<string, List<string>> dataset, int colNumber, int answer, string attributeAnswer)
        {
            if (answer == 1)
            {
                foreach (KeyValuePair<string, List<string>> entry in dataset)
                {
                    try
                    {
                        if (Convert.ToInt32(entry.Value[colNumber]) < Convert.ToInt32(attributeAnswer))
                        {
                            dataset.Remove(entry.Key);
                        }
                    }
                    catch (Exception e) {
                        continue;
                    }
                }
                return dataset;
            }
            else if (answer == 2) {
                return dataset;
            }
            foreach (KeyValuePair<string, List<string>> entry in dataset)
            {
                try
                {
                    if (Convert.ToInt32(entry.Value[colNumber]) > Convert.ToInt32(attributeAnswer))
                    {
                        dataset.Remove(entry.Key);
                    }
                }
                catch (Exception e) {
                    continue;
                }
            }
            return dataset;
        }

        // does the same for decimal point statistical attributes
        static Dictionary<string, List<string>> partitionDoubleData(Dictionary<string, List<string>> dataset, int colNumber, int answer, string attributeAnswer)
        {
            if (answer == 1)
            {
                foreach (KeyValuePair<string, List<string>> entry in dataset)
                {
                    try
                    {
                        if (Convert.ToDouble(entry.Value[colNumber]) < Convert.ToDouble(attributeAnswer))
                        {
                            dataset.Remove(entry.Key);
                        }
                    }
                    catch (Exception e) {
                        continue;
                    }

                }
                return dataset;
            }
            else if (answer == 2) {
                return dataset;
            }
            foreach (KeyValuePair<string, List<string>> entry in dataset)
            {
                try
                {
                    if (Convert.ToDouble(entry.Value[colNumber]) > Convert.ToDouble(attributeAnswer))
                    {
                        dataset.Remove(entry.Key);
                    }
                }
                catch (Exception e) { 
                    continue;
                }

            }
            return dataset;
        }

        // function is used to find player in the dataset and finds the player who has scored the most goals
        static string FindPlayer(Dictionary<string,List<string>> dataset) {
            KeyValuePair<string, List<string>> player = dataset.First(); ;
            foreach (KeyValuePair<string, List<string>> entry in dataset)
            {
                try
                {
                    if ((Convert.ToInt32(player.Value[10]) < Convert.ToInt32(entry.Value[10])))
                    {
                        player = entry;
                    }

                }
                catch (Exception e) {
                    player = entry;
                }
            }

            return player.Key;
        }

        static void Main(string[] args)
        {
            // used to see if player has been guessed
            bool playerGuessed = false;
            // used to collect users answer to question
            int answer;
            // used to store attribute being used for descision tree
            string attributeAnswer;
            // new dictionary for data
            Dictionary<string, List<string>> dataset = new Dictionary<string, List<string>>();
            // used to read data
            dataset = readData(dataset);
            // initalize count variable
            int count = 0;

            // while loop for 20 questions or untill player is guessed whenever is first
            while (count != 20 && playerGuessed == false)
            {
                // Key value pair for attribute analysis
                KeyValuePair<string, List<string>> top = new KeyValuePair<string, List<string>>();

                // gets top value in dataset
                foreach (KeyValuePair<string, List<string>> entry in dataset)
                {
                    top = entry;
                    break;
                }

                // increments count variable by 1
                count++;
                // check if atttribute is integer based
                if (int.TryParse(top.Value[count], out int value))
                {
                    IntQuestion question = new IntQuestion(count);
                    question.makeQuestion(dataset);
                    question.display_Question();
                    answer = question.fill_Answer();
                    attributeAnswer = question.getAttributeAnswer();
                    dataset = partitionIntData(dataset, count, answer, attributeAnswer);
                }
                // checks if attribute is double based
                else if (double.TryParse(top.Value[count], out double doublevalue)) {
                    DoubleQuestion question = new DoubleQuestion(count);
                    question.makeQuestion(dataset);
                    question.display_Question();
                    answer = question.fill_Answer();
                    attributeAnswer = question.getAttributeAnswer();
                    dataset = partitionDoubleData(dataset, count, answer, attributeAnswer);
                }
                // runs non number analysis
                else
                {
                    NonIntQuestion question = new NonIntQuestion(count);
                    question.makeQuestion(dataset);
                    question.display_Question();
                    answer = question.fill_Answer();
                    attributeAnswer = question.getAttributeAnswer();
                    dataset = partitionNonIntData(dataset, count, answer, attributeAnswer);
                }


                // prints out which question is complete
                Console.WriteLine($"Question {count} done!");

                // if dataset has one person remaining then it gives that player as the player found
                if (dataset.Count == 1) {
                    foreach (KeyValuePair<string, List<string>> entry in dataset)
                    {
                        Console.WriteLine("The player I guess is: " + entry.Key + "! ");
                        playerGuessed = true;
                    }
                }
            } 

            // if we go through all 20 questions and the player is not found the program is then designed to return the players with the most goals
            if(playerGuessed == false)
            {
                Console.WriteLine("The player I guess is: " + (string) FindPlayer(dataset) + "! ");
            }

            // used to ensure console stays up after execution
            Console.ReadLine();
        }
    }
}
