using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topic.Entities;

namespace Topic.Data
{
    public static class DataSeeder
    {
        public static void SeedTopics(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TopicEntity>().HasData(
                new TopicEntity
                {
                    Id = 1,
                    Title = "Test",
                    CommentsCount = 1,
                    StartDate = DateTime.Now,
                    //CommentId = 1,
                    State = State.Pending,
                    Status = Status.Active,
                    UserId = "8716071C-1D9B-48FD-B3D0-F059C4FB8031"
                }
                );
        }

        public static void SeedComments(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comments>().HasData(
                new Comments
                {
                    Id = 1,
                    Comment = "Test",
                    PostedDate = DateTime.Now,
                    TopicEntityId = 1,
                    //TopicEntityId = 1,
                    UserId = "8716071C-1D9B-48FD-B3D0-F059C4FB8031"
                }
                );
        }

        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            PasswordHasher<User> hasher = new();
            //List<TopicEntity> topics = new();

            modelBuilder.Entity<User>().HasData(
                    new User()
                    {
                        Id = "8716071C-1D9B-48FD-B3D0-F059C4FB8031",
                        UserName = "admin@gmail.com",
                        NormalizedUserName = "ADMIN@GMAIL.COM",
                        Email = "admin@gmail.com",
                        NormalizedEmail = "ADMIN@GMAIL.COM",
                        EmailConfirmed = false,
                        PasswordHash = hasher.HashPassword(null, "Admin123!"),
                        PhoneNumber = "555337681",
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnd = null,
                        LockoutEnabled = true,
                        AccessFailedCount = 0,

                        //CommentId = 1,
                        //TopicID = 1
                        //LastAction = DateTime.Now
                    }
                    ,
                    new User()
                    {
                        Id = "D514EDC9-94BB-416F-AF9D-7C13669689C9",
                        UserName = "nika@gmail.com",
                        NormalizedUserName = "NIKA@GMAIL.COM",
                        Email = "nika@gmail.com",
                        NormalizedEmail = "NIKA@GMAIL.COM",
                        EmailConfirmed = false,
                        PasswordHash = hasher.HashPassword(null, "Nika123!"),
                        PhoneNumber = "558490645",
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnd = null,
                        LockoutEnabled = true,
                        AccessFailedCount = 0,
                        //LastAction = DateTime.Now
                    },
                    new User()
                    {
                        Id = "87746F88-DC38-4756-924A-B95CFF3A1D8A",
                        UserName = "gio@gmail.com",
                        NormalizedUserName = "GIO@GMAIL.COM",
                        Email = "gio@gmail.com",
                        NormalizedEmail = "GIO@GMAIL.COM",
                        EmailConfirmed = false,
                        PasswordHash = hasher.HashPassword(null, "Gio123!"),
                        PhoneNumber = "551442269",
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnd = null,
                        LockoutEnabled = true,
                        AccessFailedCount = 0,
                        //LastAction = DateTime.Now
                    });
                    
        }

        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                    new IdentityRole { Id = "33B7ED72-9434-434A-82D4-3018B018CB87", Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole { Id = "9C07F9F6-D3B0-458A-AB7F-218AA622FA5B", Name = "Customer", NormalizedName = "CUSTOMER" }
                );
        }

        public static void SeedUserRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "33B7ED72-9434-434A-82D4-3018B018CB87", UserId = "8716071C-1D9B-48FD-B3D0-F059C4FB8031" },
                new IdentityUserRole<string> { RoleId = "9C07F9F6-D3B0-458A-AB7F-218AA622FA5B", UserId = "D514EDC9-94BB-416F-AF9D-7C13669689C9" },
                new IdentityUserRole<string> { RoleId = "9C07F9F6-D3B0-458A-AB7F-218AA622FA5B", UserId = "87746F88-DC38-4756-924A-B95CFF3A1D8A" }
                );
        }
    }
}
