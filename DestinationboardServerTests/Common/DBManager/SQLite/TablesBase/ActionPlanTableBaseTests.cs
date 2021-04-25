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
    public class ActionPlanTableBaseTests
    {

        public static ActionPlanTableBase SampleData(string action_id, string action_name, string destination_id, string destination_name,
            DateTime from_date, string memo, string staff_id, string staff_name, int status, DateTime to_date)
        {
            ActionPlanTableBase sample = new ActionPlanTableBase();
            sample.ActionID = action_id;
            sample.ActionName = action_name;
            sample.DestinationID = destination_id;
            sample.DestinationName = destination_name;
            sample.FromTime = from_date;
            sample.Memo = memo;
            sample.StaffID = staff_id;
            sample.StaffName = staff_name;
            sample.Status = status;
            sample.ToTime = to_date;

            return sample;
        }

        public static ActionPlanTableBase TestData1()
        {
            return SampleData("action_id", "action_name", "destination_id", "detination_name", DateTime.Today,
                "memo", "staff_id", "staff_name", -1, DateTime.Today);
        }
        public static ActionPlanTableBase TestData2()
        {
            return SampleData("action_id2", "action_name", "destination_id2", "detination_name2", DateTime.Today.AddDays(1),
                "memo2", "staff_id", "staff_name2", -2, DateTime.Today.AddDays(1));
        }


        [TestMethod()]
        public void CopyTest()
        {
            Type t = typeof(ActionPlanTableBase);
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var test = new ActionPlanTableBase();
            var check = new ActionPlanTableBase();

            // データのコピー処理
            test.Copy(TestData1());

            // プロパティのチェック
            CommonTestUtilities.PropCheck<ActionPlanTableBase>(test, check, false);
        }

        [TestMethod()]
        public void InsertTest()
        {
            var test = TestData1();

            ActionPlanTableBase.Delete(test);
            ActionPlanTableBase.Insert(test);
            var tmp = ActionPlanTableBase.Select();

            var check = (from x in tmp
                         where x.StaffID.Equals(test.StaffID)
                         select x).FirstOrDefault();

            // プロパティのチェック
            CommonTestUtilities.PropCheck<ActionPlanTableBase>(test, check, true);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var test = TestData1();
            var test2 = TestData2();

            ActionPlanTableBase.Delete(test);
            ActionPlanTableBase.Insert(test);
            ActionPlanTableBase.Update(test, test2);
            var tmp = ActionPlanTableBase.Select();

            var check = (from x in tmp
                         where x.StaffID.Equals(test2.StaffID)
                         select x).FirstOrDefault();

            // プロパティのチェック
            CommonTestUtilities.PropCheck<ActionPlanTableBase>(test2, check, true);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var test = TestData1();
            ActionPlanTableBase.Delete(test);
            ActionPlanTableBase.Insert(test);
            ActionPlanTableBase.Delete(test);
            var tmp = ActionPlanTableBase.Select();

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