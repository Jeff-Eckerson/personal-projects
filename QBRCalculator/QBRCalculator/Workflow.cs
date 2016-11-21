using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBRCalculator.BLL;

namespace QBRCalculator
{
    public class Workflow
    {
        public void Start()
        {
            Greet();
            while (true)
            {
                QBRRequest request = TakeInStats();
                QBRResponse response = QBRCalculations.CalculateQBR(request);
                DisplayRating(response);
                bool nextRating = CalculateAnotherRating();
                if(!nextRating)
                {
                    
                    break;
                }
                Console.Clear();
            }
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }

        private QBRRequest TakeInStats()
        {
            QBRRequest request = new QBRRequest();
            StringBuilder statsString = new StringBuilder();
            while (true)
            {
                Console.WriteLine("Enter in a quarterback name:");
                request.Name = Console.ReadLine();
                if (request.Name.Length > 0 && request.Name.Length < 40)
                {
                    statsString.AppendLine($"Name: {request.Name}");
                    Console.Clear();
                    Console.WriteLine(statsString.ToString());
                    break;
                }
                else
                {
                    Console.WriteLine("QB name cannot be blank.");
                }
            }
            while (true)
            {
                Console.WriteLine("How many passes attempted?");
                string userInput = Console.ReadLine();
                bool moveOn = validatePositiveInput("Pass Attempts", userInput);
                if(moveOn == true)
                {
                    statsString.AppendLine($"Pass Attempts: {userInput}");
                    request.PassesAttempted = Int32.Parse(userInput);
                    Console.Clear();
                    Console.WriteLine(statsString.ToString());
                    break;
                }
            }
            
            while (true)
            {
                Console.WriteLine("How many passes completed?");
                string userInput = Console.ReadLine();
                bool moveOn = validatePositiveInput("Pass Completions", userInput);
                if (moveOn == true)
                {
                    statsString.AppendLine($"Pass Completions: {userInput}");
                    request.PassesCompleted = Int32.Parse(userInput);
                    Console.Clear();
                    Console.WriteLine(statsString.ToString());
                    break;
                }
            }

            while (true)
            {
                Console.WriteLine("How many passing yards?");
                string userInput = Console.ReadLine();
                int passYards = 0;
                bool moveOn = Int32.TryParse(userInput, out passYards);
                if (moveOn == true)
                {
                    statsString.AppendLine($"Passing Yards: {passYards}");
                    request.PassingYards = passYards;
                    Console.Clear();
                    Console.WriteLine(statsString.ToString());
                    break;
                }
            }

            while (true)
            {
                Console.WriteLine("How many touchdown passes?");
                string userInput = Console.ReadLine();
                bool moveOn = validatePositiveInput("Touchdown Passes", userInput);
                if (moveOn == true)
                {
                    statsString.AppendLine($"Touchdown Passes: {userInput}");
                    request.TouchdownPasses = Int32.Parse(userInput);
                    Console.Clear();
                    Console.WriteLine(statsString.ToString());
                    break;
                }
            }

            while (true)
            {
                Console.WriteLine("How many interceptions?");
                string userInput = Console.ReadLine();
                bool moveOn = validatePositiveInput("Interceptions", userInput);
                if (moveOn == true)
                {
                    statsString.AppendLine($"Interceptions: {userInput}");
                    request.Interceptions = Int32.Parse(userInput);
                    Console.Clear();
                    Console.WriteLine(statsString.ToString());
                    break;
                }
            }
            return request;
        }

        private bool validatePositiveInput(string inputName, string userInput)
        {
            int result = 0;
            bool validNumber = Int32.TryParse(userInput, out result);
            if(validNumber)
            {
                if(result == 0 && inputName == "Interceptions")
                {
                    return true;
                }
                else if(result <= 0)
                {
                    Console.WriteLine($"{inputName} must be a positive whole number.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if(!validNumber)
            {
                Console.WriteLine($"{inputName} must be a positive whole number.");
                return false;
            }
            return true;
        }

        private void DisplayRating(QBRResponse response)
        {
            if (!response.ValidInput)
            {
                Console.Write("Input Error: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{response.Message}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Results:");
                Console.WriteLine("---------------------------------");
                Console.WriteLine($"{response.Message}");
                Console.WriteLine("---------------------------------");
            }
        }

        private bool CalculateAnotherRating()
        {
            Console.WriteLine();
            Console.WriteLine("Would you like to enter calculate another QB rating? y/n");

            while (true)
            {
                string userInput = Console.ReadLine().ToUpper();
                if (userInput == "Y" || userInput == "YES")
                {
                    return true;
                }
                else if (userInput == "N" || userInput == "NO")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Please enter y or n.");
                }
            }
        }

        private void Greet()
        {
            Console.WriteLine("Welcome to the QBR Calculator.");
            Console.WriteLine();
        }
    }
}
