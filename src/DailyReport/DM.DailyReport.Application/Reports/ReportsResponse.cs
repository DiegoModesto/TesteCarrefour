namespace DM.Application.Entries;


/**
 * Notas ao Observador:
 *
 * Repare que na resposta, estou buscando um relatório geral dos registros,
 * um valor total de Entrada (quando o EntryName for Entrada)
 * e um valor total de Saída (quando o EntryName for Saída).
 *
 * Para agilisar e não tomar tanto tempo (tanto de quem cria quanto de quem lê)
 * Optei em deixar simplificado esse relatório, apenas para cadastrar os registros recebido,
 * e gerando um relatório dos registros acumulados.
 *
 * Não estou contemplando um valor SALDO MÊS, ou algo parecido, por precisar ser algo mais
 * elaborado, porém, com essa base já é possível modificar para tal :)
 *
 *
 *
 * Obs.: Cuidado com a leitura dos nomes, deixei no mesmo arquivo para não confundir.
 */
public sealed record ReportsResponse(
    IEnumerable<ReportResponse> Reports,
    decimal TotalIn,
    decimal TotalOut
);

public sealed record ReportResponse(
    Guid Id,
    Guid ExternalId,
    decimal Balance,
    string EntryName,
    DateTime OperationDate
);
