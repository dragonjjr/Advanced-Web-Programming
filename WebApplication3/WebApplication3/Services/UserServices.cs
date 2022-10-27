using WebApplication3.DBContext;
using WebApplication3.Model;
using WebApplication3.Repository;
using static WebApplication3.Model.General;

namespace WebApplication3.Services
{
    public class UserServices
    {
        private UserRepository userRepository;
        public UserServices()
        {
            userRepository = new UserRepository();
        }

        public string Authenticate(LoginModel VM)
        {
            return userRepository.Authenticate(VM);
        }

        public ResponMessages Login(LoginModel VM)
        {
            return userRepository.Login(VM);
        }

        public ResponMessages RefreshToken(UserRefreshToken user)
        {
            return userRepository.RefreshToken(user);
        }
    }
}
