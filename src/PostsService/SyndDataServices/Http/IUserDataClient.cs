using System.Threading.Tasks;
using PostsService.Dtos;

namespace PostsService.SyncDataServices
{
    public interface IUserDataClient
    {
        Task SendPostToUsers(PostReadDto post);
    }
}