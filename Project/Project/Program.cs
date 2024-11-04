
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Who are you?");
        Console.WriteLine("1. Student");
        Console.WriteLine("2. Personal Supervisor");
        Console.WriteLine("3. Senior Tutor");

        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Student student = new Student();
              
                break;
            case "2":
                PersonalSupervisor supervisor = new PersonalSupervisor();
               
                break;
            case "3":
                SeniorTutor tutor = new SeniorTutor();
              
                break;
            default:
                Console.WriteLine("Invalid selection.");
                break;
        }
    }
}