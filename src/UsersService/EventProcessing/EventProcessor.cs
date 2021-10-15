
using System;
using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using UsersService.Data;
using UsersService.Dtos;
using UsersService.Models;

namespace UsersService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        public void ProcessEvent(string ev)
        {
            var eventType = DetrmineEvent(ev);

            switch(eventType)
            {
                case EventType.PostPublieshed:
                    addPost(ev);
                    break;
                default:
                    break;
            }
        }

        private EventType DetrmineEvent(string notificationMessage)
        {
            Console.WriteLine(" ==> Determining Event ...");

            var eventType = JsonSerializer.Deserialize<PostPublishedDto>(notificationMessage);

            switch(eventType.Event)
            {
                case "Post_Published":
                    Console.WriteLine(" ==> Published Event Detected.");
                    return EventType.PostPublieshed;
                default:
                    Console.WriteLine(" ==> Could not determine the event.");
                    return EventType.Undetermined;
            }
        }
    
        private void addPost(string postPublishedMessage)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IUsersRepo>();
                var postPublishedDto = JsonSerializer.Deserialize<PostPublishedDto>(postPublishedMessage);

                try
                {
                    var post = _mapper.Map<Post>(postPublishedDto);
                    if(!repo.ExternalPostExists(post.ExtenralId))
                    {
                        Console.WriteLine(" ==> Adding new post to DB ...");
                        repo.CreatePost(1 ,post);
                        repo.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine(" ==> We already have this post in DB.");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($" ==> Could not add post to DB {ex.Message}");
                }
            }
        }
    }



    enum EventType
    {
        PostPublieshed,
        Undetermined
    }
}