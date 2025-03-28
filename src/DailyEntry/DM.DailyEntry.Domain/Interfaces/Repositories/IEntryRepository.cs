using DM.Domain.Entities;
using DM.Domain.Interfaces.Repositories.Basis;

namespace DM.Domain.Interfaces.Repositories;

public interface IEntryRepository :
    ICreateRepository<Entry>,
    IReadRepository<Entry>,
    IUpdateRepository<Entry>
//IDeleteRepository<Entry> -> resolvi tirar a opção de exclusão
{
}