using CreditApplicationMVCProject.Models;

namespace CreditApplicationMVCProject.Repository
{
    public interface ICreditRepository
    {   //IEnumerable usage:- Returning list of records..It is an interface
        //FetchAllCustomer List for the admin to view the customer datas easily...
        IEnumerable<Customer> ListOfAllCustomers();
        //Create new application for customers to enter the details of application....
        int CreateCreditApplication(CreditApplication creditApplication);
        // We can find the application by its id 
        CreditApplication FindCreditApplicationById(int applicationId);
        // We can update those applications which have created before and return it to the list of applications page...
        int UpdateCreditApplication(CreditApplication creditApplication);
        // Here displaying the list of application's decisions which is pending, approved or denied...
        IEnumerable<CreditDecision> FetchAllCreditDecisionsList();
        // Using joins - Displaying Customer details with their financial information, which is easy to manage as an admin...
        IEnumerable<Customer> DisplayCustomerWithFinancialInformation();
        // Filter application by status - Filter those applications by its status, easy managable for admin...
        IEnumerable<CreditApplication> FilterApplicaionByStatus(string creditApplicationStatus);
        // Filter using linq query by passing parameters
        // Here, we can view the credit scores of the customers who can eligible to apply for the loan...
        IEnumerable<FinancialInformation> DisplayCustomerWithCreditScore(decimal monthlyIncome, decimal expenses);
        IEnumerable<CreditApplication> FindCreditApplicationDetailsIdForUpdate();
        User GetUserByUsername(string username);
        User GetUserByEmail(string email);
        void AddUser(User user);
        bool ValidateUser(string username, string password);
        IEnumerable<PurposeMaster> GetPurposeOptions();
        IEnumerable<CreditApplicationStatusMaster> GetStatusOptions();
    }
}
