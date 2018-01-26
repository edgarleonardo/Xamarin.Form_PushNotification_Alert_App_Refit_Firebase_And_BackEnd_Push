using Firebase.MessagePush.Models;
using Refit;
using System.Threading.Tasks;

namespace Firebase.MessagePush.WepApiDefinition
{
    public interface IHttpAccessApi
    {
        [Post("/api/values")]
        Task SendAlert([Body] Alert alert);
    }
}
