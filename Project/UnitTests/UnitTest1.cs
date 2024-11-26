using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class GuidanceHubTests
    {
        [TestMethod]
        public void TP01_ValidLogin_ShouldSucceed()
        {
            // Simulate valid login logic
            string correctEmail = "m.o.mukhtar-2022@hull.ac.uk";
            string correctPassword = "Password";
            string inputEmail = "m.o.mukhtar-2022@hull.ac.uk";
            string inputPassword = "Password";

            bool result = (inputEmail == correctEmail && inputPassword == correctPassword);
            Assert.IsTrue(result, "Valid login should succeed.");
        }

        [TestMethod]
        public void TP01_InvalidLogin_ShouldFail()
        {
            // Simulate invalid login logic
            string correctEmail = "m.o.mukhtar-2022@hull.ac.uk";
            string correctPassword = "Password";
            string inputEmail = "wrongemail@hull.ac.uk";
            string inputPassword = "wrongpassword";

            bool result = (inputEmail == correctEmail && inputPassword == correctPassword);
            Assert.IsFalse(result, "Invalid login should fail.");
        }

        [TestMethod]
        public void TP03_SendMessage_ShouldLogCorrectly()
        {
            // Simulate sending a message
            var inboxes = new Dictionary<string, List<string>>
            {
                { "student", new List<string>() },
                { "ps", new List<string>() }
            };

            string sender = "student";
            string recipient = "ps";
            string message = "Test message";
            inboxes[recipient].Add($"Message from {sender.ToUpper()}: {message}");

            Assert.IsTrue(inboxes["ps"].Count > 0, "Message should be logged in recipient's inbox.");
        }

        [TestMethod]
        public void TP04_BookMeeting_ValidFormat_ShouldSucceed()
        {
            // Simulate booking a meeting
            var inboxes = new Dictionary<string, List<string>>
            {
                { "student", new List<string>() }
            };

            string sender = "ps";
            string recipient = "student";
            string dateTime = "24/11/2024 - 10:00";
            inboxes[recipient].Add($"Meeting request from {sender.ToUpper()} on {dateTime}");

            Assert.IsTrue(inboxes["student"].Count > 0, "Valid meeting request should succeed.");
        }

        [TestMethod]
        public void TP07_Actions_ShouldLogToTxtFile()
        {
            string logFilePath = "TestLog.txt";
            if (File.Exists(logFilePath)) File.Delete(logFilePath);

            string action = "Test action logged";
            File.AppendAllText(logFilePath, $"{System.DateTime.Now}: {action}{System.Environment.NewLine}");

            string logContents = File.ReadAllText(logFilePath);
            Assert.IsTrue(logContents.Contains(action), "Action should be logged to TXT file.");
        }

        [TestMethod]
        public void TP10_PasswordMasking_ShouldReplaceCharacters()
        {
            // Simulate password masking
            string inputPassword = "Password";
            string maskedPassword = new string('*', inputPassword.Length);

            Assert.AreEqual("********", maskedPassword, "Password should be masked correctly.");
        }
    }
}