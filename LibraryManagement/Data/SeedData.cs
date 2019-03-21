using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryManagementContext(
                serviceProvider.GetRequiredService<DbContextOptions<LibraryManagementContext>>()))
            {
                // Look for any movies.
                if (context.Student.Any() || context.Book.Any() || context.BookTaken.Any())
                {
                    return;   // DB has been seeded
                }

                context.Student.AddRange(
                    new Student
                    {
                        ID = 120001,
                        Name = "ABCD",
                        /*BooksTaken = {
                            new Book
                                {
                                    ID = 123,
                                    Title = "Book C",
                                    Author = "Author C",
                                    Quantity = 3
                                }
                        }*/
                    },

                    new Student
                    {
                        ID = 120002,
                        Name = "EFGH",
                        /*BooksTaken = {
                            new Book
                                {
                                    ID = 124,
                                    Title = "Book D",
                                    Author = "Author D",
                                    Quantity = 5
                                }
                        }*/
                    },

                    new Student
                    {
                        ID = 120003,
                        Name = "IJKL"
                    },

                    new Student
                    {
                        ID = 120004,
                        Name = "MNOP",
                        /*BooksTaken = {
                            new Book
                                {
                                    ID = 121,
                                    Title = "Book A",
                                    Author = "Author A",
                                    Quantity = 2
                                }
                        }*/
                    }
                );

                context.Book.AddRange(
                    new Book
                    {
                        ID = 121,
                        Title = "Book A",
                        Author = "Author A",
                        Quantity = 2
                    },

                    new Book
                    {
                        ID = 122,
                        Title = "Book B",
                        Author = "Author B",
                        Quantity = 4
                    },

                    new Book
                    {
                        ID = 123,
                        Title = "Book C",
                        Author = "Author C",
                        Quantity = 3
                    },

                    new Book
                    {
                        ID = 124,
                        Title = "Book D",
                        Author = "Author D",
                        Quantity = 5
                    }
                );

                context.BookTaken.AddRange(
                    new BookTaken
                    {
                        TakenBy = 120002,
                        TakenBook = 124,
                        TakingDate = DateTime.Parse("2017-09-01")
                    },

                    new BookTaken
                    {
                        TakenBy = 120004,
                        TakenBook = 121,
                        TakingDate = DateTime.Parse("2017-05-14")
                    },

                    new BookTaken
                    {
                        TakenBy = 120001,
                        TakenBook = 123,
                        TakingDate = DateTime.Parse("2017-11-23")
                    }

                );

                context.SaveChanges();
            }
        }
    }
}
