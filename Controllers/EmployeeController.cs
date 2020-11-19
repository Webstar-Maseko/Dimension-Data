using Dimension_Data.Data;
using Dimension_Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Dimension_Data.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DimensionContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public EmployeeController(DimensionContext context, UserManager<IdentityUser> userManager , SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public void empAge()
        {
            ViewBag.below21 = (from age in _context.EmployeeData where age.Age >=16 && age.Age <=20  select age).Count();
            ViewBag.below31 = (from age in _context.EmployeeData where age.Age >= 21 && age.Age <= 30 select age).Count();
            ViewBag.below41 = (from age in _context.EmployeeData where age.Age >= 31 && age.Age <= 40 select age).Count();
            ViewBag.below51 = (from age in _context.EmployeeData where age.Age >= 41 && age.Age <= 50 select age).Count();
            ViewBag.above51 = (from age in _context.EmployeeData where age.Age >= 51 select age).Count();
        }
        public void employRole()
        {
            ViewBag.saleEx = (from role in _context.EmployeeData where role.JobRole == "Sales Executive" select role).Count();
            ViewBag.saleRep = (from role in _context.EmployeeData where role.JobRole == "Sales Representative" select role).Count();
            ViewBag.manager = (from role in _context.EmployeeData where role.JobRole == "Manager" select role).Count();
            ViewBag.health = (from role in _context.EmployeeData where role.JobRole == "Healthcare Representative" select role).Count();
            ViewBag.lab = (from role in _context.EmployeeData where role.JobRole == "Laboratory Technician" select role).Count();
            ViewBag.manufa = (from role in _context.EmployeeData where role.JobRole == "Manufacturing Director" select role).Count();
            ViewBag.humanR = (from role in _context.EmployeeData where role.JobRole == "Human Resources" select role).Count();
            ViewBag.researchD = (from role in _context.EmployeeData where role.JobRole == "Research Director" select role).Count();
            ViewBag.researchS = (from role in _context.EmployeeData where role.JobRole == "Research Scientist" select role).Count();
        }

        public void viewData()
        {
            ViewBag.depart = (from depar in _context.EmployeeData select depar.Department).Distinct().Count();
            ViewBag.count = _context.EmployeeData.Count();
            double total = (double)ViewBag.count * 5;
            ViewBag.salar = (from salar in _context.EmployeeData select salar.MonthlyIncome).Sum().ToString("c");
            ViewBag.satisfa = ((from satis in _context.EmployeeData select satis.EnvironmentSatisfaction).Sum() / total).ToString("P");
            ViewBag.sales = (from sales in _context.EmployeeData where sales.Department == "sales" select sales).Count();
            ViewBag.hr = (from hr in _context.EmployeeData where hr.Department == "Human Resources" select hr).Count();
            ViewBag.rd = (from rd in _context.EmployeeData where rd.Department == "Research & Development" select rd).Count();
            ViewBag.male = (from male in _context.EmployeeData where male.Gender == "Male" select male).Count();
            ViewBag.female = (from female in _context.EmployeeData where female.Gender == "Female" select female).Count();
            ViewBag.married = (from marital in _context.EmployeeData where marital.MaritalStatus == "Married" select marital).Count();
            ViewBag.single = (from marital in _context.EmployeeData where marital.MaritalStatus == "Single" select marital).Count();
            ViewBag.divorce = (from marital in _context.EmployeeData where marital.MaritalStatus == "Divorced" select marital).Count();
        }
        [Authorize(Policy = "writepolicy")]
        public IActionResult Analytics()
        {
            empAge();
            viewData();
            employRole();
            return View();
        }
        [Authorize(Policy = "writepolicy")]
        public async Task<IActionResult> Employees()
        {
            viewData();
            String query = "Select TOP(10) * FROM EmployeeData ORDER BY EmployeeNumber DESC";
            return View(await _context.EmployeeData.FromSqlRaw(query).ToListAsync());
        }

        // GET: Employee
        [Authorize(Policy = "readpolicy")]
        public async Task<IActionResult> Index()
        {

            String query = $"Select * FROM EmployeeData where UserID= '{_userManager.GetUserId(User)}'";
            return View(await _context.EmployeeData.FromSqlRaw(query).ToListAsync());
        }
        public async Task<IActionResult> Events()
        {
            var events = await (from user in _context.ToDo where user.UserId == _userManager.GetUserId(User) select user).ToListAsync();
            return Json(events);
        }

        //GET: Employee by empNumber
        [HttpPost]
        [Authorize(Policy = "readpolicy")]
        public async Task<IActionResult> index(int id)
        {
            viewData();
            String query = $"Select * from EmployeeData where EmployeeNumber = {id}";
            return View(await _context.EmployeeData.FromSqlRaw(query).ToListAsync());
        }

        // GET: Employee/Details/5
        [Authorize]
        [Authorize(Policy = "readpolicy")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeData = await _context.EmployeeData
                .FirstOrDefaultAsync(m => m.EmployeeNumber == id);
            if (employeeData == null)
            {
                return NotFound();
            }

            return View(employeeData);
        }

        // GET: Employee/Create
        [Authorize]
        [Authorize(Policy = "writepolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "writepolicy")]
        public async Task<IActionResult> Create([Bind("Age,Attrition,BusinessTravel,DailyRate,Department,DistanceFromHome,Education,EducationField,EmployeeCount,EmployeeNumber,EnvironmentSatisfaction,Gender,HourlyRate,JobInvolvement,JobLevel,JobRole,JobSatisfaction,MaritalStatus,MonthlyIncome,MonthlyRate,NumCompaniesWorked,Over18,OverTime,PercentSalaryHike,PerformanceRating,RelationshipSatisfaction,StandardHours,StockOptionLevel,TotalWorkingYears,TrainingTimesLastYear,WorkLifeBalance,YearsAtCompany,YearsInCurrentRole,YearsSinceLastPromotion,YearsWithCurrManager")] EmployeeData employeeData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeData);
        }

        // GET: Employee/Edit/5
        [Authorize]
        [Authorize(Policy = "writepolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeData = await _context.EmployeeData.FindAsync(id);
            if (employeeData == null)
            {
                return NotFound();
            }
            return View(employeeData);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "writepolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Age,Attrition,BusinessTravel,DailyRate,Department,DistanceFromHome,Education,EducationField,EmployeeCount,EmployeeNumber,EnvironmentSatisfaction,Gender,HourlyRate,JobInvolvement,JobLevel,JobRole,JobSatisfaction,MaritalStatus,MonthlyIncome,MonthlyRate,NumCompaniesWorked,Over18,OverTime,PercentSalaryHike,PerformanceRating,RelationshipSatisfaction,StandardHours,StockOptionLevel,TotalWorkingYears,TrainingTimesLastYear,WorkLifeBalance,YearsAtCompany,YearsInCurrentRole,YearsSinceLastPromotion,YearsWithCurrManager")] EmployeeData employeeData)
        {
            if (id != employeeData.EmployeeNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeDataExists(employeeData.EmployeeNumber))
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
            return View(employeeData);
        }

        // GET: Employee/Delete/5
        [Authorize(Policy = "writepolicy")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeData = await _context.EmployeeData
                .FirstOrDefaultAsync(m => m.EmployeeNumber == id);
            if (employeeData == null)
            {
                return NotFound();
            }

            return View(employeeData);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeData = await _context.EmployeeData.FindAsync(id);
            _context.EmployeeData.Remove(employeeData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeDataExists(int id)
        {
            return _context.EmployeeData.Any(e => e.EmployeeNumber == id);
        }
    }
}
