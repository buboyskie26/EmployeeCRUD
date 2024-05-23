using Microsoft.Ajax.Utilities;
using SDS.Data;
using SDS.Models.Entities;
using SDS.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SDS.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;

        }
        // Parameterless constructor for MVC to create the controller
        public EmployeesController() : this(new ApplicationDbContext())
        {
            // Default constructor calls the parameterized constructor with a new instance of ApplicationDbContext
        }
        // GET: Employees
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var employees = await _dbContext
                    .Employees
                    .ToListAsync();
                return View(employees);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            // Creates a new instance of AddEmployeeViewModel to represent an empty employee,
            // So the model here should be AddEmployeeViewModel.
            var employee = new AddEmployeeViewModel();
            return View(employee);
        }

        [HttpPost]
        public async Task<ActionResult> Create(AddEmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // If the model is not valid, return to the same view with validation errors
                return View(viewModel);
            }

            /* Check if EmpNo in the database is already in use */
            if (await IsEmployeeNumberInUse(viewModel.EmpNo))
            {
                ModelState.AddModelError("EmpNo", "Employee number is already in use.");
                return View(viewModel);
            }

            /* Check if First Name and Last Name in the database is already in use */
            if (await HasFirstNameLastName(viewModel.FirstName, viewModel.LastName))
            {
                // Validation error in the FirstName field that will show-up on the Create MVC Page.
                ModelState.AddModelError("FirstName", "Firstname is already in use");

                // Validation error in the LastName field that will show-up on the Create MVC Page.
                ModelState.AddModelError("LastName", "Lastname is already in use");
                return View(viewModel);
            }

            try
            {
                //
                var employee = new Employee()
                {
                    EmpNo = viewModel.EmpNo,
                    FirstName = viewModel.FirstName.Trim(),
                    LastName = viewModel.LastName.Trim(),
                    BirthDate = viewModel.BirthDate,
                    EmailAddress = viewModel.EmailAddress,
                    ContactNo = viewModel.ContactNo,
                };

                _dbContext.Employees.Add(employee);
                await _dbContext.SaveChangesAsync();
                // Redirected to the Index Action under Employees Controller
                // In our case, It's the Employee Table
                return RedirectToAction("Index", "Employees");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while saving to the database. {ex}");
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            // Get Employee Object base on the id parameter
            var getEmployeeObject = await _dbContext.Employees.FindAsync(id);

            if (getEmployeeObject == null)
            {
                // Handle the case where the employee's id is not found
                return HttpNotFound();
            }
            // All values from getEmployeeObject will be placed in EditEmployeeViewModel
            var employee = new EditEmployeeViewModel()
            {
                EmpNo = getEmployeeObject.EmpNo,
                FirstName = getEmployeeObject.FirstName,
                LastName = getEmployeeObject.LastName,
                BirthDate = getEmployeeObject.BirthDate,
                EmailAddress = getEmployeeObject.EmailAddress,
                ContactNo = getEmployeeObject.ContactNo,
            };
            return View(employee);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditEmployeeViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                // If the model is not valid, return to the same view with validation errors
                return View(viewModel);
            }
            // Get Employee Object base on the viewModel.Id parameter

            var getEmployeeObject = await _dbContext.Employees.FindAsync(viewModel.ID);

            if (getEmployeeObject == null)
            {
                // Handle the case where the employee's id is not found
                return HttpNotFound();
            }
            // 
            if (await IsEmployeeNumberInUseInEditMode(viewModel.EmpNo, viewModel.ID))
            {
                ModelState.AddModelError("EmpNo", "Employee number is already in use.");
                return View(viewModel);
            }

            // Check if First Name and Last Name in the database is already in use
            if (await HasFirstNameLastNameInEditMode(
                viewModel.FirstName, viewModel.LastName, viewModel.ID))
            {
                ModelState.AddModelError("FirstName", "Firstname is already in use");
                ModelState.AddModelError("LastName", "Lastname is already in use");
                return View(viewModel);
            }

            if (getEmployeeObject != null)
            {
                getEmployeeObject.EmpNo = viewModel.EmpNo;
                getEmployeeObject.FirstName = viewModel.FirstName.Trim();
                getEmployeeObject.LastName = viewModel.LastName.Trim();
                getEmployeeObject.ContactNo = viewModel.ContactNo;
                getEmployeeObject.BirthDate = viewModel.BirthDate;
                getEmployeeObject.EmailAddress = viewModel.EmailAddress;

                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Employees");

            /* return View(employee);*/
        }

        [HttpPost]
        public async Task<JsonResult> DeleteJSON(long id)
        {
            // This is from the delete function ajax call in the Index.cshtml
            try
            {
                var employee = await _dbContext.Employees.FirstOrDefaultAsync(
                    emp => emp.ID == id);

                if (employee != null)
                {
                    _dbContext.Employees.Remove(employee);
                    await _dbContext.SaveChangesAsync();
                }

                return Json(new { success = true, employee });
            }
            catch (Exception)
            {
                // Handle exception
                return Json(new { success = false, message = "Error occurred during deletion" });
            }
        }


        private async Task<bool> IsEmployeeNumberInUse(string empNo)
        {
            if (string.IsNullOrEmpty(empNo))
            {
                return false; // If either parameter is null or empty, return false
            }
            // Making sure it is a unique EmpNo in the Creation scenario.
            return await _dbContext.Employees.AnyAsync(e => e.EmpNo == empNo.Trim());
        }
        private async Task<bool> HasFirstNameLastName(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false; // If either parameter is null or empty, return false
            }
            // Making sure it is a unique Firstname + Lastname combination in the Creation scenario.
            return await _dbContext.Employees
                .AnyAsync(e => e.FirstName.Trim() == firstName.Trim() && e.LastName.Trim() == lastName.Trim());

        }

        private async Task<bool> IsEmployeeNumberInUseInEditMode(string empNo, int employeeId)
        {
            return await _dbContext.Employees.AnyAsync(e => e.EmpNo == empNo.Trim()
                // At first place, you're editing the employee object under employeeId
                // Making sure, the validation error will not showup while editing other field of Employee Object under the  selected employeeId
                // while not touching the EmpNo values
                && e.ID != employeeId);
        }

        private async Task<bool> HasFirstNameLastNameInEditMode(string firstName, string lastName, int employeeId)
        {
            return await _dbContext.Employees
                .AnyAsync(e => e.FirstName.Trim() == firstName.Trim()
                && e.LastName.Trim() == lastName.Trim()
                // At first place, you're editing the employee object under employeeId
                // Making sure, the validation error will not showup while editing other field of Employee Object under the  selected employeeId
                // while not touching the FirstName and LastName values
                && e.ID != employeeId);
        }

    }
}