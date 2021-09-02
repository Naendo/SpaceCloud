using Coworking.Domain.Entities;

namespace Coworking.Infrastructure.Persistence.Repositories
{
    public class TestRepository : BaseRepository<User>
    {
        public TestRepository(WorkingContext context) : base(context)
        {
        }
    }
}