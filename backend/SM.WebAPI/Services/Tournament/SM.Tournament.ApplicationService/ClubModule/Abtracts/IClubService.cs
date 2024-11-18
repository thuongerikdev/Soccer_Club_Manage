using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.Club;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.ClubModule.Abtracts
{
    public interface IClubService

    {
        public Task<TournamentResponeDto> CreateClubTeam(CreateClubDto createClubTeamDto);
        public Task<TournamentResponeDto> UpdateClubTeam(UpdateClubDto updateClubTeamDto);
        public Task<TournamentResponeDto> RemoveClubTeam(int clubTeamId);
        public ValueTask<TournamentResponeDto> GetAllClubTeam();
        public ValueTask<TournamentResponeDto> GetClubTeamById(int UserID);
        public ValueTask<TournamentResponeDto> GetClubTeamByUserId(int UserID);
        public Task<TournamentResponeDto> FindClubByName(string name);
    }
}
