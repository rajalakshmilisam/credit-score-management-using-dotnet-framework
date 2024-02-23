using CreditApplicationMVCProject.Models;
using CreditApplicationMVCProject.Bo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using CreditApplicationMVCProject.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CreditApplicationMVCProject.Controllers
{
    public class CreditController : Controller
    {
        private CreditBo _creditBo;
        private ICreditRepository _creditRepository;

        public CreditController(ICreditRepository creditRepository)
        {
            _creditRepository = creditRepository;
            _creditBo = new CreditBo(creditRepository);
        }
        //Get: CreditController
        public ActionResult Index()
        {
            IEnumerable<Customer> customersList = _creditBo.ListOfAllCustomers();
            return View("Views/Credit/ListofAllCustomers.cshtml", customersList);
        }
        //Get: CreditController/create
        public ActionResult Create()
        {
            IEnumerable<PurposeMaster> purposes = _creditBo.GetPurposeOptions();
            ViewBag.Purposes = purposes;

            // Retrieve Status options from the database
            IEnumerable<CreditApplicationStatusMaster> statuses = _creditBo.GetStatusOptions();
            ViewBag.Statuses = statuses;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreditApplication creditApplication)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = _creditBo.CreateCreditApplication(creditApplication);

                    if (id > 0)
                    {
                        TempData["SuccessMessage"] = "Application submitted successfully!";
                        //return RedirectToAction("FetchAllCreditApplicationList");
                    }
                    else
                    {
                        ViewData["Response"] = "Failed to create credit application";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["Response"] = "An error occurred: " + ex.Message;
            }

            // Retrieve Purpose and Status options again for redisplaying the form
            IEnumerable<PurposeMaster> purposes = _creditBo.GetPurposeOptions();
            ViewBag.Purposes = purposes;

            IEnumerable<CreditApplicationStatusMaster> statuses = _creditBo.GetStatusOptions();
            ViewBag.Statuses = statuses;

            return View("Views/Credit/Create.cshtml", creditApplication);
        }
        public ActionResult FetchAllCreditDecisionList()
        {
            IEnumerable<CreditDecision> creditDecisionsList = _creditBo.FetchAllCreditDecisionList();
            return View("Views/Credit/FetchAllCreditDecisionList.cshtml", creditDecisionsList);
        }


        public ActionResult FetchAllCreditApplicationList()
        {
            IEnumerable<CreditApplication> creditApplications = _creditBo.FindCreditApplicationDetailsIdForUpdate();
            return View("Views/Credit/FetchAllCreditApplicationList.cshtml", creditApplications);
        }

        public ActionResult FilterByStatus(string creditApplicationStatus)
        {
            try
            {
                IEnumerable<CreditApplication> filteredApplications = _creditBo.FilterApplicaionByStatus(creditApplicationStatus);
                ViewData["ApplicationStatus"] = creditApplicationStatus;
                if (filteredApplications.Any())
                {
                    TempData["SuccessMessage"] = "Credit applications filtered successfully!";
                }
                else
                {
                    TempData["SuccessMessage"] = "No credit applications found for the specified status.";
                }
                return View("Views/Credit/FilteredApplications.cshtml", filteredApplications);
            }
            catch (Exception ex)
            {
                ViewData["Response"] = "An error occurred: " + ex.Message;
                return View("Views/Shared/Error.cshtml");
            }
        }

        public ActionResult DisplayCustomerCreditScore(decimal monthlyIncome, decimal expenses)
        {
            try
            {
                IEnumerable<FinancialInformation> customerCreditScores = _creditBo.DisplayCustomerWithCreditScore(monthlyIncome, expenses);
                return View("Views/Credit/CustomerCreditScores.cshtml", customerCreditScores);
            }
            catch (Exception ex)
            {
                ViewData["Response"] = "An error occurred: " + ex.Message;
                return View("Views/Shared/Error.cshtml");
            }
        }

        public ActionResult DisplayCustomersWithFinancialInfo()
        {
            try
            {
                IEnumerable<Customer> customersWithFinancialInfo = _creditBo.DisplayCustomerWithFinancialInformation();
                return View("Views/Credit/CustomersWithFinancialInfo.cshtml", customersWithFinancialInfo);
            }
            catch (Exception ex)
            {
                ViewData["Response"] = "An error occurred: " + ex.Message;
                return View("Views/Shared/Error.cshtml");
            }
        }

        public ActionResult UpdateCreditApplication(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("FetchAllCreditApplicationList");
                }

                CreditApplication creditApplication = _creditBo.FindCreditApplicationByIdForUpdate(id.Value);

                if (creditApplication == null)
                {
                    TempData["ErrorMessage"] = $"Credit application with ID {id} not found.";
                    return RedirectToAction("FetchAllCreditApplicationList");
                }

                return View("Views/Credit/Update.cshtml", creditApplication);
            }
            catch (Exception ex)
            {
                ViewData["Response"] = "An error occurred: " + ex.Message;
                return View("Views/Shared/Error.cshtml");
            }
        }

     /*   [HttpPost]
        [ValidateAntiForgeryToken]*/
		/* public ActionResult Update([Bind("ApplicationId,CustomerId,RequestedAmount,Purpose,ApplicationDate,CreditApplicationStatus")] CreditApplication creditApplication)
		 {
			 try
			 {
				 if (ModelState.IsValid)
				 {
					 int id = _creditBo.UpdateCreditApplication(creditApplication);

					 if (id > 0)
					 {
						 ViewData["Response"] = "Credit application updated successfully";
						 return RedirectToAction("FetchAllCreditApplicationList");
					 }
					 else
					 {
						 ViewData["Response"] = "Failed to update credit application";
					 }
				 }
			 }
			 catch (Exception ex)
			 {
				 ViewData["Response"] = "An error occurred: " + ex.Message;
			 }
			 return View("Views/Credit/Update.cshtml", creditApplication);
		 }*/

		[HttpPost]
		public ActionResult UpdateCreditApplication(CreditApplication creditApplication)
		{
			try
			{
				int result = _creditBo.UpdateCreditApplication(creditApplication);
				if (result > 0)
				{
					TempData["SuccessMessage"] = "Credit application updated successfully!";
				}
				else
				{
					TempData["SuccessMessage"] = "No changes made or an error occurred during update.";
				}
				return RedirectToAction("FetchAllCreditApplicationList");
			}
			catch (Exception ex)
			{
				ViewData["Response"] = "An error occurred: " + ex.Message;
				return View("Views/Shared/Error.cshtml");
			}
		}



		public ActionResult FindByApplicationId(int? applicationId)
        {
            try
            {
                if (applicationId == null)
                {
                    return RedirectToAction("Index");
                }

                CreditApplication creditApplication = _creditBo.FindCreditApplicationByIdForUpdate(applicationId.Value);

                if (creditApplication == null)
                {
                    TempData["ErrorMessage"] = $"Credit application with ID {applicationId} not found.";
                    return RedirectToAction("Index");
                }

                return View("Views/Credit/Details.cshtml", creditApplication);
            }
            catch (Exception ex)
            {
                ViewData["Response"] = "An error occurred: " + ex.Message;
                return View("Views/Shared/Error.cshtml");
            }
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User newUser)
        {
            if (_creditBo.RegisterUser(newUser))
            {
                // Successful registration
                return RedirectToAction("Login");
            }

            // Handle registration failure
            ModelState.AddModelError("", "Username or Email already exists");
            return View(newUser);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUser model)
        {
            if (ModelState.IsValid)
            {
                var isValid = _creditBo.LoginUser(model.Username, model.Password);
                if (isValid)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("","Invalid username or password");
            }
            return View(model);
        }


        public ActionResult Logout()
        {
          return RedirectToAction("Login");
        }
    }
}
