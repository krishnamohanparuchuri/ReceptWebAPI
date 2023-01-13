using ReceptWebAPI.Models.Domain;
using ReceptWebAPI.Models.DTO;

namespace ReceptWebAPI.Repository.Interfaces
{
    public interface IReceptRepo
    {
        public List<ReceptResponseDto> GetAllRecepts();

        public List<ReceptResponseDto> GetAvailableReceptsByCustomerId(int customerId);

        public Recept GetIndividualReceptById(int receptId);
        public ReceptStatusResponseDto InsertReceptByCustomerId(ReceptInputInsertDto receptInputDto,int customerId);

        public ReceptStatusResponseDto UpdateReceptById(ReceptUpdateDto updateinfo, int customerId,int receptId);

        public string DeleteReceptById(int customerId, int receptId);

        public string SetRatingValue(int customerId, int receptId, int ratingValue);

        public ReceptResponseDto GetReceptByTitle(string title);
    }
}
