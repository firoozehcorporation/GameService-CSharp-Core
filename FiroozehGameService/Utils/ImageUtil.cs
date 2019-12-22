using System.Threading.Tasks;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Models.Internal;

namespace FiroozehGameService.Utils
{
    internal class ImageUtil
    {
        internal static async Task<ImageUploadResult> UploadProfileImage(byte[] imageBuffer)
        {
           return await ApiRequest.UploadUserProfileLogo(imageBuffer);
        }
    }
}