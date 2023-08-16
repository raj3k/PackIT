namespace PackIT.Shared.Abstractions.Queries;

public interface IQueryDispatcher
{
    Task<TResult> DispatchQueryAsync<TResult>(IQuery<TResult> query);
}