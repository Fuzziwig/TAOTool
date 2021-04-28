using System;
using System.Collections.Generic;
using System.IO;

namespace TAOTool
{
    public class CSVAccess
    {
        readonly string csvDateFormat = "dd-MM-yyyy HH.mm";

        //method for checking if the csv format in the file is correct else return an errorlist
        public List<string> checkCSVFile(string filename)
        {
            List<string> output = new List<string>();
            //lets check if the file exist in the first place
            if (!File.Exists(filename))
            {
                output.Add(filename + " could not be found");
                return output;
            }
            //lets read the csv file and check the format
            using (var reader = new StreamReader(filename))
            {
                int linenumber = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    linenumber++;
                    var values = line.Split(';');
                    string result = "";
                    //check format of content of csv line
                    if (!checkCSVDate(values[0]))
                    {
                        result += " Line " + linenumber.ToString() + ", Field 1 [Date Error]";
                    }
                    if (!checkCSVFloat(values[2]))
                    {
                        result += " Line " + linenumber.ToString() + ", Field 2 [Float Error]";
                    }
                    if (values[3] != "MWh")
                    {
                        result += " Line " + linenumber.ToString() + ", Field 4 [Unit Error]";
                    }
                    if (!checkCSVFloat(values[4]))
                    {
                        result += " Line " + linenumber.ToString() + ", Field 5 [Float Error]";
                    }
                    if (values[5] != "M3")
                    {
                        result += " Line " + linenumber.ToString() + ", Field 6 [Unit Error]";
                    }
                    if (result != "")
                    {
                        output.Add(result);
                    }
                }
            }
            return output;
        }

        private bool checkCSVDate(string input)
        {
            DateTime dateValue;
            if (DateTime.TryParseExact(input, this.csvDateFormat,System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.None, out dateValue))
            {
                return true;
            }
            else return false;
        }

        private bool checkCSVFloat(string input)
        {
            double number;
            return Double.TryParse(input, out number);
        }

        //method for reading a csv file and returning a list of reading objects
        public List<Reading> readCSVFile(string filename)
        {
            List<Reading> readingList = new List<Reading>();

            using (var reader = new StreamReader(@filename))
            {
                int linenumber = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    linenumber++;
                    DateTime dateResult = DateTime.ParseExact(values[0], this.csvDateFormat, null);
                    readingList.Add(new Reading { RDate = dateResult, Mwh = double.Parse(values[2]), M3 = double.Parse(values[4]), UserID = linenumber });
                }
            }
            return readingList;
        }
    }
}
