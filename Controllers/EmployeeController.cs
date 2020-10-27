using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dimension_Data.Data;
using Dimension_Data.Models;
using Microsoft.EntityFrameworkCore.Storage;
using NuGet.Frameworks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Dimension_Data.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DimensionContext _context;

        public EmployeeController(DimensionContext context)
        {
            _context = context;
        }
        public void viewData()
        {
            ViewBag.depart = (from depar in _context.EmployeeData select depar.Department).Distinct().Count();
            ViewBag.count = _context.EmployeeData.Count();
            ViewBag.salar = (from salar in _context.EmployeeData select salar.MonthlyIncome).Sum().ToString("c");
            ViewBag.satisfa = (from satis in _context.EmployeeData select satis.EnvironmentSatisfaction).Average().ToString("P");
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
          
            viewData();
            String query = "Select TOP(5) * from EmployeeData ORDER BY EmployeeNumber DESC";
           
            return View(await _context.EmployeeData.FromSqlRaw(query).ToListAsync());
        }
        //GET: Employee by empNumber
        [HttpPost]
        public async Task<IActionResult> index( int id)
        {
            viewData();
            String query = $"Select * from EmployeeData where EmployeeNumber = {id}";
            return View(await _context.EmployeeData.FromSqlRaw(query).ToListAsync());
        }

        // GET: Employee/Details/5
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
