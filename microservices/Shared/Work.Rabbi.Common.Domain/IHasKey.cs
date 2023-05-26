namespace Worl.Rabbi.Common.Domain
{
    public interface IHasKey<T>
    {
        T Id { get; set; }
    }
}