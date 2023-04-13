using DbTypes.Interfaces;

namespace DbTypes;

public class BankAccount : IIdentification
{
    public Guid Id { get; }
    public Person Owner { get; }
    public double Balance { get; private set; }
    public bool IsFrozen { get; private set; }

    public BankAccount(Person owner, double balance, bool isFrozen)
    {
        Id = Guid.NewGuid();
        Owner = owner;
        Balance = balance;
        IsFrozen = isFrozen;
    }

    public bool Deposit(double amount)
    {
        if (IsFrozen)
            return false;
        Balance += amount;
        return true;
    }

    public bool Withdraw(double amount)
    {
        return Deposit(-amount);
    }

    public void Freeze()
    {
        IsFrozen = true;
    }

    public void Unfreeze()
    {
        IsFrozen = false;
    }
}