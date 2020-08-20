using System.Threading.Tasks;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Internal;

namespace FiroozehGameService.Utils
{
    internal static class ImageUtil
    {
        internal static async Task<ImageUploadResult> UploadProfileImage(byte[] imageBuffer)
        {
            if (imageBuffer.Length > 1000 * 1024) throw new GameServiceException("ProfileImage is Too Large");
            return await ApiRequest.UploadUserProfileLogo(imageBuffer);
        }
    }
}