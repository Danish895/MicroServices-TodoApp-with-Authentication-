using ToDoAuthentication.DataAccessLayer.AuthenticationContext;

namespace ToDoAuthentication.DataAccessLayer.AuthenticationRepository
{
    public class TodoAuthenticationRepository : ITodoAuthenticationRepository
    {
        private AuthenticationDbContext _context;

        public TodoAuthenticationRepository(AuthenticationDbContext context)
        {
            _context = context;
        }

        public async Task<string> getUserIdAsync()
        {
            return "guhkj";
        }
    }
}
