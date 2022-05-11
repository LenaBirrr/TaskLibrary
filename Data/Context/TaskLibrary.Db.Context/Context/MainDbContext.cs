using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.Db.Context.Context
{
    public class MainDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public MainDbContext(DbContextOptions<MainDbContext>options) : base(options) { }

        public DbSet<Category> Categories { get; set; }

       // public DbSet<User> Users { get;set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LanguageForTask> LanguageForTasks { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<ProgrammingTask> ProgrammingTasks { get; set; }

        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Comment>().ToTable("comments");
            modelBuilder.Entity<LanguageForTask>().ToTable("languages_tasks");
            modelBuilder.Entity<Notification>().ToTable("notifications");
            modelBuilder.Entity<ProgrammingTask>().ToTable("programming_tasks");
            modelBuilder.Entity<ProgrammingLanguage>().ToTable("programming_languages");
            modelBuilder.Entity<Solution>().ToTable("solutions");
            modelBuilder.Entity<Subscription>().ToTable("subscribtions");

            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("user_roles");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("user_role_owners");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("user_role_claims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");

            modelBuilder.Entity<ProgrammingTask>().HasOne(x => x.User).WithMany(x => x.ProgrammingTasks).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<ProgrammingTask>().HasOne(x => x.Category).WithMany(x => x.ProgrammingTasks).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<ProgrammingTask>().HasOne(x => x.Category).WithMany(x => x.ProgrammingTasks).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Subscription>().HasOne(x => x.User).WithMany(x => x.Subscriptions).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Subscription>().HasOne(x => x.ProgrammingTask).WithMany(x => x.Subscriptions).HasForeignKey(x => x.ProgrammingTaskId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Solution>().HasOne(x => x.ProgrammingTask).WithMany(x => x.Solutions).HasForeignKey(x => x.ProgrammingTaskId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<LanguageForTask>().HasOne(x => x.ProgrammingTask).WithMany(x => x.ProgrammingLanguages).HasForeignKey(x => x.ProgrammingLanguageId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<LanguageForTask>().HasOne(x => x.ProgrammingLanguage).WithMany(x => x.ProgrammingTasks).HasForeignKey(x => x.ProgrammingTaskId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Notification>().HasOne(x => x.Subscription).WithMany(x => x.Notifications).HasForeignKey(x => x.SubscribtionId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Solution>().HasOne(x => x.ProgrammingLanguage).WithMany(x => x.Solutions).HasForeignKey(x => x.ProgrammingLanguageId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Comment>().HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Comment>().HasOne(x => x.ProgrammingTask).WithMany(x => x.Comments).HasForeignKey(x => x.ProgrammingTaskId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
