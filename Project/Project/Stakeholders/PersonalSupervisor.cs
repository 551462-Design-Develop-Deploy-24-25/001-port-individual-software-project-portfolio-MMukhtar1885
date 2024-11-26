using GuidanceHubApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PersonalSupervisor : User
{
    public PersonalSupervisor(Dictionary<string, List<string>> inboxes, string logFilePath)
        : base(inboxes, logFilePath) { }


    public override void DisplayMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Personal Supervisor Menu:");
            Console.WriteLine("1. Send message to Student");
            Console.WriteLine("2. Send message to ST");
            Console.WriteLine("3. Book a meeting with Student");
            Console.WriteLine("4. Check Inbox");
            Console.WriteLine("5. Log Out");
            Console.Write("Enter your choice: ");


            string choice = Console.ReadLine();


            switch (choice)
            {
                case "1":
                    SendMessage("ps", "student");
                    break;
                case "2":
                    SendMessage("ps", "st");
                    break;
                case "3":
                    BookMeeting("ps", "student");
                    break;
                case "4":
                    CheckInbox("ps");
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Utilities.Pause();
                    break;
            }
        }
    }
}