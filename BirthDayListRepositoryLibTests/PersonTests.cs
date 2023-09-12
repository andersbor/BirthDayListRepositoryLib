using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BirthDayListRepositoryLib.Tests
{
    [TestClass()]
    public class PersonTests
    {
        private readonly Person thomas = new() { Id = 1, Name = "Thomas", BirthYear = 2006, BirthMonth = 7, BirthDayOfMonth = 7 };
        private readonly Person svend = new() { Id = 2, Name = "Svend", BirthYear = 1942, BirthMonth = 12, BirthDayOfMonth = 30 };
        private readonly Person illegalMonth = new() { Id = 3, Name = "Bo", BirthYear = 2023, BirthMonth = 13, BirthDayOfMonth = 1 };
        private readonly Person leap = new() { Id = 4, Name = "An", BirthYear = 2004, BirthMonth = 2, BirthDayOfMonth = 29 };
        private readonly Person noLeap = new() { Id = 4, Name = "An", BirthYear = 2005, BirthMonth = 2, BirthDayOfMonth = 29 };


        [TestMethod()]
        public void ToStringTest()
        {
            //Assert.AreEqual("1 Thomas 115225492 1", p.ToString());
        }

        [TestMethod]
        public void TestValidate()
        {
            thomas.Validate();
            svend.Validate();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => illegalMonth.Validate());
            leap.Validate();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => noLeap.Validate());
        }

        [TestMethod]
        public void AgeTest()
        {
            Assert.AreEqual(17, thomas.Age);
            Assert.AreEqual(80, svend.Age);
        }
    }
}