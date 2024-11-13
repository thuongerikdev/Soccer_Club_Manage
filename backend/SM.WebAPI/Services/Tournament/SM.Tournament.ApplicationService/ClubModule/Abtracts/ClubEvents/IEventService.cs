using SM.Tournament.Dtos;

public interface IEventService
{
    Task<TournamentResponeDto> CreateEvent(object eventDto);
    Task<TournamentResponeDto> UpdateEvent(object eventDto);
    Task<TournamentResponeDto> RemoveEvent(int eventID);
    Task<TournamentResponeDto> GetEventById(int eventID);
    Task<TournamentResponeDto> GetAllEvents();
    Task<TournamentResponeDto> GetEventByClub(int clubID);
}