using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonus_Class
{
        class Customer
        {
            public string Name { get; }

            public Customer(string name)
            {
                Name = name;
            }
        }
        class CheckingAccount : Customer
        {
            public static decimal DailyWithdrawCap { get; } = 300m;

            public decimal Balance { get; private set; }

            private List<Transaction> transactions = new List<Transaction>();
            
            public CheckingAccount(string name) : base(name)
            {

            }
            public void Deposit(decimal amount)
            {
                Balance += amount;
                transactions.Add(new Transaction(TransactionType.Deposit, amount));
            }

            public void Withdraw(decimal amount)
            {
                Balance -= amount;
                transactions.Add(new Transaction(TransactionType.Withdrawal, amount));
            }

            public List<Transaction> GetTransactions()
            {
                return transactions;
            }
        }

        class SavingAccount : Customer
        {
            public static decimal InterestRate { get; } = 0.03m;

            public static decimal PenaltyAmount { get; } = 10m;

            public decimal Balance { get; private set; }

            public List<Transaction> transactions = new List<Transaction>();
            
            public SavingAccount (string name) : base(name)
            {

            }
            public void Deposit(decimal amount)
            {
                Balance += amount;
                transactions.Add(new Transaction(TransactionType.Deposit, amount));
            }

            public void Withdraw(decimal amount)
            {
                Balance -= amount;
                transactions.Add(new Transaction(TransactionType.Withdrawal, amount));
            }

            public decimal CalculateInterest(decimal amount)
            {
                return amount * InterestRate;
            }

            public List<Transaction> GetTransactions()
            {
                return transactions;
            }
        }

        class Transaction
        {
            public DateTime Date { get; }

            public TransactionType Type { get; }

            public decimal Amount { get; }

            public Transaction(TransactionType type, decimal amount)
            {
                Date = DateTime.Today;
                Type = type;
                Amount = amount;
            }
        }

        enum TransactionType
        {
            Deposit,
            Withdrawal
        }
}
