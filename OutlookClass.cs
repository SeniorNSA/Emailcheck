using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using classifications;
using Microsoft.Office.Interop.Outlook;

namespace nsaEmail
{
    public class OutlookClass
    {

        static Dictionary<string, string> classifications = new Dictionary<string, string>() {
        {"anthony.magana@my.utsa.edu", "Head of Department"},
        {"rj.renteria@my.utsa.edu", "Employee"},

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
        }}
        };


        //This is where each paragraph will be checked.
        static bool CheckAccess(string sender, string receiver, string paragraph)
        {
            string senderClassification = classifications[sender];
            string receiverClassification = classifications[receiver];
            List<string> senderPrivileges = privileges[senderClassification];
            List<string> receiverPrivileges = privileges[receiverClassification];

            foreach (string privilege in senderPrivileges)
            {
                if (paragraph.Contains("(" + privilege + ")"))
                {
                    return receiverPrivileges.Contains(privilege);
                }
            }

            return false;
        }



        public void SendEmail() {

            MailItem mailItem = null;
            
            try {

                String bodText = "";

                //anthony.magana@my.utsa.edu is the sender

                String emailBod= "Sender: anthony.magana@my.utsa.edu\nReceiver: rj.renteria@my.utsa.edu\n--------------------------------------------------------------------------------------------------------------------------------------------------------------------------\nTOP SECRET//COMINT-GAMMA-ECI/TALENT KEYHOLE//ORCON\n---------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n(TS)The aroma of freshly baked bread wafted through the neighborhood, drawing people into the cozy bakery. Warm loaves with crusty exteriors lined the shelves, a testament to the baker's skill and dedication. Each bite was a delightful journey of flavors, evoking memories of simpler times.\n(S)High in the mountains, a solitary cabin stood amidst a sea of pine trees. Smoke curled from its chimney, carrying the comforting scent of a wood-burning stove. In this remote sanctuary, the owner found solace in the quiet of nature, far removed from the chaos of urban life.\n(U)A street artist, armed with vibrant paints and a vision, transformed a drab concrete wall into a mesmerizing masterpiece. Passersby marveled at the colors and creativity, momentarily escaping their daily routines. Art had the power to transcend boundaries and inspire the soul;.\n(C)In a laboratory, scientists worked tirelessly on groundbreaking research, unlocking the mysteries of the universe. The hum of machinery and the scribbling of equations on whiteboards filled the room. With each discovery, humanity took a step closer to understanding the cosmos and its infinite possibilities.\n-------------------------------------------------------------------------------------------------------------------------------------------------------------------------\nTOP SECRET//COMINT-GAMMA-ECI/TALENT KEYHOLE//ORCON";
                string[] lines = emailBod.Split('\n');

                string sender = "";
                string receiver = "";
                string paragraph = "";
                bool hasAccess = true;
                string header = lines[3];
                string footer = lines[lines.Length - 1];




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


                    //This if statement will execute if there was no match found on the last section of the classification banner
                    if (bannClass.Capacity != 3)
                    {
                        bannClass.Add("");

                    }
                    //stat1 and stat2 will call the method validate from the classes SectionOne and SectionFour
                    stat1 = obj1.validate((String)bannClass[0]);
                    stat2 = obj2.validate((String)bannClass[0], (String)bannClass[1], (String)bannClass[3], (String)bannClass[3], (String)bannClass[2]);

                    

                    if (stat1 != 0 && stat2 != 0)
                    {
                        foreach (string line in lines)
                        {
                            if (line.StartsWith("Sender: "))
                            {
                                sender = line.Substring(8);
                            }
                            else if (line.StartsWith("Receiver: "))
                            {
                                receiver = line.Substring(10);
                            }
                            else
                            {
                                paragraph = line;
                                if (paragraph != "")
                                {
                                    hasAccess = CheckAccess(sender, receiver, paragraph);
                                    bodText+=paragraph + " (" + (hasAccess ? "Access granted" : "Access denied") + ")";
                                    bodText+="\n";
                                }
                                else
                                {

                                    bodText+=paragraph;
                                    bodText+="\n";
                                }
                            }

                        }
                        mailItem = GlobalClass.outlookApplication.CreateItem(OlItemType.olMailItem) as MailItem;
                        mailItem.Subject = "First NSA email";
                        mailItem.To = "rj.renteria@my.utsa.edu";
                        mailItem.Body = bodText;
                        mailItem.Send();
                    }
                    else
                    {
                        MessageBox.Show("ERROR: Invalid classifications");

                    }
                    //These two print statements are simply here to show how the program is working.
                    // Console.WriteLine("\nHeader is: " + header + "\nFooter is: " + footer + "\nsect1 is: " + bannClass[0] + "\nsect4 is: " + bannClass[1] + "\nTK is: " + bannClass[2] + "\nnof is: " + bannClass[3] + "\norcon is: " + bannClass[3]);

                }
                else //This else statement will execute and error message telling the user that the header and footer do not match.
                {


                    MessageBox.Show("\nERROR: Classification banner for the header and footer DO NOT match\n");
                }



          

                



            }
            catch (System.Exception ex)
            {

                MessageBox.Show("Exception in SendEmail", ex.Message);

            }
            finally {

                if (mailItem != null)  Marshal.ReleaseComObject(mailItem); 

            }

        }


    }
}
