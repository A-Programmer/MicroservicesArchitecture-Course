using PostsService.Dtos;

namespace PostsService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewPost(PostPublishedDto postPublishedDto);
    }
}