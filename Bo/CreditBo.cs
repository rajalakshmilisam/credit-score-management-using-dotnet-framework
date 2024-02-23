using CreditApplicationMVCProject.Models;
using CreditApplicationMVCProject.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreditApplicationMVCProject.Bo
{
    public class CreditBo
    {
        private readonly ICreditRepository _creditRepository;

        public CreditBo(ICreditRepository creditRepository)
        {
            _creditRepository = creditRepository;
        }

        public CreditBo() { }

        public int CreateCreditApplication(CreditApplication creditApplication)
        {
            int codeValue;
            try
            {
                codeValue = _creditRepository.CreateCreditApplication(creditApplication);
            }
            catch (Exception ex)
            {
                return -1;
            }
            return codeValue;
        }

        
        public IEnumerable<CreditDecision> FetchAllCreditDecisionList()
        {
            return _creditRepository.FetchAllCreditDecisionsList();
        }
        
        public IEnumerable<Customer> ListOfAllCustomers()
        {
            return _creditRepository.ListOfAllCustomers();
        }

        public IEnumerable<CreditApplication> FilterApplicaionByStatus(string creditApplicationStatus)
        {
            return _creditRepository.FilterApplicaionByStatus(creditApplicationStatus);
        }

        public IEnumerable<FinancialInformation> DisplayCustomerWithCreditScore(decimal monthlyIncome, decimal expenses)
        {
            return _creditRepository.DisplayCustomerWithCreditScore(monthlyIncome, expenses);
        }

        public IEnumerable<Customer> DisplayCustomerWithFinancialInformation()
        {
            return _creditRepository.DisplayCustomerWithFinancialInformation();
        }

        public IEnumerable<CreditApplication> FindCreditApplicationDetailsIdForUpdate()
        {
            return _creditRepository.FindCreditApplicationDetailsIdForUpdate();
        }

        public CreditApplication FindCreditApplicationByIdForUpdate(int applicationId)
        {
            return _creditRepository.FindCreditApplicationById(applicationId);
        }

        public int UpdateCreditApplication(CreditApplication creditApplication)
        {
            return _creditRepository.UpdateCreditApplication(creditApplication);
        }

        public bool RegisterUser(User user)
        {
            User existingUserByUsername = _creditRepository.GetUserByUsername(user.Username);
            User existingUserByEmail = _creditRepository.GetUserByEmail(user.Email);

            if (existingUserByUsername != null || existingUserByEmail != null)
            {
                // User with the same username or email already exists
                return false;
            }

            _creditRepository.AddUser(user);

            return true;
        }

        public bool LoginUser(string username, string password)
        {
            return _creditRepository.ValidateUser(username, password);
        }

        public IEnumerable<PurposeMaster> GetPurposeOptions()
        {
            return _creditRepository.GetPurposeOptions();
        }

        public IEnumerable<CreditApplicationStatusMaster> GetStatusOptions()
        {
            return _creditRepository.GetStatusOptions();
        }
    }
}
