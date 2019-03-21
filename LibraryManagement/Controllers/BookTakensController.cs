using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;
using LibraryManagement.Models.LibraryVM;

namespace LibraryManagement.Controllers
{
    public class BookTakensController : Controller
    {
        private readonly LibraryManagementContext _context;

        public BookTakensController(LibraryManagementContext context)
        {
            _context = context;
        }

        // GET: BookTakens
        public async Task<IActionResult> Index()
        {
            return View(await _context.BookTaken.ToListAsync());
        }

        // GET: BookTakens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookTaken = await _context.BookTaken
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bookTaken == null)
            {
                return NotFound();
            }

            return View(bookTaken);
        }

        // GET: BookTakens/Create
        public IActionResult Create()
        {
            //return View();

            var AllStudentsList = new BookTakenCreateVM(this._context);
            return View(AllStudentsList);
        }

        /*
        // POST: BookTakens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TakenBy,TakenBook,TakingDate")] BookTaken bookTaken)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookTaken);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookTaken);
        }
        */

        // POST: BookTakens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, StudentID, BookID, TakingDate")] BookTakenCreateVM bookTakenCreateVM)
        {
            if (ModelState.IsValid)
            {
                BookTaken issue = new BookTaken()
                {
                    TakenBy = bookTakenCreateVM.StudentID,
                    TakenBook = bookTakenCreateVM.BookID,
                    TakingDate = bookTakenCreateVM.TakingDate
                };
                
                _context.BookTaken.Add(issue);
                //await _context.SaveChangesAsync();

                int bookID = issue.TakenBook;
                var bookToUpdate = await _context.Book
                    .FirstOrDefaultAsync(s => s.ID == bookID);

                bookToUpdate.Quantity -= 1;

                if (await TryUpdateModelAsync<Book>(
                        bookToUpdate,
                        "Book",
                        i => i.ID, i => i.Title,
                        i => i.Author, i => i.Quantity))
                {
                    
                }
                
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(bookTakenCreateVM);
        }

        /*
        // GET: BookTakens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookTaken = await _context.BookTaken.FindAsync(id);
            if (bookTaken == null)
            {
                return NotFound();
            }
            return View(bookTaken);
        }

        // POST: BookTakens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TakenBy,TakenBook,TakingDate")] BookTaken bookTaken)
        {
            if (id != bookTaken.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookTaken);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookTakenExists(bookTaken.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookTaken);
        }
        */

        // GET: BookTakens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookTaken = await _context.BookTaken
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bookTaken == null)
            {
                return NotFound();
            }

            return View(bookTaken);
        }

        // POST: BookTakens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookTaken = await _context.BookTaken.FindAsync(id);
            _context.BookTaken.Remove(bookTaken);

            int bookID = bookTaken.TakenBook;
            var bookToUpdate = await _context.Book
                .FirstOrDefaultAsync(s => s.ID == bookID);

            bookToUpdate.Quantity += 1;

            if (await TryUpdateModelAsync<Book>(
                    bookToUpdate,
                    "Book",
                    i => i.ID, i => i.Title,
                    i => i.Author, i => i.Quantity))
            {

            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool BookTakenExists(int id)
        {
            return _context.BookTaken.Any(e => e.ID == id);
        }
    }
}
