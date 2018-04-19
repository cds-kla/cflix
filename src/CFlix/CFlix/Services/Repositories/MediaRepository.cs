using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFlix.Data;
using CFlix.Models;

namespace CFlix.Services.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly CFlixContext _context;
        private readonly CFlixAuthContext _authContext;

        public MediaRepository(CFlixContext context,
            CFlixAuthContext authContext = null)
        {
            _context = context;
            _authContext = authContext;
        }

        public async Task<IEnumerable<Media>> GetAllMediasAsync()
        {
            return await _context.Medias.ToListAsync();
        }

        public async Task<IEnumerable<Media>> SearchMediaAsync(string query)
        {
            return await _context.Medias.FromSql("SELECT Id, ImageUri, Title, Type, YouTubeId, ReleaseDate FROM Medias WHERE Title LIKE '" + query + "'")
                .Select(m => new Media { Id = m.Id, ImageUri = m.ImageUri, Title = m.Title, Type = m.Type }).ToListAsync();
        }

        public async Task<Media> GetMediaWithDetailAsync(int mediaId)
        {
            return await _context.Medias.Include(m => m.Reviews)
                                  .FirstOrDefaultAsync(m => m.Id == mediaId);
        }

        public async Task<IEnumerable<Review>> GetMediaReviewsAsync(int mediaId)
        {
            var reviews = await (from review in _context.Reviews
                                 where review.MediaId == mediaId //&& !review.IsHidden
                                 orderby review.LastUpdated ascending
                                 select review).ToListAsync();
            var arr = reviews.Select(r => r.UserName).Distinct().ToArray();

            var users = await (from user in _authContext.Users
                               where arr.Contains(user.UserName)
                               select user).ToListAsync();

            foreach (var review in reviews)
            {
                review.User = users.FirstOrDefault(u => u.UserName == review.UserName) ??
                    new CFlixUser { UserName = review.UserName };
            }

            return reviews;
        }

        public async Task AddReviewAsync(int mediaId, string userName, string content)
        {
            var review = (await _context.Reviews.FirstOrDefaultAsync(r => r.Media.Id == mediaId && r.UserName == userName)) ??
                new Review { UserName = userName, Media = await _context.Medias.FindAsync(mediaId) };

            if (review.Media == null)
            {
                throw new ArgumentException("Invalid Media", nameof(mediaId));
            }

            if (review.Id == 0)
            {
                _context.Add(review);
            }

            review.Content = content;

            await _context.SaveChangesAsync();
        }

        public async Task<Review> GetReviewAsync(int reviewId)
        {
            return await _context.Reviews.FindAsync(reviewId);
        }

        public async Task EditReviewAsync(Review review)
        {
            _context.Attach(review);
            await _context.SaveChangesAsync();
        }
    }
}
