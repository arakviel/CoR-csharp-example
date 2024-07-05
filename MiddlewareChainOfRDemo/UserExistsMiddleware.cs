namespace MiddlewareChainOfRDemo;

/**
 * ConcreteHandler. Checks whether a user with the given credentials exists.
 */
public class UserExistsMiddleware : Middleware
{
    private IUserRepository _userRepository;

    public UserExistsMiddleware(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public override bool Check(string email, string password)
    {
        if (!_userRepository.HasEmail(email))
        {
            Console.WriteLine("This email is not registered!");
            return false;
        }
        if (!_userRepository.IsValidPassword(email, password))
        {
            Console.WriteLine("Wrong password!");
            return false;
        }
        return CheckNext(email, password);
    }
}