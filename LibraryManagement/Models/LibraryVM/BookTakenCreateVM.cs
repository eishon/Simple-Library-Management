using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models.LibraryVM
{
    public class BookTakenCreateVM
    {
        LibraryManagementContext _context;

        public BookTakenCreateVM()
        {
            _context = null;
        }

        public BookTakenCreateVM(LibraryManagementContext context)
        {
            _context = context;
            Initialize();
        }

        public int ID { get; set; }

        [Required]
        [Display(Name = "Student ID")]
        public int StudentID { get; set; }

        public IEnumerable<SelectListItem> AllStudents { get; set; }

        [Required]
        [Display(Name = "Book ID")]
        public int BookID { get; set; }

        public IEnumerable<SelectListItem> AllBooks { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TakingDate { get; set; }

        void Initialize()
        {
            using (var context = _context)
            {
                List<SelectListItem> studentsList = context.Student.AsNoTracking()
                    .OrderBy(n => n.Name)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.ID.ToString(),
                            Text = n.Name
                        }).ToList();

                List<SelectListItem> booksList = context.Book.AsNoTracking()
                    .OrderBy(n => n.Title)
                        .Where(n=> n.Quantity>0)
                            .Select(n =>
                                new SelectListItem
                                {
                                    Value = n.ID.ToString(),
                                    Text = n.Title
                                }).ToList();

                AllStudents = new SelectList(studentsList, "Value", "Text");
                AllBooks = new SelectList(booksList, "Value", "Text");
            }
        }
    }
}
