using MediPlusMVC.DAL.Contexts;
using MediPlusMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediPlusMVC.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class BlogController : Controller
    {
        readonly AppDBContext _context;
        public BlogController(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await _context.Blogs.ToListAsync();
            return View(blogs);
        }
        public async Task<IActionResult> Info(int Id)
        {
            Blog blog = await _context.Blogs.FirstOrDefaultAsync(b=>b.Id == Id);
            if (blog == null)
            {
                return NotFound("Something went wrong");
            }
            return View(blog);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");
            }
            blog.CreatedAt = DateTime.Now;
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Blog");
        }



    }
}
