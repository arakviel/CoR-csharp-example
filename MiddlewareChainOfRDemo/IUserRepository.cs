namespace MiddlewareChainOfRDemo;

public interface IUserRepository
{
    bool HasEmail(string email);
    bool IsValidPassword(string email, string password);
    bool LogIn(string? email, string? password);
    void Register(string email, string password);
    void SetMiddleware(Middleware middleware);
}