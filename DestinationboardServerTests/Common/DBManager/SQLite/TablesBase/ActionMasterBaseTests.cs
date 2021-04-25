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
    public class ActionMasterBaseTests
    {
        [TestMethod()]
        public void ActionMasterBaseTest()
        {
        }

        [TestMethod()]
        public void ActionMasterBaseTest1()
        {
        }

        public static ActionMasterBase SampleData(string action_id, string action_name, DateTime create_date,
            string create_user, int sort_order, DateTime update_date, string update_user)
        {
            ActionMasterBase sample = new ActionMasterBase();
            sample.ActionID = action_id;
            sample.ActionName = action_name;
            sample.CreateDate = create_date;
            sample.CreateUser = create_user;
            sample.SortOrder = sort_order;
            sample.UpdateDate = update_date;
            sample.UpdateUser = update_user;
            return sample;
        }

        public static ActionMasterBase TestData1()
        {
            return SampleData("action_id", "action_name", DateTime.Today, "create_user", -1000, DateTime.Today, "update_user");
        }
        public static ActionMasterBase TestData2()
        {
            return SampleData("action_id", "action_name2", DateTime.Today.AddDays(1), "test1", -1001, DateTime.Today.AddDays(1), "test1");
        }


        #region コピー処理が機能しているかどうかの確認
        /// <summary>
        /// コピー処理が機能しているかどうかの確認
        /// コピー前とコピー後で初期値以外にちゃんと変わっていなければNGとしている
        /// </summary>
        [TestMethod()]
        public void CopyTest()
        {
            Type t = typeof(ActionMasterBase);
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var test = new ActionMasterBase();
            var check = new ActionMasterBase();

            // データのコピー処理
            test.Copy(TestData1());

            // プロパティのチェック
            CommonTestUtilities.PropCheck<ActionMasterBase>(test, check, false);
        }
        #endregion



        [TestMethod()]
        public void InsertTest()
        {
            var test = TestData1();

            ActionMasterBase.Delete(test);
            ActionMasterBase.Insert(test);
            var tmp = ActionMasterBase.Select();

            var check = (from x in tmp
                         where x.ActionID.Equals(test.ActionID)
                         select x).FirstOrDefault();

            // プロパティのチェック
            CommonTestUtilities.PropCheck<ActionMasterBase>(test, check, true);

        }

        [TestMethod()]
        public void UpdateTest()
        {
            var test = TestData1();
            var test2 = TestData2();

            ActionMasterBase.Delete(test);
            ActionMasterBase.Insert(test);
            ActionMasterBase.Update(test, test2);
            var tmp = ActionMasterBase.Select();

            var check = (from x in tmp
                         where x.ActionID.Equals(test2.ActionID)
                         select x).FirstOrDefault();

            // プロパティのチェック
            CommonTestUtilities.PropCheck<ActionMasterBase>(test2, check, true);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var test = TestData1();
            ActionMasterBase.Delete(test);
            ActionMasterBase.Insert(test);
            ActionMasterBase.Delete(test);
            var tmp = ActionMasterBase.Select();

            var check = (from x in tmp
                         where x.ActionID.Equals(test.ActionID)
                         select x).FirstOrDefault();

            if (check != null)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void SelectTest()
        {
            // Insertのテストと同じのため省略
        }
    }
}