namespace Work.Rabbi.Common.Interfaces
{
    public interface ILoggedInUserService
    {
        bool IsAuthenticated { get; }
        string UserId { get; }
    }
}