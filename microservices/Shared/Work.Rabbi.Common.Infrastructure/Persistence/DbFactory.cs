namespace Work.Rabbi.Common.Infrastructure.Persistence
{
    public class DbFactory<TContext> : IDisposable where TContext : class
    {
        #region Declaration(s)    
        private readonly Func<TContext> _instanceFunc;
        private TContext? _dbContext;
        public TContext DbContext => _dbContext ??= _instanceFunc.Invoke();
        #endregion

        #region Constructor(s)
        public DbFactory(Func<TContext> dbContextFactory)
        {
            _instanceFunc = dbContextFactory;
        }
        #endregion

        #region IDisposable
        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // Free unmanaged resources
                if (_dbContext != null)
                    (_dbContext as IDisposable).Dispose();
            }

            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
        #endregion
    }
}