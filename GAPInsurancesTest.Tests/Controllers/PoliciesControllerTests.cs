using Microsoft.VisualStudio.TestTools.UnitTesting;
using GAPInsurancesTest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPInsurancesTest.Models;
using GAPInsurancesTest.Repositories;
using GAPInsurancesTest.Tests.Repositories;

namespace GAPInsurancesTest.Controllers.Tests
{
    [TestClass()]
    public class PoliciesControllerTests
    {
        private PoliciesController policiesController;
        public PoliciesControllerTests()
        {
            this.policiesController = new PoliciesController(new MockPoliciesRepository());
        }

        [TestMethod()]
        public async Task PostPolicyTest1()
        {
            Policy p = new Policy
            {
                coverage_percent = 0
            };

            await this.policiesController.PostPolicy(p);
        }

        [TestMethod()]
        public async Task PostPolicyTest2()
        {
            Policy p = new Policy
            {
                risk_type_id = 4,
                coverage_percent = 67
            };

            await this.policiesController.PostPolicy(p);
        }

        [TestMethod()]
        public void GetPoliciesTest()
        {
            List<Policy> policies = this.policiesController.GetPolicies();
            Assert.IsNotNull(policies);
            Assert.IsTrue(policies.Count() > 0);
        }

        [TestMethod()]
        public void GetPolicyTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PutPolicyTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "Policy price and coverage percent must be greater than 0")]
        public void PostPolicyTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeletePolicyTest()
        {
            Assert.Fail();
        }
    }
}