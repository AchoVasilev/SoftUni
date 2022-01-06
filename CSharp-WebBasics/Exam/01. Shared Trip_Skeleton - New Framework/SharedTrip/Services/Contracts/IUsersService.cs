namespace SharedTrip.Services.Contracts
{
    public interface IUsersService
    {
        void Create(string username, string email, string password);

        string GetUserId(string username, string password);

        bool IsUsernameAvailable(string username);

        bool IsEmailAvailable(string email);
    }
}
