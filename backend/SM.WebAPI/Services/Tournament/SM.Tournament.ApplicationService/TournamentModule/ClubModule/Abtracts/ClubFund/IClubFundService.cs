using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubFund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TournamentModule.ClubModule.Abtracts.ClubFund
{
    public interface IClubFundService
    {
        public Task<TournamentResponeDto> CreateClubFund(CreateFundDto createClubFundDto);
        public Task<TournamentResponeDto> UpdateClubFund(UpdateFundDto updateClubFundDto);
        public Task<TournamentResponeDto> RemoveClubFund(int clubFundId);
        public ValueTask<TournamentResponeDto> GetAllClubFund();
        public ValueTask<TournamentResponeDto> GetClubFundById(int clubID);
    }
}
