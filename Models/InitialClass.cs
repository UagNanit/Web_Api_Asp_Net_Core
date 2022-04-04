using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVC_2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_2.Models
{
    public class InitialClass
    {
        public static void Initialize(bookContext context)
        {
            // Look for any movies.
            if (context.Books.Any())
            {
                return;   // DB has been seeded
            }
            context.Authors.AddRange(
                new Author { FirstName = "Стендаль", SecondName = "Бомбе" },
                new Author { FirstName = "Саймак", SecondName = "Клиффорд" },
                new Author { FirstName = "a3", SecondName = "f7" },
                new Author { FirstName = "Брауде", SecondName = "Эрик" }
                );

            context.SaveChanges();

            context.Books.AddRange(
                new Book
                {
                    Title = "Червоне та чорне1",
                    ReleaseDate = 1989,
                    AuthorId=1,
                    Publisher = "Старий Львыв",
                    AgeCategory = 16,
                    Rating = 7.6
                },
                new Book
                {
                       Title = "Город",
                       ReleaseDate = 1985,
                       Author = context.Authors.First(el=>el.Id==2),
                       Publisher = "Фантастика",
                       AgeCategory = 12,
                       Rating=11
                },
                new Book
                {
                        Title = "Сказки",
                        ReleaseDate = 2015,
                        Author = context.Authors.First(el => el.Id == 3),
                        Publisher = "Bhv",
                        AgeCategory = 2,
                        Rating=8.5

                },
                new Book
                {
                         Title = "Технология разработки программного обеспечения",
                         ReleaseDate = 2004,
                         Author = context.Authors.First(el => el.Id == 4),
                         Publisher = "Питер",
                         AgeCategory = 8,
                         Rating=9.8

                },
                new Book
                {
                    Title = "wdwdwefwef",
                    ReleaseDate = 2015,
                    Author = context.Authors.First(el => el.Id == 2),
                    Publisher = "Bhv",
                    AgeCategory = 2,
                    Rating = 9.5

                },
                new Book
                {
                    Title = "efeasfsefsef",
                    ReleaseDate = 2004,
                    Author = context.Authors.First(el => el.Id == 2),
                    Publisher = "Питер",
                    AgeCategory = 8,
                    Rating = 6.8

                });
            
            context.SaveChanges();

            const string adminRoleName = "admin";
            const string userRoleName = "user";

            // добавляем роли
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };

            context.Roles.AddRange(new Role[]{ adminRole, userRole});
            context.SaveChanges();

            context.Users.Add(new User { Email = "admin@gmail.com", Password = "123456", Role=adminRole });
            context.Users.Add(new User { Email = "user@gmail.com", Password = "123456", Role = userRole });
            context.SaveChanges();
        }

    }
}
