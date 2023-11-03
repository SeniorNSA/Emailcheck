using System;
using System.Collections.Generic;

namespace SecurityClassifications
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> emailClass = new Dictionary<string, string>(); // this is what the if statment is about
            Dictionary<string, string> employeeRoles = new Dictionary<string, string> // this is what is defined 
            {
                {"NSAProgramTester1@outlook.com", "Head of Department"},
                {"NSAProgramTester2@outlook.com", "Employee"},
                {"NSAProgramTester3@outlook.com", "Intern"}
            };

            foreach (var entry in employeeRoles)// gets the values
            {
                if (entry.Value == "head of the department")// checks the values and classifies them
                {
                    emailClass[entry.Key] = "TOP SECRET, SECRET, CONFIDENTIAL, UNCLASSIFIED";
                    emailClass[entry.Key] = "HCS, COMINT, -GAMMA, -ECI, TALENT KEYHOLE";
                    emailClass[entry.Key] = "TS, S, C , U";
                    emailClass[entry.Key] = "HCS, SI, -G, -ECI XXX, TK";
                }
                else if (entry.Value == "Employee")
                {
                    emailClass[entry.Key] = "SECRET, CONFIDENTIAL, UNCLASSIFIED";
                    emailClass[entry.Key] = "HCS, COMINT, TALENT KEYHOLE";
                    emailClass[entry.Key] = "S, C, U";
                    emailClass[entry.Key] = "HCS, SI, TK";
                }
                else if (entry.Value == "Intern")
                {
                    emailClass[entry.Key] = " CONFIDENTIAL, UNCLASSIFIED";
                    emailClass[entry.Key] = "HCS, COMINT";
                    emailClass[entry.Key] = " C, U";
                    emailClass[entry.Key] = "HCS, SI";
                }
                
            }

            foreach (var entry in emailClass)//prints with the classification.
            {
                Console.WriteLine($"Email: {entry.Key} | Classification: {entry.Value}");
            }
        }
    }
}


