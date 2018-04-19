using CFlix.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CFlix.Services.Repositories
{
    public interface IMediaRepository
    {
        Task<IEnumerable<Media>> GetAllMediasAsync();
        Task<IEnumerable<Media>> SearchMediaAsync(string query);

        Task<Media> GetMediaWithDetailAsync(int mediaId);
        Task<IEnumerable<Review>> GetMediaReviewsAsync(int mediaId);
        Task AddReviewAsync(int mediaId, string userName, string content);
        Task<Review> GetReviewAsync(int reviewId);
        Task EditReviewAsync(Review review);
    }
}
