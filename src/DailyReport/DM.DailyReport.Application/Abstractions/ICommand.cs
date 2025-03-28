using MediatR;

namespace DM.Application.Abstractions;

public interface ICommand <out TResponse> : IRequest<TResponse>{ }
