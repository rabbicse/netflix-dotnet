namespace Worl.Rabbi.Common.Domain
{
    public interface IAuthorizeEntity
    {
        public string? AuthorizeStatus { get; protected set; }
        public string? AuthorizedBy { get; protected set; }
        public DateTime? AuthorizedDate { get; protected set; }
    }
}
