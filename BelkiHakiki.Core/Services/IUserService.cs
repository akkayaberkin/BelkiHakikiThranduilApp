using BelkiHakiki.Core.DTOs;

namespace BelkiHakiki.Core.Services
{
    public interface IUserService:IService<AppUsers>

    {//  (string username, string token)? Authenticate(string username, string password);

        Task<bool> CheckUser(AppUsers appUsers);
        Task<AppUsers> GetUserInfo(AppUsers appUsers);
        Task<AppUsers> GetByGuidAsync(Guid guid);
        AppUsers GetByUserName(string userName);
        Task<AppUsers> GetByResetGuidAsync(Guid resetGuid);

    }
}
