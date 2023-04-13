using System.Collections.Concurrent;
using DbTypes.Interfaces;

namespace DbTypes;

public class BankAccount : IIdentification
{
    public static readonly ConcurrentDictionary<Guid, BankAccount> BankAccountOwnerMap = new();

    public Guid Id { get; }
    public Guid PersonOwner { get; }
    public double Balance { get; private set; }
    public bool Frozen { get; private set; }

    public BankAccount(Guid personOwner, double balance, bool frozen)
    {
        Id = Guid.NewGuid();
        PersonOwner = personOwner;
        Balance = balance;
        Frozen = frozen;
        BankAccountOwnerMap.TryAdd(personOwner, this);
    }

    public bool Deposit(double amount)
    {
        if (Frozen)
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
        Frozen = true;
    }

    public void Unfreeze()
    {
        Frozen = false;
    }
}