using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Responses;

[ExcludeFromCodeCoverage]
public class SingleResponse<T> where T : class
{
    public SingleResponse(T? data)
    {
        Data = data;
    }

    public T? Data { get; }
}