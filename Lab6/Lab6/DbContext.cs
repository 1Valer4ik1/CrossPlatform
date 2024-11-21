using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace GlobalCommunity.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<LocalCommunity> LocalCommunities { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<InterestGroup> InterestGroups { get; set; }
        public DbSet<MemberInterest> MemberInterests { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<PublicationSubscription> PublicationSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Додаткове налаштування зв’язків
            modelBuilder.Entity<MemberInterest>()
                .HasOne(mi => mi.Member)
                .WithMany(m => m.MemberInterests)
                .HasForeignKey(mi => mi.MemberId);

            modelBuilder.Entity<MemberInterest>()
                .HasOne(mi => mi.InterestGroup)
                .WithMany(ig => ig.MemberInterests)
                .HasForeignKey(mi => mi.InterestGroupId);

            modelBuilder.Entity<PublicationSubscription>()
                .HasOne(ps => ps.Member)
                .WithMany(m => m.MemberInterests)
                .HasForeignKey(ps => ps.MemberId);

            modelBuilder.Entity<PublicationSubscription>()
                .HasOne(ps => ps.Publication)
                .WithMany(p => p.MemberInterests)
                .HasForeignKey(ps => ps.PublicationId);
        }
    }
}