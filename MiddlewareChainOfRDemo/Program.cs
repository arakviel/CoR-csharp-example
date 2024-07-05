namespace MiddlewareChainOfRDemo;

internal class Program
{
    private static IUserRepository _userRepository;

    private static void Init()
    {
        _userRepository = new UserRepository();
        _userRepository.Register("admin@example.com", "admin_pass");
        _userRepository.Register("user@example.com", "user_pass");

        // All checks are linked. Client can build various chains using the same
        // components.
        Middleware middleware = Middleware.Link(
            new ThrottlingMiddleware(2),
            new UserExistsMiddleware(_userRepository),
            new RoleCheckMiddleware()
        );

        // Server gets a chain from client code.
        _userRepository.SetMiddleware(middleware);
    }

    static void Main(string[] args)
    {
        Init();

        bool success;
        do
        {
            Console.WriteLine("Enter email: ");
            string? email = Console.ReadLine();
            Console.WriteLine("Input password: ");
            string? password = Console.ReadLine();
            success = _userRepository.LogIn(email, password);
        } while (!success);
    }
}
