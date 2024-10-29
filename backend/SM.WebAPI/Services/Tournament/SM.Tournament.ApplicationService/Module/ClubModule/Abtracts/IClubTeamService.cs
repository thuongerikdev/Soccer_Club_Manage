using SM.Tournament.Domain.Club;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Tournament.Dtos.ClubTeamDtos;
using SM.Tournament.Dtos;

namespace SM.Tournament.ApplicationService.Module.ClubModule.Abtracts
{
    public interface IClubTeamService
    {
        public Task<TournamentResponeDto> CreateClubTeam(CreateClubTeamDto createClubTeamDto);
        public Task<TournamentResponeDto> UpdateClubTeam(UpdateClubTeamDto updateClubTeamDto);
        public Task<TournamentResponeDto> RemoveClubTeam(int clubTeamId);
        public ValueTask<IEnumerable<ClubTeam>> GetAllClubTeam();
        public ValueTask<TournamentResponeDto> GetClubTeamById(int clubTeamId);
    }
}
