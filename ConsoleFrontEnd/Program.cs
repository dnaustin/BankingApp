using ConsoleFrontEnd.Dtos;

namespace ConsoleFrontEnd
{
    internal class Program
    {
        private static ApiClient apiClient = new ApiClient("http://localhost:61729");

        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the Account Management Console");

            await Authenticate();
            await FunctionLoop();

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

        private static async Task FunctionLoop()
        {
            while (true)
            {
                Console.WriteLine("Please enter desired function (type help to list all functions)");
                string? desiredFunction = Console.ReadLine();

                switch (desiredFunction?.ToLower()) 
                {
                    case "create":
                        await CreateNewAccount();
                        continue;
                    case "edit":
                        await EditExistingAccount();
                        continue;
                    case "delete":
                        await DeleteExistingAccount();
                        continue;

                    case "debit":
                        await DebitAnAccount();
                        continue;
                    case "credit":
                        await CreditAnAccount();
                        continue;
                    case "transfer":
                        await TransferBetweenAccounts();
                        continue;
                    case "freeze":
                        await FreezeAnAccount();
                        continue;
                                      
                    case "help":
                        ListAvailableOptions();
                        continue;
                    case "exit":
                        break;
                    
                    default:
                        Console.WriteLine("Could not determine function from input, please try again.");
                        continue;
                }

                break;
            }

        }

        private static async Task FreezeAnAccount()
        {
            int accountNumber = GetUserInputForIntQuestion("Enter account number to freeze:");
            string? response = await apiClient.Put($"api/accounts/{accountNumber}/freeze", null);
            Console.WriteLine(response);
        }

        private static async Task TransferBetweenAccounts()
        {
            int accountNumberToDebit = GetUserInputForIntQuestion("Enter account number to debit:");
            int accountNumberToCredit = GetUserInputForIntQuestion("Enter account number to credit:");
            decimal amount = GetUserInputForDecimalQuestion("Enter amount to transfer:");

            TransactionDto dto = new TransactionDto
            {
                AccountNumberToCredit = accountNumberToCredit,
                AccountNumberToDebit = accountNumberToDebit,
                Amount = amount,
                Type = "transfer"
            };

            string? response = await apiClient.Put($"api/transaction", dto);
            Console.WriteLine(response);
        }

        private static async Task CreditAnAccount()
        {
            int accountNumber = GetUserInputForIntQuestion("Enter account number to credit:");
            decimal amount = GetUserInputForDecimalQuestion("Enter amount to credit:");

            TransactionDto dto = new TransactionDto
            {
                AccountNumberToCredit = accountNumber,
                Amount = amount,
                Type = "credit"
            };

            string? response = await apiClient.Put($"api/transaction", dto);
            Console.WriteLine(response);
        }

        private static async Task DebitAnAccount()
        {
            int accountNumber = GetUserInputForIntQuestion("Enter account number to debit:");
            decimal amount = GetUserInputForDecimalQuestion("Enter amount to debit:");

            TransactionDto dto = new TransactionDto
            {
                AccountNumberToDebit = accountNumber,
                Amount = amount,
                Type = "debit"
            };

            string? response = await apiClient.Put($"api/transaction", dto);
            Console.WriteLine(response);
        }

        private static void ListAvailableOptions()
        {
            Console.WriteLine("The available functions are:");
            Console.WriteLine("create (create a new account),");
            Console.WriteLine("edit (amend an existing account),");
            Console.WriteLine("delete (delete an account),");
            
            Console.WriteLine("debit (debit an account),");
            Console.WriteLine("credit (credit an account),");
            Console.WriteLine("transfer (transfer funds between accounts),");
            Console.WriteLine("freeze (prevent an account from further transactions),");

            Console.WriteLine("help (list all functions)");
            Console.WriteLine("exit (exit the application)");
        }

        private static async Task DeleteExistingAccount()
        {
            int accountNumber = GetUserInputForIntQuestion("Enter account number to delete:");
            string? response = await apiClient.Delete($"api/accounts/{accountNumber}");
            Console.WriteLine(response);
        }

        private static async Task EditExistingAccount()
        {
            int accountNumber = GetUserInputForIntQuestion("Enter account number to edit:");
            string? name = GetUserInputForQuestion("Enter new account holder name:");
            string? response = await apiClient.Put($"api/accounts/{accountNumber}", new { name });
            Console.WriteLine(response);
        }

        private static async Task CreateNewAccount()
        {
            string? name = GetUserInputForQuestion("Enter account holder name:");
            decimal balance = GetUserInputForDecimalQuestion("Enter starting balance:");
            string? type = GetUserInputForQuestion("Enter account type:");

            string? response = await apiClient.Post("api/accounts", new { balance, name, type });
            Console.WriteLine(response);
        }

        private static string? GetUserInputForQuestion(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        private static int GetUserInputForIntQuestion(string question)
        {
            Console.WriteLine(question);

            while (true)
            {
                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int value))
                {
                    Console.WriteLine("Invalid input, please try again.");
                    continue;
                }

                return value;
            }
        }

        private static decimal GetUserInputForDecimalQuestion(string question)
        {
            Console.WriteLine(question);

            while (true)
            {   
                string? input = Console.ReadLine();

                if (!decimal.TryParse(input, out decimal value))
                {
                    Console.WriteLine("Invalid input, please try again.");
                    continue;
                }

                return value;
            }
        }
    }
}