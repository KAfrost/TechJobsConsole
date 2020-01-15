using System;
using System.Collections.Generic;

namespace TechJobsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create two Dictionary vars to hold info for menu and data

            // Top-level menu options
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");
            actionChoices.Add("list", "List");

            // Column options
            Dictionary<string, string> columnChoices = new Dictionary<string, string>();
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");

            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");

            // Allow user to search/list until they manually quit with ctrl+c
            while (true)
            {

                string actionChoice = GetUserSelection("View Jobs", actionChoices);

                if (actionChoice.Equals("list"))
                {
                    string columnChoice = GetUserSelection("List", columnChoices);

                    if (columnChoice.Equals("all"))
                    {
                        PrintJobs(JobData.FindAll());
                    }
                    else
                    {
                        List<string> results = JobData.FindAll(columnChoice);

                        Console.WriteLine("\n*** All " + columnChoices[columnChoice] + " Values ***");
                        foreach (string item in results)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                else // choice is "search"
                {
                    // How does the user want to search (e.g. by skill or employer)
                    string columnChoice = GetUserSelection("Search", columnChoices);

                    // What is their search term?
                    Console.WriteLine("\nSearch term: ");
                    string searchTerm = Console.ReadLine();
                    
                   //bool ifSearchTerm = true;

                    List<Dictionary<string, string>> searchResults;

                    // Fetch results
                    if (columnChoice.Equals("all"))
                    {
                        searchResults = JobData.FindByValue(searchTerm);
                        PrintJobs(searchResults);
                    }
                    else
                    {
                        searchResults = JobData.FindByColumnAndValue(columnChoice, searchTerm);
                        PrintJobs(searchResults);
                    }
                }
            }
        }


         /*
         * Returns the key of the selected item from the choices Dictionary
         */
        private static string GetUserSelection(string choiceHeader, Dictionary<string, string> choices)
        {
            //define variable types needed
            int choiceIdx;
            bool isValidChoice = false;
            string[] choiceKeys = new string[choices.Count];


            int i = 0;
            foreach (KeyValuePair<string, string> choice in choices)
            {
                //variable 'choiceKeys[i] is the key value of the choice and increments +1 - Search or List
                choiceKeys[i] = choice.Key;
                i++;
            }

            do
            {
                // display (ie) "search/list" (based on choiceHeader) "by:"
                Console.WriteLine("\n" + choiceHeader + " by:");

                // start count at 0 and iterate across the length of the choiceKeys, increments +1
                for (int j = 0; j < choiceKeys.Length; j++)
                {
                    //write the number from j and the Key of the choice
                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]);
                }

                // take in string input and convert to integer
                string input = Console.ReadLine();
                choiceIdx = int.Parse(input);

                //if the user choice is less than 0, or greater than the number of choices
                if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length)
                {
                    //tell the user the choice is invalid
                    Console.WriteLine("Invalid choices. Try again.");
                }
                else
                {
                    //if the choice is in the range of options, the choice is valid
                    isValidChoice = true;
                }

            } while (!isValidChoice);

            //return the index of the choice
            return choiceKeys[choiceIdx];
        }

        private static void PrintJobs(List<Dictionary<string, string>> someJobs)
        {
            if (someJobs.Count == 0)
            {
                Console.WriteLine("No Results Found.");
            }
            Console.WriteLine("****");
            // Iterate through list
            foreach (var dict in someJobs)
            {
                // Iterate through dict
                foreach (KeyValuePair<string, string> keyValue in dict)
                {
                    //var key = keyValue.Key;
                    //var value = keyValue.Value;

                    //Console.WriteLine(key + ": " + value);
                    Console.WriteLine("{0} : {1}", keyValue.Key, keyValue.Value);
                }
                Console.WriteLine("****");
            }
        }
    }
}
