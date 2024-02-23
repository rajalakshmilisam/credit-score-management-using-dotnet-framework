using CreditApplicationMVCProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CreditApplicationMVCProject.Repository
{
	public class CreditRepository : ICreditRepository
	{
		//This variable is used to hold the data in the database instance
		private readonly CreditapplicationContext _creditapplicationContext;
		//Initializes the db context instance which it recieved as a argument

		public CreditRepository(CreditapplicationContext creditapplicationContext)
		{
			_creditapplicationContext = creditapplicationContext;
		}
		//used to initializing a context

		public CreditRepository()
		{
			_creditapplicationContext = new CreditapplicationContext();
		}
		public int CreateCreditApplication(CreditApplication creditApplication)
		{
			_creditapplicationContext.CreditApplications.Add(creditApplication);
			return _creditapplicationContext.SaveChanges();
		}

		public IEnumerable<Customer> ListOfAllCustomers()
		{
			return _creditapplicationContext.Customers.ToList();
		}
		public IEnumerable<CreditApplication> FilterApplicaionByStatus(string creditApplicationStatus)
		{
			/*            return _creditapplicationContext.CreditApplications.Where(ca => string.IsNullOrEmpty(creditApplicationStatus) *//*|| ca.CreditApplicationStatus == creditApplicationStatus*//*).ToList();
			*/
			return _creditapplicationContext.CreditApplications
			.Include(ca => ca.CreditApplicationStatusMasters)  // Include the CreditApplicationStatusMaster
			.Where(ca => string.IsNullOrEmpty(creditApplicationStatus) || ca.CreditApplicationStatusMasters.StatusName == creditApplicationStatus)
			.ToList();
		}
		public IEnumerable<FinancialInformation> DisplayCustomerWithCreditScore(decimal monthlyIncome, decimal expenses)
		{
			return _creditapplicationContext.FinancialInformations.Where(fi => fi.MonthlyIncome <= monthlyIncome && fi.Expenses <= expenses).ToList();
		}

		public IEnumerable<Customer> DisplayCustomerWithFinancialInformation()
		{
			var customersWithFinancialInfo = _creditapplicationContext.Customers
			.GroupJoin(
				_creditapplicationContext.FinancialInformations,
				customer => customer.CustomerId,
				financialInfo => financialInfo.CustomerId,
				(customer, financialInfos) => new
				{
					Customer = customer,
					FinancialInfos = financialInfos.ToList()
				}
			)
			.Select(result => new Customer
			{
				CustomerId = result.Customer.CustomerId,
				FirstName = result.Customer.FirstName,
				LastName = result.Customer.LastName,
				// Add other properties as needed
				FinancialInformations = result.FinancialInfos
			})
			.ToList();

			return customersWithFinancialInfo;
		}

		public IEnumerable<CreditDecision> FetchAllCreditDecisionsList()
		{
			return _creditapplicationContext.CreditDecisions.ToList();
		}

		public IEnumerable<CreditApplication> FindCreditApplicationDetailsIdForUpdate()
		{
			return _creditapplicationContext.CreditApplications.ToList();
		}

		public CreditApplication FindCreditApplicationById(int applicationId)
		{
			return _creditapplicationContext.CreditApplications.Find(applicationId);
		}

		public int UpdateCreditApplication(CreditApplication creditApplication)
		{
			_creditapplicationContext.Entry(creditApplication).State = EntityState.Modified;
			return _creditapplicationContext.SaveChanges();
		}

		public User GetUserByUsername(string username)
		{
			return _creditapplicationContext.Users.FirstOrDefault(u => u.Username == username);
		}

		public User GetUserByEmail(string email)
		{
			return _creditapplicationContext.Users.FirstOrDefault(u => u.Email == email);
		}

		public void AddUser(User user)
		{
			_creditapplicationContext.Users.Add(user);
			_creditapplicationContext.SaveChanges();
		}

		public bool ValidateUser(string username, string password)
		{
			// Validate user credentials (plaintext comparison, not recommended for production)
			User user = _creditapplicationContext.Users.FirstOrDefault(u => u.Username == username && u.NewPassword == password);
			return user != null;
		}
		public IEnumerable<PurposeMaster> GetPurposeOptions()
		{
			return _creditapplicationContext.PurposeMasters.ToList();
		}

		public IEnumerable<CreditApplicationStatusMaster> GetStatusOptions()
		{
			return _creditapplicationContext.CreditApplicationStatusMasters.ToList();
		}

	}
}
