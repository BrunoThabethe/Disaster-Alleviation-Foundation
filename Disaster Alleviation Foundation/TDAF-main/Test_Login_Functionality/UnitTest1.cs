using Microsoft.VisualStudio.TestTools.UnitTesting;
using DisasterAlleviationFoundation.Logic;

namespace Test_Login_Functionality
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void test_If_Username_and_Password_Match_DBValues()
        {
            //arrange
            //Essential test values
            var userU = "admin";
            var userP = "123";
            applicationHelperClass n = new applicationHelperClass();
            //Act
            //Used in login, this method determines whether the inserted values match the ones in the database
            //returns true if they match
            var user = n.UsernameAndPassMatch(Users.DbDummyUsernameTwo, Users.DbDummyPasswordTwo, userU, userP);

            //Assert
            //Unit testing the method
            Assert.IsTrue(user);
        }

        [TestMethod]
        public void Value_Less_Than_Available_Amount()
        {
            //arrange

            //Essential Test values
            var DBAvailableMonetary = 11500;
            var UserMonetary = 11500;
            applicationHelperClass applicationHelper = new applicationHelperClass();

            //Act
            //this tests that the method that determines whether the entered value is either than available money is working or not
            //the method is generally used in the allocate funds page and purchase goods page
            var helper = applicationHelper.isAmountLessThan(DBAvailableMonetary, UserMonetary);

            //Assert
            Assert.IsTrue(helper);
        }
        [TestMethod]
        public void CheckIfInsertedValuesAreNotEmpty()
        {
            //arrange
            //test values
            var value1 ="Test Data Value1";
            var value2 ="Test Data Value2";
            var value3 ="Test Data Value3";
            var value4 ="Test Data Value4";
            
            applicationHelperClass applicationHelper = new applicationHelperClass();

            //Act
            //this tests that the method that determines whether the form values are entered is working or not
            var helper = applicationHelper.isNotEmpty(value1, value2, value3, value4);

            //Assert

            Assert.IsTrue(helper);
        }
    }
    
    }
