
namespace UsersService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string ev);
    }
}