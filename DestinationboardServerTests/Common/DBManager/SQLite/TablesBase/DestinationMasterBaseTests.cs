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
    public class DestinationMasterBaseTests
    {
        public static DestinationMasterBase SampleData(string destination_id, int sort_order, string destination_name, string action_id,
            DateTime create_date, string create_user, DateTime update_date, string update_user)
        {
            DestinationMasterBase sample = new DestinationMasterBase();
            sample.DestinationID = destination_id;
            sample.SortOrder = sort_order;
            sample.DestinationName = destination_name;
            sample.ActionID = action_id;
            sample.CreateDate = create_date;
            sample.CreateUser = create_user;
            sample.UpdateDate = update_date;
            sample.UpdateUser = update_user;

            return sample;
        }
        public static DestinationMasterBase TestData1()
        {
            return SampleData("destination_id", -1, "destination_name", "action_id", DateTime.Today,
                "create_user", DateTime.Today, "update_user");
        }
        public static DestinationMasterBase TestData2()
        {
            return SampleData("destination_id", -2, "destination_name2", "action_id2", DateTime.Today.AddDays(1),
                "create_user2", DateTime.Today.AddDays(1), "update_user2");
        }


        [TestMethod()]
        public void DestinationMasterBaseTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void DestinationMasterBaseTest1()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void CopyTest()
        {
            Type t = typeof(DestinationMasterBase);
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var test = new DestinationMasterBase();
            var check = new DestinationMasterBase();

            // データのコピー処理
            test.Copy(TestData1());

            // プロパティのチェック
            CommonTestUtilities.PropCheck<DestinationMasterBase>(test, check, false);
        }

        [TestMethod()]
        public void InsertTest()
        {
            var test = TestData1();

            DestinationMasterBase.Delete(test);
            DestinationMasterBase.Insert(test);
            var tmp = DestinationMasterBase.Select();

            var check = (from x in tmp
                         where x.DestinationID.Equals(test.DestinationID)
                         select x).FirstOrDefault();

            // プロパティのチェック
            CommonTestUtilities.PropCheck<DestinationMasterBase>(test, check, true);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var test = TestData1();
            var test2 = TestData2();

            DestinationMasterBase.Delete(test);
            DestinationMasterBase.Insert(test);
            DestinationMasterBase.Update(test, test2);
            var tmp = DestinationMasterBase.Select();

            var check = (from x in tmp
                         where x.DestinationID.Equals(test2.DestinationID)
                         select x).FirstOrDefault();

            // プロパティのチェック
            CommonTestUtilities.PropCheck<DestinationMasterBase>(test2, check, true);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var test = TestData1();
            DestinationMasterBase.Delete(test);
            DestinationMasterBase.Insert(test);
            DestinationMasterBase.Delete(test);
            var tmp = DestinationMasterBase.Select();

            var check = (from x in tmp
                         where x.DestinationID.Equals(test.DestinationID)
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