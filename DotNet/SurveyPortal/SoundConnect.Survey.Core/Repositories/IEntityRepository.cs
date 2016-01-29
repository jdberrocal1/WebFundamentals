namespace SoundConnect.Survey.Core.Repositories
{
    public interface IEntityRepository<T> : IRepository<T> where T : class
    {
        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}
