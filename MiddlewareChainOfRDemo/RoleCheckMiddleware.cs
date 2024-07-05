namespace MiddlewareChainOfRDemo;

/**
 * ConcreteHandler. Checks a user's role.
 */
public class RoleCheckMiddleware : Middleware
{
    public override bool Check(string email, string password)
    {
        if (email == "admin@example.com")
        {
            Console.WriteLine("Hello, admin!");
            return true;
        }
        Console.WriteLine("Hello, user!");
        return CheckNext(email, password);
    }
}