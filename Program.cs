using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Transactions;
using Bonus_Class;
using System.Globalization;


namespace BankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set the currency culture to Canadian Dollars 
            CultureInfo canadianCulture = CultureInfo.GetCultureInfo("en-CA");
            
            Console.Write("Enter your name: ");
            string customerName = Console.ReadLine();
            Customer customer = new Customer(customerName);

            
            CheckingAccount checkingAccount = new CheckingAccount(customerName);
            SavingAccount savingAccount = new SavingAccount(customerName);

            decimal totalWithdraw = 0;
                      
                      
            while (true)
            {
                
                Console.WriteLine("\nSelect one of the following activities: ");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Transfer");
                Console.WriteLine("4. Account Activity Enquiry");
                Console.WriteLine("5. Balance Enquiry");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your selection (1 to 6): ");

                
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        
                        Console.Write("Enter account type (1 for Checking, 2 for Saving): ");
                        string accountType = Console.ReadLine().ToUpper();

                        if (accountType == "1")
                        {
                            Console.Write("Enter deposit amount: ");
                            decimal amount = decimal.Parse(Console.ReadLine());
                            checkingAccount.Deposit(amount);
                            Console.WriteLine("Deposit successful.");
                        }
                        else if (accountType == "2")
                        {
                            Console.Write("Enter deposit amount: ");
                            decimal amount = decimal.Parse(Console.ReadLine());
                            decimal interestAmount = savingAccount.CalculateInterest(amount);
                            savingAccount.Deposit(amount + interestAmount);
                            Console.WriteLine("Deposit successful. Interest earned: {0}", interestAmount.ToString("C", canadianCulture));
                        }
                        else
                        {
                            Console.Write("Invalid Option, please start again.");
                        }

                        break;
                    case 2:
                        
                        Console.Write("Enter account type (1 for Checking, 2 for Saving): ");
                        accountType = Console.ReadLine().ToUpper();

                        if (accountType == "1")
                        {
                            Console.Write("Enter withdrawal amount: ");
                            decimal amount = decimal.Parse(Console.ReadLine());
                            
                            if (amount > CheckingAccount.DailyWithdrawCap || totalWithdraw > CheckingAccount.DailyWithdrawCap)
                            {
                                Console.WriteLine("Withdrawal amount exceeds daily withdraw cap of $300.00.");
                            }
                            else if (checkingAccount.Balance < amount)
                            {
                                Console.WriteLine("Insufficient balance.");
                            }
                            else
                            {
                                checkingAccount.Withdraw(amount);
                                Console.WriteLine("Withdrawal successful.");
                                totalWithdraw += amount;
                            }
                        }
                        else if (accountType == "2")
                        {
                            Console.Write("Enter withdrawal amount: ");
                            decimal amount = decimal.Parse(Console.ReadLine());
                            
                            if (savingAccount.Balance < amount + SavingAccount.PenaltyAmount)
                            {
                                Console.WriteLine("Insufficient balance.");
                            }
                            else
                            {
                                savingAccount.Withdraw(amount + SavingAccount.PenaltyAmount);
                                Console.WriteLine("Withdrawal successful. Penalty amount: $10.00");
                            }
                        }
                        else
                        {
                            Console.Write("Invalid Option, please start again.");
                        }

                        break;
                    case 3:
                        
                        Console.Write("Enter from account type (1 for Checking, 2 for Saving): ");
                        string fromAccountType = Console.ReadLine().ToUpper();
                        
                        if (fromAccountType == "1")
                        {
                            Console.Write("Enter transfer amount: ");
                            decimal amount = decimal.Parse(Console.ReadLine());

                            if (checkingAccount.Balance < amount)
                            {
                                Console.WriteLine("Insufficient balance.");
                            }
                            else
                            {
                                checkingAccount.Withdraw(amount);
                                decimal interestAmount = savingAccount.CalculateInterest(amount);
                                savingAccount.Deposit(amount + interestAmount);
                                Console.WriteLine("Transfer successful. Interest earned: {0}", interestAmount.ToString("C", canadianCulture));
                            }
                        }
                        else if (fromAccountType == "2")
                        {
                            Console.Write("Enter transfer amount: ");
                            decimal amount = decimal.Parse(Console.ReadLine());

                            if (savingAccount.Balance < amount + SavingAccount.PenaltyAmount)
                            {
                                Console.WriteLine("Insufficient balance.");
                            }
                            else
                            {
                                savingAccount.Withdraw(amount + SavingAccount.PenaltyAmount);
                                checkingAccount.Deposit(amount);
                                Console.WriteLine("Transfer successful. Penalty: $10.00");
                            }
                        }
                        else
                        {
                            Console.Write("Invalid Option, please start again.");
                        }

                        break;

                    case 4:
                        
                        Console.WriteLine("Checking Account:");
                        List<Bonus_Class.Transaction> checkingTransactions = checkingAccount.GetTransactions();
                        DisplayTransactions(checkingTransactions);
                        Console.WriteLine("\nSaving Account:");
                        List<Bonus_Class.Transaction> savingTransactions = savingAccount.GetTransactions();
                        DisplayTransactions(savingTransactions);
                        break;

                    case 5:
                        
                        Console.WriteLine("Checking Account balance: {0}", checkingAccount.Balance.ToString("C", canadianCulture));
                        Console.WriteLine("Saving Account balance: {0}", savingAccount.Balance.ToString("C", canadianCulture));
                        break;
                    case 6:
                        
                        Console.WriteLine("Thank you for using Algonquin Banking System!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void DisplayTransactions(List<Bonus_Class.Transaction> transactions)
        {
            CultureInfo canadianCulture = CultureInfo.GetCultureInfo("en-CA");
            Console.WriteLine("{0,-15} {1,-15} {2,-15}", "Amount", "Date", "Activity");
            foreach (Bonus_Class.Transaction transaction in transactions)
            {
                Console.WriteLine("{0,-15} {1,-15} {2,-15}", transaction.Amount.ToString("C", canadianCulture), transaction.Date.ToString("yyyy-MM-dd"), transaction.Type);
            }
        }
    }
}



