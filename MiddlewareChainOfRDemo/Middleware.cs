namespace MiddlewareChainOfRDemo;

public abstract class Middleware
{
    private Middleware? _next;

    /**
     * Builds chains of middleware objects.
     */
    public static Middleware Link(Middleware first, params Middleware[] chain)
    {
        Middleware head = first;
        foreach (Middleware nextInChain in chain)
        {
            head._next = nextInChain;
            head = nextInChain;
        }
        return first;
    }

    /**
     * Subclasses will implement this method with concrete checks.
     */
    public abstract bool Check(string email, string password);

    /**
     * Runs check on the next object in chain or ends traversing if we're in
     * last object in chain.
     */
    protected bool CheckNext(string email, string password)
    {
        if (_next == null)
        {
            return true;
        }
        return _next.Check(email, password);
    }
}