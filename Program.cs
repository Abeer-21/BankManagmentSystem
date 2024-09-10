abstract class BankAccount{

    private decimal balance;
    private string? accountHolder;
    private string? accountNumber;
    public static decimal balanceAnmount;

    

   public string? AccountHolder {get{return accountHolder; } set{ accountHolder = value; } }
   public string? AccountNumber {get{return accountNumber; } set{ accountNumber = value; } }
   public decimal Balance {get{return balance; } set{ balance = value; } }

   public BankAccount(string accountNumber , string accountHolder , int balance)
   {
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        Balance = balance;
        
   }
    public abstract void CalculateInterest();
    public virtual void Deposit(decimal amount)
    {
        balanceAnmount = amount;
        balance += amount;

        Console.WriteLine($"Deposited ${amount} to {AccountHolder}'s account");
    }
    public virtual void Withdraw(decimal amount)
    {
        balance -= amount; 
        Console.WriteLine($"Withdrawing ${amount} from {AccountHolder}'s account");
    }
    public virtual void DisplayAccountInfo(){

    Console.WriteLine($"Account Number: {accountNumber}\nAccount Holder: {accountHolder}\nBalance: {balance}");
}


}

class SavingsAccount : BankAccount{

private decimal interestRate;

public decimal InterestRate {get{return interestRate; } set{ interestRate = value; } }
public SavingsAccount(string accountNumber,string accountHolder,int balance, decimal interestRate)
:base(accountNumber, accountHolder,  balance)
{
    InterestRate = interestRate; 
   
}

public override void CalculateInterest()
{
   decimal interestAmount = Balance * InterestRate / 100;
   Deposit(interestAmount);
    Console.WriteLine($"Interest of ${balanceAnmount} added at {InterestRate} rate.");
}


}


class CheckingAccount  : BankAccount{
private int overdraftLimit;
public int OverdraftLimit {get{return overdraftLimit; } set{ overdraftLimit = value; } }

public CheckingAccount(string accountNumber,string accountHolder,int balance, int overdraftLimit)
:base(accountNumber, accountHolder,  balance)
{
    OverdraftLimit = overdraftLimit;
}
public override void CalculateInterest(){

    Console.WriteLine("Checking accounts do not have interest.");
}
public override void Withdraw(decimal amount)
    {
        base.Withdraw(amount);
    }

}

class FixedDepositAccount  : BankAccount{

private int maturityPeriod;
private decimal interestRate;
public int MaturityPeriod  {get{return maturityPeriod; } set{ maturityPeriod = value; } }
public decimal FixedInterestRate  {get{return interestRate; } set{ interestRate = value; } }

public FixedDepositAccount(string accountNumber,string accountHolder,int balance, int maturityPeriod, decimal interestRate)
:base(accountNumber, accountHolder,  balance)
{
    MaturityPeriod = maturityPeriod;
    FixedInterestRate = interestRate;
}

public override void CalculateInterest(){

    decimal fixedInterestAmount = Balance * FixedInterestRate / 100;
    Deposit(fixedInterestAmount);
    Console.WriteLine($"Fixed deposit interest of ${balanceAnmount} calculated at {FixedInterestRate} rate over {MaturityPeriod} months.");
}

}

 // Main program demonstrating Polymorphism
 class Program
 {
     public static void Main()
     {
         // Create instances of SavingsAccount, CheckingAccount, and FixedDepositAccount
         BankAccount savings = new SavingsAccount("SA12345", "Alice", 1000, 3.5m);
         BankAccount checking = new CheckingAccount("CA67890", "Bob", 500, 200);
         BankAccount fixedDeposit = new FixedDepositAccount("FD54321", "Charlie", 10000, 12, 5.0m);
 
         // Demonstrate polymorphism by calling methods on the abstract type
         
         savings.DisplayAccountInfo();
         savings.CalculateInterest();
         savings.DisplayAccountInfo();

 
         Console.WriteLine();
 
         checking.DisplayAccountInfo();
         checking.Withdraw(600);  // Demonstrate overdraft
         checking.CalculateInterest();
 
         Console.WriteLine();
 
         fixedDeposit.DisplayAccountInfo();
         fixedDeposit.CalculateInterest();
 
         // Using an array of BankAccount objects to demonstrate polymorphism

         Console.WriteLine("\nAll accounts and their interests:");
         BankAccount[] accounts = { savings, checking, fixedDeposit };
         foreach (BankAccount account in accounts)
         {
            account.DisplayAccountInfo();
            account.CalculateInterest();
         }
     }
 }