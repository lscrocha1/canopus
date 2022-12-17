using System.Diagnostics.CodeAnalysis;

namespace Canopus.API.Responses;

[ExcludeFromCodeCoverage]
public class MultipleResponse<T> where T : class
{
    public MultipleResponse(int totalItems, int pageIndex, int pageSize, IList<T> items)
    {
        Data = new MultipleDataResponse<T>(totalItems, pageIndex, pageSize, items);
    }

    public MultipleDataResponse<T> Data { get; }
}