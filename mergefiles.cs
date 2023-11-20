//First we must state what we will be using from both files
//Both files being "Program.cs" and "checkuserclassification.cs"
//We must find a dependency
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections;
//for now we will just use classifications.cs separately
using classifications;

//now we move on to the program


class Program{
//We must initialize the Dictionary here:
static Dictionary<string, string> classifications = new Dictionary<string, string>() {
        {"NSAProgramTester1@gmail.com", "Head of Department"},
        {"NSAProgramTester2@gmail.com", "Employee"},
        {"NSAProgramTester3@gmail.com", "Intern"}
    };

    static Dictionary<string, List<string>> privileges = new Dictionary<string, List<string>>() {
        {"Head of Department", new List<string>() {
            "TOP SECRET", "SECRET", "CONFIDENTIAL", "UNCLASSIFIED", "HCS", "COMINT",
            "-GAMMA", "-ECI", "TALENT KEYHOLE", "TS", "S", "C", "U", "HCS", "SI",
            "-G", "-ECI XXX", "TK"
        }},
        {"Employee", new List<string>() {
            "SECRET", "CONFIDENTIAL", "UNCLASSIFIED", "HCS", "COMINT", "TALENT KEYHOLE",
            "C", "U", "HCS", "SI", "TK"
        }},
        {"Intern", new List<string>() {
            "CONFIDENTIAL", "UNCLASSIFIED", "HCS", "COMINT", "C", "U", "HCS", "SI"
        }}
    };



static bool CheckAccess(string sender, string receiver, string paragraph) {
        string senderClassification = classifications[sender];
        string receiverClassification = classifications[receiver];
        List<string> senderPrivileges = privileges[senderClassification];
        List<string> receiverPrivileges = privileges[receiverClassification];

        foreach (string privilege in senderPrivileges) {
            if (paragraph.Contains("(" + privilege + ")")) {
                return receiverPrivileges.Contains(privilege);
            }
        }

        return false;
    }
    //Our "Main" must contain elements from both files
    static void Main(string[] args){

    //This is from "checkuserclassifications.cs"
    string path = "./email2.txt";
    string s;
    

    //This is from "Program.cs" 
    string filePath = "./email2.txt";
    string[] lines = File.ReadAllLines(filePath);

    string sender = "";
    string receiver = "";
    string paragraph = "";
    bool hasAccess = true;
    string header =lines[4];
    string footer = lines[lines.Length-1];
//These lines of code are from checkuserclassifications.cs.

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

            //This ArrayList will contain every classification collected from the banner.
            ArrayList bannClass = new ArrayList();




        if (header.Equals(footer) == true)
            {

            //This section of code will assign sect1 with the match found after using regex1
                MatchCollection section1 = regex1.Matches(header);

                foreach (Match match in section1)
                {
                    bannClass.Add(match.Value);
                   
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
                            bannClass.Add(match1a.Value);
                            
                        }


                        //This is where tk will be assigned with TALENT KEYHOLE if it exists in the banner
                        MatchCollection section2b = regex4.Matches(match.Value);

                        foreach (Match match1b in section2b)
                        {
                            
                            bannClass.Add(match1b.Value);
                         
                        }

                    }

                }





            //This is where nof and orcon will be assigned with the last piece of the classification banner.
            //during validation, the program will check if either NOFORN or ORCON exists in the classification banner.
                MatchCollection lsection = regex4.Matches(header);

                foreach (Match match in lsection)
                {
                    
                    bannClass.Add(match.Value);
                    
                }


            //This is where we begin the validation process
            SectionOne obj1 = new SectionOne();
            SectionFour obj2 = new SectionFour();

            int stat1;
            int stat2;

            string result1;
            string result2;

            //This if statement will execute if there was no match found on the last section of the classification banner
            if(bannClass.Capacity != 3){
                bannClass.Add("");

            }
            //stat1 and stat2 will call the method validate from the classes SectionOne and SectionFour
            stat1 = obj1.validate((String)bannClass[0]);
            stat2 = obj2.validate((String)bannClass[0], (String)bannClass[1], (String)bannClass[3], (String)bannClass[3], (String)bannClass[2]);

            //This switch case statement will check if stat 1 returned a valid result
            /*switch (stat1)
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

            }*/

            if(stat1 != 0 && stat2!=0){
                foreach (string line in lines) {
            if (line.StartsWith("Sender: ")) {
                sender = line.Substring(8);
            } else if (line.StartsWith("Receiver: ")) {
                receiver = line.Substring(10);
            } else {
                paragraph = line;
                if(paragraph != ""){
                hasAccess = CheckAccess(sender, receiver, paragraph);
                Console.WriteLine(paragraph + " (" + (hasAccess ? "Access granted" : "Access denied") + ")");
                }else{

                    Console.WriteLine(paragraph);
                }            
            }
                
        }
     }else{
        Console.WriteLine("ERROR: Invalid classifications");

        }
                //These two print statements are simply here to show how the program is working.
               // Console.WriteLine("\nHeader is: " + header + "\nFooter is: " + footer + "\nsect1 is: " + bannClass[0] + "\nsect4 is: " + bannClass[1] + "\nTK is: " + bannClass[2] + "\nnof is: " + bannClass[3] + "\norcon is: " + bannClass[3]);
                
            }
            else //This else statement will execute and error message telling the user that the header and footer do not match.
            {


                Console.WriteLine("\nERROR: Classification banner for the header and footer DO NOT match\n");
            }
        

    }




}//This is end of Class "Program"
