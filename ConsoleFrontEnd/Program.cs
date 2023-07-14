namespace ConsoleFrontEnd
{
    internal class Program
    {
        private static ApiClient apiClient = new ApiClient("http://localhost:61729");

        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the Account Management Console");

            await Authenticate();
        }

        private static async Task Authenticate()
        {         
            bool authenticated = false;
            
            while (!authenticated)
            {
                string userName = GetUsername();
                int pinCode = GetPinCode();

                authenticated = await apiClient.Authenticate(userName, pinCode);
                if (!authenticated)
                {
                    Console.WriteLine("Authentication failed, please try again.");
                }
            }   
        }

        private static string GetUsername()
        {
            string? userName;

            while (true)
            {
                Console.WriteLine("Enter your username:");
                userName = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(userName))
                {
                    break;
                }

                Console.WriteLine("Invalid username, please try again.");
            }

            return userName;
        }

        private static int GetPinCode()
        {
            int pinCode;
            
            while (true)
            {
                Console.WriteLine("Enter your pin code:");

                if (int.TryParse(Console.ReadLine(), out pinCode))
                {
                    break;
                }

                Console.WriteLine("Invalid pin code, please try again.");
            }

            return pinCode;
        }
    }
}