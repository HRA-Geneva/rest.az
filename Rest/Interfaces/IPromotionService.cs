using Rest.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest.Interfaces
{
    public interface IPromotionService
    {
        Task<List<PromotionDto>> GetCurrentPromotions();
    }
}
