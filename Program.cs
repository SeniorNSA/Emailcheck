
using System;
using System.IO;
using System.Collections.Generic;

class EmailClassifier {
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

    static void Main7(string[] args) {
        string filePath = "./email1.txt";
        string[] lines = File.ReadAllLines(filePath);

        string sender = "";
        string receiver = "";
        string paragraph = "";
        bool hasAccess = true;

        foreach (string line in lines) {
            if (line.StartsWith("Sender: ")) {
                sender = line.Substring(8);
            } else if (line.StartsWith("Receiver: ")) {
                receiver = line.Substring(10);
            } else {
                paragraph = line;
                hasAccess = CheckAccess(sender, receiver, paragraph);
                Console.WriteLine(paragraph + " (" + (hasAccess ? "Access granted" : "Access denied") + ")");
            }
        }
    }

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
}

