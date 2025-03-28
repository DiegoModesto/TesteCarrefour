using MediatR;

namespace DM.Application.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>{ }
