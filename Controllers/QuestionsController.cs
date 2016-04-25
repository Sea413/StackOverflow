using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using StackOverflow.Models;
using Microsoft.Data.Entity;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StackOverflow.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionsController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db
        )
        {
            _userManager = userManager;
            _db = db;
        }


        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
            return View(_db.Questions.Where(x => x.User.Id == currentUser.Id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Question question)
        {
            var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
            question.User = currentUser;
            _db.Questions.Add(question);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var thisQuestion = db.Questions.FirstOrDefault(questions => questions.QuestionId == id);
            return View(thisQuestion);
        }

        [HttpPost]
        public IActionResult Edit(Question question)
        {
            db.Entry(question).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            var thisQuestion = db.Questions.FirstOrDefault(x => x.QuestionId == id);
            db.Questions.Remove(thisQuestion);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
