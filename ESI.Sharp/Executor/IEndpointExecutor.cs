using System.Threading.Tasks;
using ESI.Sharp.Models;
using RestSharp;

namespace ESI.Sharp.Executor
{
    public interface IEndpointExecutor
    {
        Task<EsiResponse<T>> ExecuteEndpointAsync<T>(RestRequest restRequest);
    }
}