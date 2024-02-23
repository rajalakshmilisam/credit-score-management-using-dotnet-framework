using Microsoft.AspNetCore.Mvc;
using CreditApplicationMVCProject.Bo;
using CreditApplicationMVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreditApplicationMVCProject.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class CreditApiController : ControllerBase
	{
		private readonly CreditBo _creditBo;

		public CreditApiController(CreditBo creditBo)
		{
			_creditBo = creditBo;
		}

		// GET: api/CreditApi
		[HttpGet]
		public ActionResult<IEnumerable<Customer>> GetAllCustomerList()
		{
			try
			{
				IEnumerable<Customer> customerList = _creditBo.ListOfAllCustomers();
				return Ok(customerList);
			}
			catch (Exception ex)
			{
				return BadRequest("An error occurred: " + ex.Message);
			}
		}

		// GET: api/CreditApi/5
		/*[HttpGet("{id}")]
		public ActionResult<CreditDecision> GetCreditDecisionBYApplicationId(int id)
		{
			try
			{
				CreditDecision creditDecision = _creditBo.ViewDecisionByID(id);

				if (creditDecision == null)
				{
					return NotFound($"Credit application with ID {id} not found.");
				}

				return Ok(creditDecision);
			}
			catch (Exception ex)
			{
				return BadRequest("An error occurred: " + ex.Message);
			}
		}*/

		// POST: api/CreditApi/AddCreditApplication
		[HttpPost("AddCreditApplication")]
		public ActionResult<IEnumerable<CreditApplication>> AddCreditApplication(CreditApplication creditApplication)
		{
			try
			{
				int applicationId = _creditBo.CreateCreditApplication(creditApplication);

				if (applicationId > 0)
				{
					return Ok(new { Status = "Success", Message = "Credit application added successfully", ApplicationId = applicationId });
				}
				else
				{
					return BadRequest(new { Status = "Error", Message = "Failed to add credit application" });
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Status = "Error", Message = "Internal server error", Exception = ex.Message });
			}
		}

		// PUT: api/CreditApi/UpdateCreditApplication/5
		/*[HttpPut("UpdateCreditApplication/{id}")]
		public ActionResult UpdateCreditApplication(int id, CreditApplication creditApplication)
		{
			try
			{
				int affectedRows = _creditBo.UpdateCreditApplication(creditApplication);

				if (affectedRows > 0)
				{
					return Ok(new { Status = "Success", Message = "Credit application updated successfully", UpdatedRows = affectedRows });
				}
				else
				{
					return BadRequest(new { Status = "Error", Message = "Failed to update credit application" });
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Status = "Error", Message = "Internal server error", Exception = ex.Message });
			}
		}*/

		// GET: api/CreditApi/ViewDecisionByApplicationID/5
		/*[HttpGet("ViewDecisionByApplicationID/{applicationID}")]
		public ActionResult ViewDecisionByApplicationID(int? applicationID)
		{
			try
			{
				if (applicationID == null)
				{
					return BadRequest("Invalid application ID");
				}

				CreditDecision creditDecision = _creditBo.ViewDecisionByID(applicationID.Value);

				if (creditDecision == null)
				{
					return NotFound($"Credit decision for application ID {applicationID} not found.");
				}

				return Ok(creditDecision);
			}
			catch (Exception ex)
			{
				return BadRequest("An error occurred: " + ex.Message);
			}
		}*/

		// GET: api/CreditApi/FilterByStatus?creditApplicationStatus=Pending
		[HttpGet("FilterByStatus")]
		public ActionResult<IEnumerable<CreditApplication>> FilterByStatus(string creditApplicationStatus)
		{
			try
			{
				IEnumerable<CreditApplication> filteredApplications = _creditBo.FilterApplicaionByStatus(creditApplicationStatus);
				if (filteredApplications.Any())
				{
					return Ok(filteredApplications);
				}
				else
				{
					return NotFound($"No credit applications found for the specified status: {creditApplicationStatus}");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("An error occurred: " + ex.Message);
			}
		}

		// GET: api/CreditApi/DisplayCustomerCreditScore?monthlyIncome=5000.0&expenses=2000.0
		[HttpGet("DisplayCustomerCreditScore")]
		public ActionResult<IEnumerable<FinancialInformation>> DisplayCustomerCreditScore(decimal monthlyIncome, decimal expenses)
		{
			try
			{
				IEnumerable<FinancialInformation> customerCreditScores = _creditBo.DisplayCustomerWithCreditScore(monthlyIncome, expenses);
				return Ok(customerCreditScores);
			}
			catch (Exception ex)
			{
				return BadRequest("An error occurred: " + ex.Message);
			}
		}

		// GET: api/CreditApi/DisplayCustomersWithFinancialInfo
		[HttpGet("DisplayCustomersWithFinancialInfo")]
		public ActionResult<IEnumerable<Customer>> DisplayCustomersWithFinancialInfo()
		{
			try
			{
				IEnumerable<Customer> customersWithFinancialInfo = _creditBo.DisplayCustomerWithFinancialInformation();
				return Ok(customersWithFinancialInfo);
			}
			catch (Exception ex)
			{
				return BadRequest("An error occurred: " + ex.Message);
			}
		}
	}
}
