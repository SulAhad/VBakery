using Microsoft.EntityFrameworkCore;
using VBakery.DB;
using VBakery.MenuDB;
using VBakery.Model;

namespace VBakery
{
    public class OrderForBuyersContext : DbContext
    {
        public DbSet<OrderForBuyer> OrderForBuyers => Set<OrderForBuyer>();
        public OrderForBuyersContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=OrderForBuyer.db");
        }
    }
    public class LogOrdersContext : DbContext
    {
        public DbSet<LogOrder> LogOrders => Set<LogOrder>();
        public LogOrdersContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=LogOrder.db");
        }
    }
    public class OrderForDeliverysContext : DbContext
    {
        public DbSet<OrderForDelivery> OrderForDeliverys => Set<OrderForDelivery>();
        public OrderForDeliverysContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=OrderForDelivery.db");
        }
    }
    public class RecipesContext : DbContext
    {
        public DbSet<Recipe> Recipes => Set<Recipe>();
        public RecipesContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=recipes.db");
        }
    }
    public class TimerStartsContext : DbContext
    {
        public DbSet<TimerStart> TimerStarts => Set<TimerStart>();
        public TimerStartsContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=TimerStarts.db");
        }
    }
    public class TimerEndsContext : DbContext
    {
        public DbSet<TimerEnd> TimerEnds => Set<TimerEnd>();
        public TimerEndsContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=TimerEnd.db");
        }
    }
    public class UsersContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public UsersContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=User.db");
        }
    }
    public class MessagesContext : DbContext
    {
        public DbSet<Message> Messages => Set<Message>();
        public MessagesContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Message.db");
        }
    }
    public class VisitContext : DbContext
    {
        public DbSet<Visit> Visits => Set<Visit>();
        public VisitContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Visit.db");
        }
    }
    public class MenuArea1Context : DbContext
    {
        public DbSet<MenuArea1> Menu1 => Set<MenuArea1>();
        public MenuArea1Context()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Menu1.db");
        }
    }
    public class MenuArea2Context : DbContext
    {
        public DbSet<MenuArea2> Menu2 => Set<MenuArea2>();
        public MenuArea2Context()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Menu2.db");
        }
    }
    public class MenuArea3Context : DbContext
    {
        public DbSet<MenuArea3> Menu3 => Set<MenuArea3>();
        public MenuArea3Context()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Menu3.db");
        }
    }
    public class MenuArea4Context : DbContext
    {
        public DbSet<MenuArea4> Menu4 => Set<MenuArea4>();
        public MenuArea4Context()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Menu4.db");
        }
    }
    public class SecondCourseContext : DbContext
    {
        public DbSet<SecondCourse> SecondCourses => Set<SecondCourse>();
        public SecondCourseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=SecondCourse.db");
        }
    }
    public class ThirdCourseContext : DbContext
    {
        public DbSet<ThirdCourse> ThirdCourses => Set<ThirdCourse>();
        public ThirdCourseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ThirdCourse.db");
        }  
    }
}

