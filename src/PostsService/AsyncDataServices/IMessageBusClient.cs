using PostsService.Dtos;

namespace PostsServices.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewPost(PostPublishedDto postPublishedDto);
    }
}