using System;
using System.Collections.Generic;

namespace SecurityClassifications
{
    class Program
    {
        static void Main1(String[] args)
        {
            Dictionary<string, string> emailClass = new Dictionary<string, string>();
            Dictionary<string, string> employeeRoles = new Dictionary<string, string>
            {
                {"NSAProgramTester1@outlook.com", "Head of Department"},
                {"NSAProgramTester2@outlook.com", "Employee"},
                {"NSAProgramTester3@outlook.com", "Intern"}
            };

            // Populate emailClass dictionary based on roles
            foreach (var entry in employeeRoles)
            {
                if (entry.Value.Equals("Head of Department", StringComparison.OrdinalIgnoreCase))
                {
                    emailClass[entry.Key] = "TOP SECRET, SECRET, CONFIDENTIAL, UNCLASSIFIED, HCS, COMINT, -GAMMA, -ECI, TALENT KEYHOLE, TS, S, C , U, HCS, SI, -G, -ECI XXX, TK";
                }
                else if (entry.Value.Equals("Employee", StringComparison.OrdinalIgnoreCase))
                {
                    emailClass[entry.Key] = "SECRET, CONFIDENTIAL, UNCLASSIFIED, HCS, COMINT, TALENT KEYHOLE, C, U, HCS, SI, TK";
                }
                else if (entry.Value.Equals("Intern", StringComparison.OrdinalIgnoreCase))
                {
                    emailClass[entry.Key] = "CONFIDENTIAL, UNCLASSIFIED, HCS, COMINT, C, U, HCS, SI";
                }
            }

            string line;
            while ((line = Console.ReadLine()) != null)
            {
                // Check for the delimiter line
                if (line.Contains("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------"))
                {
                    break;
                }

                // Extract sender and receiver from the line
                var parts = line.Split(new string[] { "Sender:", "Receiver:" }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2) continue; // Skip if the format is incorrect

                string senderEmail = parts[0].Trim();
                string receiverEmail = parts[1].Trim();

                // Determine the classification based on the sender's email
                string classification = emailClass.TryGetValue(senderEmail, out var value) ? value : "Unknown";

                Console.WriteLine($"Sender: {senderEmail} | Receiver: {receiverEmail} | Classification: {classification}");
            }
        }
    }
}