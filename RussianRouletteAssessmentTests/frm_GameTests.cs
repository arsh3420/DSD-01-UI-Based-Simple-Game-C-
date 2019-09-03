using Microsoft.VisualStudio.TestTools.UnitTesting;
using RussianRouletteAssessment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RussianRouletteAssessment.Tests
{
    [TestClass()]
    public class frm_GameTests
    {
        [TestMethod()]
        public void Test_Game_Die()
        {
            frm_Game Game = new frm_Game();
            try
            {
                Game.Game_Die();
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Error prone test");
                Assert.Fail("Test failed");
                return;
            }
            
        }

        [TestMethod()]
        public void Test_Game_Survived()
        {
            frm_Game Game = new frm_Game();
            try
            {
                Game.Game_Survived();
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Error prone test");
                Assert.Fail("Test failed");
                return;
            }
        }
    }
}