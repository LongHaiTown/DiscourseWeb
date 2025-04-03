using DisCourse.Models;
using DisCourseW.Models;
using Microsoft.EntityFrameworkCore;

namespace DisCourseW.Repository
{
    public class EFUserProfilePictureRepository : IUserProfilePictureRepository
    {
        private readonly ApplicationDbContext _context;

        public EFUserProfilePictureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfilePicture> GetProfilePictureByUserIdAsync(string userId)
        {
            return await _context.UserProfilePictures
                .FirstOrDefaultAsync(up => up.UserId == userId);
        }

        public async Task AddProfilePictureAsync(UserProfilePicture userProfilePicture)
        {
            await _context.UserProfilePictures.AddAsync(userProfilePicture);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProfilePictureAsync(UserProfilePicture userProfilePicture)
        {
            _context.UserProfilePictures.Update(userProfilePicture);
            await _context.SaveChangesAsync();
        }
    }
}
