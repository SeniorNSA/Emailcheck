using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
namespace BodyCheck
{





    class BodyCheck
    {

   

        static void Main(string[] args)
        {
            // Define the path to the file
            string path = "./email2.txt";

            // Call the new method to handle extraction and concatenation
            string extractedText = ExtractAndConcatenateTextInsideParentheses(path);

            // Call the new method to handle reading and processing
            //ReadAndProcessBody(path);

            Console.WriteLine(extractedText);


            //START OF ADDED STRINGS FOR CHECKING PERMISSIONS-----------------------------------

            // Call the new method to handle reading and processing
            ReadAndProcessBody(path);

            // Print the stored extracted texts
            Console.WriteLine(extractedText);
            }
            //END OF ADDED STRINGS------------------------------------------------------------------------


//START OF ADDED METHOD-----------------------------------------------------------------------------------
       //NEED A METHOD THAT WILL COMPARE EACH BODY CLASSIFICATION WITH CLASSIFICATIONS FOUND IN EMAIL
         // New method to compare dictionary strings to the TextinParentheses
        

//END OF ADDED METHOD -------------------------------------------------------------------------------------
        // New method to extract text inside parentheses using regex and concatenate into a string
        static string ExtractTextInParentheses(string input)
        {
            // This is the Regex for extracting text inside parentheses
            Regex regex = new Regex(@"\(([^)]*)\)");

            // Using match, this will find the first match in the input string
            Match match = regex.Match(input);

            // If a match is found, return the value inside the parentheses
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        static string ExtractAndConcatenateTextInsideParentheses(string path)
{
    // Initialize the StringBuilder for concatenating strings
    System.Text.StringBuilder extractedTextBuilder = new System.Text.StringBuilder();

    using (StreamReader sr = File.OpenText(path))
    {
        string s;
        while ((s = sr.ReadLine()) != null)
        {
            // Skip empty lines
            if (string.IsNullOrWhiteSpace(s))
            {
                continue;
            }

            // Push the text over to the method to extract the strings
            string textInParentheses = ExtractTextInParentheses(s);

            // Skip appending if the extracted text is empty
            if (!string.IsNullOrWhiteSpace(textInParentheses))
            {
                // Append the extracted text to the StringBuilder, separating the classifications by commas
                extractedTextBuilder.Append(textInParentheses);
                extractedTextBuilder.Append(",");
            }
        }
    }

    // Convert the StringBuilder to a string, removing the commas afterward
    string extractedText = extractedTextBuilder.ToString().TrimEnd(',');

    // Return the result
    return extractedText;
}

        static void ReadAndProcessBody(string path)
        {
            bool startReading = false;
            bool stopReading = false;

            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                while ((s = sr.ReadLine()) != null && !stopReading)
                {
                    if (s.Contains("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------"))
                    {
                        if (!startReading)
                        {
                            startReading = true;
                            sr.ReadLine(); // This will skip the first set of dashes, starting after the next set
                            continue;
                        }
                        else
                        {
                            stopReading = true;
                            break;
                        }
                    }
                    if (startReading)
            {
                // This method call will extract the text in parenthesis using method "TextInParenthesis"
                string textInParentheses = ExtractTextInParentheses(s);

                // Skip printing if the extracted text is empty
                if (!string.IsNullOrWhiteSpace(textInParentheses))
                {
                    // Process the extracted text as needed
                    Console.WriteLine($"Text inside parentheses: {textInParentheses}");
                }
            }
                }
            }

            
           
        }

        // Helper method to check if any classification exists in the given text
        
    }//CLASS
}//NAMESPACE