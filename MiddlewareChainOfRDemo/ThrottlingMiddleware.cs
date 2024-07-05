namespace MiddlewareChainOfRDemo;

/**
 * ConcreteHandler. Checks whether there are too many failed login requests.
 */
public class ThrottlingMiddleware : Middleware
{
    private const int MinuteInMilliseconds = 60_000;
    private int requestPerMinute;
    private int request;
    private long currentTime;

    public ThrottlingMiddleware(int requestPerMinute)
    {
        this.requestPerMinute = requestPerMinute;
        this.currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    /**
     * Please, not that checkNext() call can be inserted both in the beginning
     * of this method and in the end.
     *
     * This gives much more flexibility than a simple loop over all middleware
     * objects. For instance, an element of a chain can change the order of
     * checks by running its check after all other checks.
     */
    public override bool Check(string email, string password)
    {
        if (DateTimeOffset.Now.ToUnixTimeMilliseconds() > currentTime + MinuteInMilliseconds)
        {
            request = 0;
            currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        request++;

        if (request > requestPerMinute)
        {
            Console.WriteLine("Request limit exceeded!");
            Thread.CurrentThread.Abort();
            //Thread.currentThread().stop();
        }
        return CheckNext(email, password);
    }
}