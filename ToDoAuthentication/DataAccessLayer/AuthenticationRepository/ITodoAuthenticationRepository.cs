namespace ToDoAuthentication.DataAccessLayer.AuthenticationRepository
{
    public interface ITodoAuthenticationRepository
    {
        Task<String> getUserIdAsync();
    }
}
