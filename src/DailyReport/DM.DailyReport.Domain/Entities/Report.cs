using DM.SharedKernel.Enums;

namespace DM.Domain.Entities;

public sealed class Report
{
    /**
     * Nota do Observador:
     *
     * Utilizo ExternalId para receber o ID externo do usuário cadastrado
     * em outra API, com isso, tenho rastreio de todo o cadastro.
     *
     * Porém, o ideia é haver Cadastro de Tenant, CorrelationId e registros dos Logs.
     *
     * Também faço o cadastro da data do registro recebido no RegisterDate, com isso
     * eu separo quando foi notificado e quando foi registrado
     */
    
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid ExternalId { get; private set; } = Guid.Empty;
    public DateTimeOffset RegisterDate { get; init; }
    
    public decimal Balance { get; private set; }
    //# Tipo de entrada:
    // 1- Entrada (+)
    // 2- Saída (-)
    public string EntryName { get; private set; }

    public DateTimeOffset CreatedDate { get; init; } = DateTimeOffset.Now;
    
    public static Report Create(Guid externalId, decimal balance, int type, DateTimeOffset registerDate)
        => new(externalId, balance, type, registerDate);

    private Report(Guid externalId, decimal balance, int type, DateTimeOffset registerDate)
    {
        var entryType = (EntryType)type;
        
        ExternalId = externalId;
        Balance = balance;
        EntryName = nameof( entryType );
        RegisterDate = registerDate;
    }

    public Report()
    {
        //Apenas para "dar uma volta" no problema de suitetable constructor
    }
}