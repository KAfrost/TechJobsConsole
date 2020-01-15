using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TechJobsConsole
{
    class JobData
    {
        // create new method called AllJobs and initialize the new list
        static List<Dictionary<string, string>> AllJobs = new List<Dictionary<string, string>>();
        static bool IsDataLoaded = false;
        
        //load the data from the csv 
        public static List<Dictionary<string, string>> FindAll()
        {
            LoadData();
            return AllJobs;
        }

        /* create a method called FindAll 
         * Returns a list of all values contained in a given column,
         * without duplicates. 
         */
        public static List<string> FindAll(string column)
        {
            //load the data from the csv
            LoadData();

            //create values as a list of strings
            List<string> values = new List<string>();

            //for each dictionary type called job in the list of dictionaries AllJobs
            foreach (Dictionary<string, string> job in AllJobs)
            {
                string aValue = job[column];

                if (!values.Contains(aValue))
                {
                    values.Add(aValue);
                }
            }
            return values;
        }

        public static List<Dictionary<string, string>> FindByColumnAndValue(string column, string value)
        {
            // load data, if not already loaded
            LoadData();

            // create a list of dictionaries called jobs 
            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>();

            // for each dictionary row in the list of dictionaries AllJobs
            foreach (Dictionary<string, string> row in AllJobs)
            {
                string aValue = row[column].ToLower();

                if (aValue.Contains(value.ToLower()))
                //if (aValue.Equals(value, System.StringComparison.OrdinalIgnoreCase))
                {
                    jobs.Add(row);
                }
            }

            return jobs;
        }


        public static List<Dictionary<string, string>> FindByValue(string value)
        {
            // load data, if not already loaded
            LoadData();

            // create a list of dictionaries called jobs 

            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>();

           // var comparer = System.StringComparer.OrdinalIgnoreCase;

            // for each dictionary row in the list of dictionaries AllJobs
           foreach (Dictionary<string, string> row in AllJobs)
           {
                foreach (KeyValuePair<string, string> keyValue in row)
                {
                    string aValue = keyValue.Value.ToLower();
                    //string aValue = keyValue.Value;
                    //need to use "Contains" instead of "Equals" to catch any cases like "Ruby, JavaScript"
                   //System.StringComparison comp = System.StringComparison.OrdinalIgnoreCase;

                    if (aValue.Contains(value.ToLower()))
                   // if (aValue.Equals(value, comp))
                        
                    {
                        jobs.Add(row);
                    }

                }
           }

            return jobs;
        }

        /*
         * Load and parse data from job_data.csv
         */
        private static void LoadData()
        {

            if (IsDataLoaded)
            {
                return;
            }

            List<string[]> rows = new List<string[]>();

            using (StreamReader reader = File.OpenText("job_data.csv"))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    string[] rowArrray = CSVRowToStringArray(line);
                    if (rowArrray.Length > 0)
                    {
                        rows.Add(rowArrray);
                    }
                }
            }

            string[] headers = rows[0];
            rows.Remove(headers);

            // Parse each row array into a more friendly Dictionary
            foreach (string[] row in rows)
            {
                Dictionary<string, string> rowDict = new Dictionary<string, string>();

                for (int i = 0; i < headers.Length; i++)
                {
                    rowDict.Add(headers[i], row[i]);
                }
                AllJobs.Add(rowDict);
            }

            IsDataLoaded = true;
        }


        /*
         * Parse a single line of a CSV file into a string array
         */
        private static string[] CSVRowToStringArray(string row, char fieldSeparator = ',', char stringSeparator = '\"')
        {
            bool isBetweenQuotes = false;
            StringBuilder valueBuilder = new StringBuilder();
            List<string> rowValues = new List<string>();

            // Loop through the row string one char at a time
            foreach (char c in row.ToCharArray())
            {
                if ((c == fieldSeparator && !isBetweenQuotes))
                {
                    rowValues.Add(valueBuilder.ToString());
                    valueBuilder.Clear();
                }
                else
                {
                    if (c == stringSeparator)
                    {
                        isBetweenQuotes = !isBetweenQuotes;
                    }
                    else
                    {
                        valueBuilder.Append(c);
                    }
                }
            }

            // Add the final value
            rowValues.Add(valueBuilder.ToString());
            valueBuilder.Clear();

            return rowValues.ToArray();
        }
    }
}
