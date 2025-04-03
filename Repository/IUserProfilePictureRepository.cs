using DisCourseW.Models;

namespace DisCourseW.Repository
{
    public interface IUserProfilePictureRepository
    {
        Task<UserProfilePicture> GetProfilePictureByUserIdAsync(string userId);
        Task AddProfilePictureAsync(UserProfilePicture userProfilePicture);
        Task UpdateProfilePictureAsync(UserProfilePicture userProfilePicture);
    }
}
