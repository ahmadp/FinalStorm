using Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class FinalStromContext: DbContext
    {
        public FinalStromContext() : base("name=FinalStormConStr")
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<SubscriptionType> SubscriptionType { get; set; }
        public DbSet<UserSubscrption> UserSubscrption { get; set; }
        public DbSet<Comments> Comment { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<UserFollower> UserFollower { get; set; }
        public DbSet<UserNotificationSetting> UserNotificationSetting { get; set; }



        //public DbSet<ApplicantsJobs> ApplicantsJobs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
