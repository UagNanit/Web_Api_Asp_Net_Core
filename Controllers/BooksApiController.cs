using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_2.Data;
using MVC_2.Models;

namespace MVC_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors]
    public class BooksApiController : ControllerBase
    {
        private readonly bookContext _context;
        //IBookRepository repo;

        public BooksApiController(bookContext context/*, IBookRepository r*/)
        {
            _context = context;
            //repo = r;
        }

        // GET: api/BooksApi/Auth
       
        [Route("Auth")]
        [HttpGet]
        [Authorize]
        public IActionResult GetAuth()
        {
            Console.WriteLine("API Validated");
            return Ok("API Validated");
        }

        // GET: api/BooksApi/Authors"
        // Получение всех авторов
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Authors")]
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var res = await _context.Authors.ToArrayAsync();
            if (res == null) { return NotFound(); }
            return res;
        }


        // GET: api/BooksApi/Books
        // Получение всех книг
        //[Authorize]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Authorize(Roles = "admin,user")]
        [Route("Books")]
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var res = await _context.Books.ToArrayAsync();
            if (res == null) { return NotFound(); }
            return res;
           
            /*var rep = repo.GetBooks();
            if (rep == null) { return NotFound(); }
            return rep.ToArray();*/

        }

        // GET: api/BooksApi/Books/5
        // Получение книги по id
        [Route("Books/{id}")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.Include(el => el.Author).FirstAsync(el=>el.Id==id);
            if (book == null)
            {
                return NotFound();
            }
            return book;

            /*var rep = repo.Get(id);
            if (rep == null) { return NotFound(); }
            return rep;*/
        }

        // PUT: api/BooksApi/Books/5
        // редактирование книги по id
        [Route("Books/{id}")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            /*if (id != book.Id)
            {
                return BadRequest("err " + book.Id);
            }

            try { repo.Update(book); } catch (Exception e) { return BadRequest(); }
            return Ok();*/

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

        // POST: api/BooksApi/Books
        // созданиие новой записи "книга"
        [Route("Books")]
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            /*try { repo.Create(book); } catch (Exception e) { return BadRequest(); }
            */
            return Ok();
        }

        // DELETE: api/BooksApi/5
        // удаление книги по id
        [Route("Books/{id}")]
        [HttpDelete]
        //[Authorize]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            
            /*if (!BookExists(id))
            {
                return NotFound();
            }

            repo.Delete(id);*/
            return Ok();
        }

        
    }
}
