using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment1.Data;
using Assignment1.Models;
using Assignment1.Models.ViewModels;
namespace Assignment1.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolCommunityContext _context;

        public StudentsController(SchoolCommunityContext context)
        {
            _context = context;
        }


        // GET: Students
        public async Task<IActionResult> Index(int ID)
        {
            CommunityViewModel studentViewModel = new CommunityViewModel();

            studentViewModel.Students = await _context.Students
                .Include(i => i.CommunityMemberships)
                .ThenInclude(i => i.Community)
                .AsNoTracking()
                .ToListAsync()
            ;

            if (ID != 0)
            {
                ViewData["StudentID"] = ID;
                studentViewModel.CommunityMemberships = studentViewModel.Students.Where(i => i.ID == ID).Single().CommunityMemberships;
            }

            return View(studentViewModel);
        }


        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName,EnrollmentDate")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }

        // GET: Students/EditMemberships/6
        public async Task<IActionResult> EditMemberships(int id)
        {
            CommunityViewModel communityViewModel = new CommunityViewModel();

            communityViewModel.CommunityMemberships = await _context.CommunityMemberships.Where(i => i.StudentID == id).ToListAsync();
            communityViewModel.Students = await _context.Students.Where(i => i.ID == id).ToListAsync();
            communityViewModel.Communities = await _context.Communities.ToListAsync();

            return View(communityViewModel);
        }

        public async Task<IActionResult> AddMemberships(int studentId, string communityId)
        {
            CommunityMembership addMember = new CommunityMembership();

            addMember.CommunityID = communityId;
            addMember.StudentID = studentId;
            _context.CommunityMemberships.Add(addMember);

            await _context.SaveChangesAsync();

            return RedirectToAction("EditMemberships", new { id = studentId });
        }

        public async Task<IActionResult> RemoveMemberships(int studentId, string communityId)
        {
            CommunityMembership removeMember = new CommunityMembership();

            removeMember.CommunityID = communityId;
            removeMember.StudentID = studentId;
            _context.CommunityMemberships.Remove(removeMember);

            await _context.SaveChangesAsync();

            return RedirectToAction("EditMemberships", new { id = studentId });

        }


    }
}
