using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject1.WSVersion2;

namespace TestProject1
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            
            WSVersion2.WSCustomerInfo1 custInfo = new WSCustomerInfo1();
            custInfo.FirstName = "Bill";
            custInfo.LastName = "";
            custInfo.Street1 = "179 John St.";
            custInfo.City = "Toronto";
            custInfo.Province = "ON";
            custInfo.Country = "CA";
            custInfo.PostalCode = "M5T1X";
            custInfo.EmailAddress = "bad";

            WSVersion2.WSRequest request = new WSVersion2.WSRequest();
            request.CustomerInfo = custInfo;

            WSVersion2.WSResponse response = null;
            try
            {
                using (WSVersion2.Service client = new WSVersion2.Service())
                {
                    response = client.ProcessRequest(request);

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            Assert.IsTrue(response.ErrorCodes.Length > 0);
        }
    }
}
