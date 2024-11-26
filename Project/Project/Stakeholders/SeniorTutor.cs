using GuidanceHubApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SeniorTutor : User
{
    public SeniorTutor(Dictionary<string, List<string>> inboxes, string logFilePath)
        : base(inboxes, logFilePath) { }


    public override void DisplayMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Senior Tutor Menu:");
            Console.WriteLine("1. Send message to Student");
            Console.WriteLine("2. Send message to PS");
            Console.WriteLine("3. Check Inbox");
            Console.WriteLine("4. View Student Supervision Status");
            Console.WriteLine("5. Log Out");
            Console.Write("Enter your choice: ");


            string choice = Console.ReadLine();


            switch (choice)
            {
                case "1":
                    SendMessage("st", "student");
                    break;
                case "2":
                    SendMessage("st", "ps");
                    break;
                case "3":
                    CheckInbox("st");
                    break;
                case "4":
                    ViewSupervisionStatus();
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


    public void ViewSupervisionStatus()
    {
        Console.WriteLine("Student Supervision Status:");
        foreach (var message in Inboxes["student"])
        {
            Console.WriteLine($"- {message}");
        }
        foreach (var message in Inboxes["ps"])
        {
            Console.WriteLine($"- {message}");
        }
        Utilities.Pause();
    }
}