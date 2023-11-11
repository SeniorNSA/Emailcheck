//Header is sender
//Footer is receiver
//read and validate
//Header is what needs regex, split them up and check each
//footer is the permissions of receiver, use regex to split this.
//grab users current permissions, split them into regex 
// There are multiple sections, the split in sections are indicatted with either '//' or '/'

//1. check the values of the person who is sending and receiving the email
//2. compare the two values together
//3. If the sender does not match the receiver then the email is not allowed to be sent

using System;
using System.IO;
using System.Text.RegularExpressions;
using classifications;

namespace checkuserclassification{

    class Program{

        static void Main(String[] args){


            //These variables are for the file IO. 
            String path = "./email2.txt";
            String s;
            String header = "";
            String footer = "";

            //These are the regexes used to grab each string in the classification banner.

            //Regex1 will grab the text at the beginning of the banner. EX: TOP SECRET
            Regex regex1 = new Regex("^([^/]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            //regex2setup will grab the text between the two double slashes, but will also grab the slashes themselves
            Regex regex2setup = new Regex("//(.*?)//", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            //regex2 will use the match if regex2setup to exclude the double slashes.
            Regex regex2 = new Regex("([^/].*[^/])", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            //regex3 will be used to grab TALENT KEYHOLE if necessary
            Regex regex3 = new Regex("([^/]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            //regex4 will be used to grab the last piece of the classification banner
            Regex regex4 = new Regex("([^/]+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            //These will be the string variables that will contain each classification marking
            String sect1 = "";
            String sect4 = "";
            String nof = "";
            String orcon = "";
            String tk = "";

            //This will be used to track which line is being read
            int line = 0;

            //This section is where the file is going to be read
            using (StreamReader sr = File.OpenText(path))
            {


                while ((s = sr.ReadLine()) != null)
                {

            //This if statement will check if the line number is 4, which is where the header is located
                    if (line == 4)
                    {
                        header = s;

                    }

            //footer will eventually get assigned with the footer classification banner since the footer is the very last line of the file
                    footer = s;
                    ++line;
                }
            }

            //If the header and footer match after the file is read, then the program will proceed to validate each classification.
            if (header.Equals(footer) == true)
            {

            //This section of code will assign sect1 with the match found after using regex1
                MatchCollection section1 = regex1.Matches(header);

                foreach (Match match in section1)
                {
                    sect1 = match.Value;
                }




            //This is where regex2setup will match the middle piece of the classification banner
                MatchCollection section2setup = regex2setup.Matches(header);

                foreach (Match setup in section2setup)
                {

                    //This is where sect4 and tk will be assigned with their respective classification marking
                    MatchCollection section2 = regex2.Matches(setup.Value);

                    foreach (Match match in section2)
                    {
                    
                        //This is where sect4 will be assigned with the classification marking
                        MatchCollection section2a = regex1.Matches(match.Value);

                        foreach (Match match1a in section2a)
                        {
                            sect4 = match1a.Value;
                        }


                        //This is where tk will be assigned with TALENT KEYHOLE if it exists in the banner
                        MatchCollection section2b = regex3.Matches(match.Value);

                        foreach (Match match1b in section2b)
                        {
                            tk = match1b.Value;
                        }

                    }

                }





            //This is where nof and orcon will be assigned with the last piece of the classification banner.
            //during validation, the program will check if either NOFORN or ORCON exists in the classification banner.
                MatchCollection lsection = regex4.Matches(header);

                foreach (Match match in lsection)
                {
                    nof = match.Value;
                    orcon = match.Value;
                }


            //This is where we begin the validation process
            SectionOne obj1 = new SectionOne();
            SectionFour obj2 = new SectionFour();

            int stat1;
            int stat2;

            string result1;
            string result2;

            //stat1 and stat2 will call the method validate from the classes SectionOne and SectionFour
            stat1 = obj1.validate(sect1);
            stat2 = obj2.validate(sect1, sect4, nof, orcon, tk);

            //This switch case statement will check if stat 1 returned a valid result
            switch (stat1)
            {

                case 1:
                    result1 = "status1 returned TOP SECRET";
                    break;

                case 2:
                    result1 = "status1 returned SECRET";
                    break;

                case 3:
                    result1 = "status1 returned CONFIDENTIAL";
                    break;

                case 4:
                    result1 = "status1 returned UNCLASSIFIED";
                    break;

                default:
                    result1 = "invalid classification";
                    break;

            }

            //This switch case statement will check if stat2 returned a valid result. 
            switch (stat2)
            {

                case 1:
                    result2 = "status2 returned HCS//NOFORN";
                    break;

                case 2:
                    result2 = "status2 returned COMINT";
                    break;

                case 3:
                    result2 = "status2 returned COMINT-GAMMA//ORCON";
                    break;

                case 4:
                    result2 = "status2 returned COMINT-ECI";
                    break;

                case 5:
                    result2 = "status2 returned COMINT-GAMMA-ECI//ORCON";
                    break;

                case 6:
                    result2 = "status2 returned COMINT-GAMMA/TALENT KEYHOLE//ORCON";
                    break;

                case 7:
                    result2 = "status2 returned COMINT-ECI/TALENT KEYHOLE";
                    break;

                case 8:
                    result2 = "status2 returned COMINT-G-ECI/TALENT KEYHOLE//ORCON";
                    break;

                default:
                    result2 = "invalid classification";
                    break;

            }


                //These two print statements are simply here to show how the program is working.
                Console.WriteLine("\nHeader is: " + header + "\nFooter is: " + footer + "\nsect1 is: " + sect1 + "\nsect4 is: " + sect4 + "\nTK is: " + tk + "\nnof is: " + nof + "\norcon is: " + orcon);
                Console.WriteLine("\nSECTION1: " + result1 + "\nSECTION2: " + result2+"\n");
            }
            else //This else statement will execute and error message telling the user that the header and footer do not match.
            {


                Console.WriteLine("\nERROR: Classification banner for the header and footer DO NOT match\n");
            }








        }

    }


}