namespace Work.Rabbi.Common.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// CommitAsync: pass identity key type
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        Task<int> CommitAsync<TKey>();
        /// <summary>
        /// CommitWithErrorMsgAsync: commit with 
        /// </summary>
        /// <returns></returns>
        Task<(int, string)> CommitWithErrorMsgAsync();
    }
}