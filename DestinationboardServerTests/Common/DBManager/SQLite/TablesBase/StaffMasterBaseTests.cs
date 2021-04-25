using Microsoft.VisualStudio.TestTools.UnitTesting;
using DestinationboardServer.Common.DBManager.SQLite.TablesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DestinationboardServerTests.Common.DBManager.SQLite;

namespace DestinationboardServer.Common.DBManager.SQLite.TablesBase.Tests
{
    [TestClass()]
    public class StaffMasterBaseTests
    {
        public static StaffMasterBase SampleData(string staff_id, int sort_order, string staff_name, bool display,
            DateTime create_date, string create_user)
        {
            StaffMasterBase sample = new StaffMasterBase();
            sample.StaffID = staff_id;
            sample.SortOrder = sort_order;
            sample.StaffName = staff_name;
            sample.Display = display;
            sample.CreateDate = create_date;
            sample.CreateUser = create_user;

            return sample;
        }

        public static StaffMasterBase TestData1()
        {
            return SampleData("staff_id", -1, "staff_name", true, DateTime.Today,
                "create_user");
        }
        public static StaffMasterBase TestData2()
        {
            return SampleData("staff_id", -2, "staff_name2", true, DateTime.Today.AddDays(1),
                "create_user2");
        }


        [TestMethod()]
        public void StaffMasterBaseTest()
        {
            Type t = typeof(StaffMasterBase);
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var test = new StaffMasterBase();
            var check = new StaffMasterBase();

            // データのコピー処理
            test.Copy(TestData1());

            // プロパティのチェック
            CommonTestUtilities.PropCheck<StaffMasterBase>(test, check, false);
        }

        [TestMethod()]
        public void StaffMasterBaseTest1()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void CopyTest()
        {
            var test = TestData1();

            StaffMasterBase.Delete(test);
            StaffMasterBase.Insert(test);
            var tmp = StaffMasterBase.Select();

            var check = (from x in tmp
                         where x.StaffID.Equals(test.StaffID)
                         select x).FirstOrDefault();

            // プロパティのチェック
            CommonTestUtilities.PropCheck<StaffMasterBase>(test, check, true);
        }

        [TestMethod()]
        public void InsertTest()
        {
            var test = TestData1();

            StaffMasterBase.Delete(test);
            StaffMasterBase.Insert(test);
            var tmp = StaffMasterBase.Select();

            var check = (from x in tmp
                         where x.StaffID.Equals(test.StaffID)
                         select x).FirstOrDefault();

            // プロパティのチェック
            CommonTestUtilities.PropCheck<StaffMasterBase>(test, check, true);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var test = TestData1();
            var test2 = TestData2();

            StaffMasterBase.Delete(test);
            StaffMasterBase.Insert(test);
            StaffMasterBase.Update(test, test2);
            var tmp = StaffMasterBase.Select();

            var check = (from x in tmp
                         where x.StaffID.Equals(test2.StaffID)
                         select x).FirstOrDefault();

            // プロパティのチェック
            CommonTestUtilities.PropCheck<StaffMasterBase>(test2, check, true);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var test = TestData1();
            StaffMasterBase.Delete(test);
            StaffMasterBase.Insert(test);
            StaffMasterBase.Delete(test);
            var tmp = StaffMasterBase.Select();

            var check = (from x in tmp
                         where x.StaffID.Equals(test.StaffID)
                         select x).FirstOrDefault();

            if (check != null)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void SelectTest()
        {
            // 省略
        }
    }
}