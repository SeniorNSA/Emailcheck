//First we must state what we will be using from both files
//Both files being "Program.cs" and "checkuserclassification.cs"
//We must find a dependency
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

    //Our "Main" must contain elements from both files
    static void Main(string[] args){

    //This is from "checkuserclassifications.cs"
    string path = "./email2.txt";
    string s;
    string header = "";
    string footer = "";

    //This is from "Program.cs" 
    string filePath = "./email1.txt";
    string[] lines = File.ReadAllLines(filePath);

    string sender = "";
    string receiver = "";
    string paragraph = "";
    bool hasAccess = true;

        

    }




}//This is end of Class "Program"
