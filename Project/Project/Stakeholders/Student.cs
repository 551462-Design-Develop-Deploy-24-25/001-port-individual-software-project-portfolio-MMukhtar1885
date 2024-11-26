using GuidanceHubApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Student : User
{
    public Student(Dictionary<string, List<string>> inboxes, string logFilePath)
        : base(inboxes, logFilePath) { }


    public override void DisplayMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Student Menu:");
            Console.WriteLine("1. Send message to PS");
            Console.WriteLine("2. Send message to ST");
            Console.WriteLine("3. Book a meeting with PS");
            Console.WriteLine("4. Check Inbox");
            Console.WriteLine("5. Log Out");
            Console.Write("Enter your choice: ");


            string choice = Console.ReadLine();


            switch (choice)
            {
                case "1":
                    SendMessage("student", "ps");
                    break;
                case "2":
                    SendMessage("student", "st");
                    break;
                case "3":
                    BookMeeting("student", "ps");
                    break;
                case "4":
                    CheckInbox("student");
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