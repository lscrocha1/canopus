using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Responses;

[ExcludeFromCodeCoverage]
public class MultipleResponse<T> where T : class
{
    public MultipleResponse(MultipleDataResponse<T> data)
    {
        Data = data;
    }

    public MultipleDataResponse<T> Data { get; }
}