using Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DbInitializer
    {
        public static void Initialize(DataContext dataContext)
        {
            //Database.EnsureDeleted();
            dataContext.Database.EnsureCreated();
        }
    }
}
