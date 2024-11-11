using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubEvent.CelebrateEvent;
using SM.Tournament.Dtos.ClubDto.ClubEvent.ClubEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TournamentModule.ClubModule.Abtracts.ClubEvents
{
    public interface IClubEventService
    {
        public  Task<TournamentResponeDto> CreateEvent(CreateEventDto createEventDto);
        public Task<TournamentResponeDto> UpdateEvent(UpdateEventDto updateEventDto);
        public Task<TournamentResponeDto> RemoveEvent(int eventId);
        public Task <TournamentResponeDto> GetAllEvents();
        public Task <TournamentResponeDto> GetEventById(int eventId);
        
    }
}
