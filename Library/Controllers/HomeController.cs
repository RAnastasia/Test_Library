

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private LibraryContext db = new LibraryContext();



        // GET: Books
        public async Task<ActionResult> Index()
        {
            return View(await db.Books.ToListAsync());
        }

    

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BookId,BookName,AuthorName,Quantity,Availability")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
           


            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BookId,BookName,AuthorName,Quantity,Availability")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Book book = await db.Books.FindAsync(id);
            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Take(int id)
        {

            ViewBag.BookId = id;
            Book book = db.Books.Find(id);
            book.Quantity--;
            if (book.Quantity == 0)
            {
                book.Availability = "Taken";
            }
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            
            return View();
        }
        [HttpPost]
        public ActionResult Take(CardFile cardFile)
        {
    
            cardFile.DateIn = DateTime.Now;
            db.CardFiles.Add(cardFile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Return(int? id)
        {

            ViewBag.BookId = id;
            Book book = db.Books.Find(id);
            book.Quantity++;
            book.Availability = "Available";
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();

            return View();
        }
        [HttpPost]
        public ActionResult Return(CardFile cardFile, int id)
        {
            if(cardFile.BookId == id)
            { cardFile.DateReturn = DateTime.Now; }
            
            db.CardFiles.Add(cardFile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
