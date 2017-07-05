using Firebase.SendNotification.Data.Enum;
using Firebase.SendNotification.Data.Repository;
using System;
using System.Threading.Tasks;

namespace Firebase.SendNotification.ConoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PushRepository pushRepo = new PushRepository();
            
            Console.WriteLine("Enter Application Details (e.g. SQx......_D7WI0)");
            string applicationID = Console.ReadLine();
            
            Console.WriteLine("\nEnter Target Details (e.g. /topics/news)");
            string target = Console.ReadLine();
            
            Console.WriteLine("\nEnter Title");
            string title = Console.ReadLine();
            
            Console.WriteLine("\nEnter Body");
            string body = Console.ReadLine();

            FirebaseNotificationType option = FirebaseNotificationType.none;

            do
            {
                Console.WriteLine("\nType of Push Notification");
                Console.WriteLine("n) Notification");
                Console.WriteLine("d) Data");
                string optionString = Console.ReadLine();
                if (optionString.ToLower().Equals("n")) option = FirebaseNotificationType.notification;
                if (optionString.ToLower().Equals("d")) option = FirebaseNotificationType.data;
            }
            while (option == FirebaseNotificationType.none);

            pushRepo.SendToFirebase(applicationID, title, body, target, FirebaseNotificationType.data);
        }
    }
}
