
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SM.Club.Dtos;
using SM.Club.Domain;
using SM.Club.Dtos.ClubTeamDtos;

namespace SM.Club.ApplicationService.Module.ClubModule.Abtracts
{
    public interface IClubTeamService
    {
        public Task<ClubResponeDto> CreateClubTeam(CreateClubTeamDto createClubTeamDto);
        public Task<ClubResponeDto> UpdateClubTeam(int CLubId , UpdateClubTeamDto updateClubTeamDto);
        public Task<ClubResponeDto> RemoveClubTeam(int clubTeamId);
        public ValueTask<ClubResponeDto> GetAllClubTeam();
        public ValueTask<ClubResponeDto> GetClubTeamById(int clubTeamId);
    }
}
