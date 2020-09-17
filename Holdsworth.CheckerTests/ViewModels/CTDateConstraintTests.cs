using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cardan.PlanChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESAPIX.Constraints;

namespace Cardan.PlanChecker.Tests
{
    [TestClass()]
    public class CTDateConstraintTests
    {
        [TestMethod()]
        public void CTDateConstraintFailsCorrectly()
        {
            //Arrange
            var ctDate = DateTime.Now.AddDays(-61);
            //Act
            var result = new CTDateConstraint().Constrain(ctDate).ResultType;
            //Assert
            Assert.AreEqual(ResultType.ACTION_LEVEL_3, result);
        }

        [TestMethod()]
        public void CTDateConstraintPassesCorrectly()
        {
            //Arrange
            var ctDate = DateTime.Now.AddDays(-59);
            //Act
            var result = new CTDateConstraint().Constrain(ctDate).ResultType;
            //Assert
            Assert.AreEqual(ResultType.PASSED, result);
        }
    }
}