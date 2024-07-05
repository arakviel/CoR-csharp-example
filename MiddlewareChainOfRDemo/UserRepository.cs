namespace MiddlewareChainOfRDemo;

public class UserRepository : IUserRepository
{
    private readonly Dictionary<string, string> _users = new Dictionary<string, string>();
    private Middleware _middleware;

    /**
     * Client passes a chain of object to server. This improves flexibility and
     * makes testing the server class easier.
     */
    public void SetMiddleware(Middleware middleware)
    {
        _middleware = middleware;
    }

    /**
     * Server gets email and password from client and sends the authorization
     * request to the chain.
     */
    public bool LogIn(string email, string password)
    {
        if (_middleware.Check(email, password))
        {
            Console.WriteLine("Authorization have been successful!");

            // Do something useful here for authorized users.

            return true;
        }
        return false;
    }

    public void Register(string email, string password)
    {
        _users[email] = password;
    }

    public bool HasEmail(string email)
    {
        return _users.ContainsKey(email);
    }

    public bool IsValidPassword(string email, string password)
    {
        return _users.TryGetValue(email, out string storedPassword) && storedPassword == password;
    }
}