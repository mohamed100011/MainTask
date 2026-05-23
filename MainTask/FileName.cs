using System;
using System.Collections.Generic;

// ============================================================
//  BASE CLASS
// ============================================================
public class Account
{
    public string Name { get; set; }
    public double Balance { get; set; }

    public Account(string name = "Unnamed Account", double balance = 0.0)
    {
        Name = name;
        Balance = balance;
    }

    public virtual bool Deposit(double amount)
    {
        if (amount < 0) return false;
        Balance += amount;
        return true;
    }

    public virtual bool Withdraw(double amount)
    {
        if (Balance - amount < 0) return false;
        Balance -= amount;
        return true;
    }

    public override string ToString() =>
        $"[{GetType().Name}] {Name} : Balance = {Balance:F2}";
}

public class SavingsAccount : Account
{
    public double InterestRate { get; set; }

    public SavingsAccount(string name = "Unnamed Account",
                          double balance = 0.0,
                          double interestRate = 0.0)
        : base(name, balance)
    {
        InterestRate = interestRate;
    }

    public override bool Deposit(double amount)
    {
        if (!base.Deposit(amount)) return false;
        if (amount >= 1000)
            Balance += amount * InterestRate / 100.0;
        return true;
    }
}

public class CheckingAccount : Account
{
    private const double WithdrawFee = 1.50;

    public CheckingAccount(string name = "Unnamed Account", double balance = 0.0)
        : base(name, balance) { }

    public override bool Withdraw(double amount)
    {
        double total = amount + WithdrawFee;
        if (Balance - total < 0) return false;
        Balance -= total;
        return true;
    }
}

public class TrustAccount : SavingsAccount
{
    private const double DepositBonusThreshold = 5000.0;
    private const double DepositBonus = 50.0;
    private const int MaxWithdrawals = 3;
    private const double MaxWithdrawPercent = 0.20;

    private int withdrawCount = 0;

    public TrustAccount(string name = "Unnamed Account",
                        double balance = 0.0,
                        double interestRate = 0.0)
        : base(name, balance, interestRate) { }

    public override bool Deposit(double amount)
    {
        if (!base.Deposit(amount)) return false;
        if (amount >= DepositBonusThreshold)
            Balance += DepositBonus;
        return true;
    }

    public override bool Withdraw(double amount)
    {
        if (withdrawCount >= MaxWithdrawals) return false;
        if (amount > Balance * MaxWithdrawPercent) return false;
        if (!base.Withdraw(amount)) return false;
        withdrawCount++;
        return true;
    }
}

public class AccountUtil
{
    public static void Deposit(List<Account> accounts, double amount)
    {
        Console.WriteLine($"\n=== Depositing to {accounts[0].GetType().Name}s =================================");
        foreach (var acc in accounts)
        {
            if (acc.Deposit(amount))
                Console.WriteLine($"Deposited {amount} to {acc}");
            else
                Console.WriteLine($"Failed Deposit of {amount} to {acc}");
        }
    }

    public static void Withdraw(List<Account> accounts, double amount)
    {
        Console.WriteLine($"\n=== Withdrawing from {accounts[0].GetType().Name}s ==============================");
        foreach (var acc in accounts)
        {
            if (acc.Withdraw(amount))
                Console.WriteLine($"Withdrew {amount} from {acc}");
            else
                Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
        }
    }
}

class Program
{
    static void Main()
    {
        // Accounts
        var accounts = new List<Account>
        {
            new Account(),
            new Account("Larry"),
            new Account("Moe", 2000),
            new Account("Curly", 5000)
        };
        AccountUtil.Deposit(accounts, 1000);
        AccountUtil.Withdraw(accounts, 2000);

        // Savings
        var savAccounts = new List<Account>
        {
            new SavingsAccount(),
            new SavingsAccount("Superman"),
            new SavingsAccount("Batman", 2000),
            new SavingsAccount("Wonderwoman", 5000, 5.0)
        };
        AccountUtil.Deposit(savAccounts, 1000);
        AccountUtil.Withdraw(savAccounts, 2000);

        // Checking
        var checAccounts = new List<Account>
        {
            new CheckingAccount(),
            new CheckingAccount("Larry2"),
            new CheckingAccount("Moe2", 2000),
            new CheckingAccount("Curly2", 5000)
        };
        AccountUtil.Deposit(checAccounts, 1000);
        AccountUtil.Withdraw(checAccounts, 2000);
        AccountUtil.Withdraw(checAccounts, 2000);

        // Trust
        var trustAccounts = new List<Account>
        {
            new TrustAccount(),
            new TrustAccount("Superman2"),
            new TrustAccount("Batman2", 2000),
            new TrustAccount("Wonderwoman2", 5000, 5.0)
        };
        AccountUtil.Deposit(trustAccounts, 1000);
        AccountUtil.Deposit(trustAccounts, 6000);
        AccountUtil.Withdraw(trustAccounts, 2000);
        AccountUtil.Withdraw(trustAccounts, 3000);
        AccountUtil.Withdraw(trustAccounts, 500);

        Console.WriteLine();
    }
}