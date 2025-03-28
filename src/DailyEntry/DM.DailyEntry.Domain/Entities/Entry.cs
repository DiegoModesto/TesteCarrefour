using DM.SharedKernel.Enums;

namespace DM.Domain.Entities;

public sealed class Entry
{
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public decimal Balance { get; private set; }
    //# Tipo de entrada:
    // 1- Entrada (+)
    // 2- Saída (-)
    public EntryType Type { get; private set; } //Se não passar o tipo de entrada, assumirá q seja DÉBITO

    public DateTimeOffset CreatedDate { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedDate { get; init; }
    
    public static Entry Create(decimal balance, int? type)
        => new(balance, type);

    private Entry(decimal balance, int? type)
    {
        Balance = balance;
        Type = (EntryType)(type ?? 1);
    }

    public Entry()
    {
        //Apenas para "dar uma volta" no problema de suitetable constructor
    }
}