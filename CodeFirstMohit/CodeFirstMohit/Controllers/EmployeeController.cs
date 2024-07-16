using CodeFirstMohit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstMohit.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeDbContext _dbContext;
        public EmployeeController(EmployeeDbContext dbContext)  // DI (Constructor DI)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var employees =await (from m in _dbContext.Employees.Where(m => m.is_del == false) select m).ToListAsync();
            return View(employees);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee empObj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Employees.Add(empObj);
                int n = await _dbContext.SaveChangesAsync();
                if (n != 0)
                {
                    TempData["insert"] = "<script>alert('Employee Added SuccessFully!!');</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["insert"] = "<script>alert('Employee Failed!!');</script>";
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error in Employee Model!!");
            }
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var employee =await _dbContext.Employees.Where(m => m.Eid == id).FirstOrDefaultAsync();
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee empObj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(empObj).State = EntityState.Modified;
                int n = await _dbContext.SaveChangesAsync();
                if (n != 0)
                {
                    TempData["update"] = "<script>alert('Employee Updated SuccessFully!!');</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["update"] = "<script>alert('Employee Failed!!');</script>";
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error in Employee Model!!");
            }
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                Employee emp =await _dbContext.Employees.Where(m => m.Eid == id).FirstOrDefaultAsync();
                if (emp != null)
                {
                     emp.is_del = true;
                    _dbContext.Entry(emp).State = EntityState.Modified;
                    int n = await _dbContext.SaveChangesAsync();
                    if (n != 0)
                    {
                        TempData["delete"] = "<script>alert('Employee Deleted SuccessFully!!');</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["delete"] = "<script>alert('Employee Failed!!');</script>";
                    }
                }
                else
                {
                    TempData["delete"] = "<script>alert('Employee Not Found!!');</script>";
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error in Employee Model!!");
            }
            return View();
            }
        }
    }
