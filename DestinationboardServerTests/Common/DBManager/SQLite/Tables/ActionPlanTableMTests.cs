using Microsoft.VisualStudio.TestTools.UnitTesting;
using DestinationboardServer.Common.DBManager.SQLite.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinationboardServer.Common.DBManager.SQLite.TablesBase.Tests;
using DestinationboardServer.Common.DBManager.SQLite.TablesBase;
using DestinationboardServerTests.Common.DBManager.SQLite;

namespace DestinationboardServer.Common.DBManager.SQLite.Tables.Tests
{
    [TestClass()]
    public class ActionPlanTableMTests
    {
        [TestMethod()]
        public void StaffActionPlanSelectTest()
        {
            var check = ActionPlanTableBaseTests.TestData1();

            ActionPlanTableBase.Delete(check);
            ActionPlanTableBase.Insert(check);

            var test = ActionPlanTableM.StaffActionPlanSelect(check.StaffID);

            // プロパティのチェック
            CommonTestUtilities.PropCheck<ActionPlanTableBase>(test, check, true);
        }

        [TestMethod()]
        public void StaffActionPlanUpdateTest()
        {
            var check = ActionPlanTableBaseTests.TestData1();

            ActionPlanTableBase.Delete(check);
            ActionPlanTableBase.Insert(check);
            var req_data = ActionPlanTableM.TableToRequest(check);

            bool ret = ActionPlanTableM.StaffActionPlanUpdate(req_data);

            // 成功したかどうかの判定
            Assert.IsTrue(ret);

            var test = ActionPlanTableM.StaffActionPlanSelect(check.StaffID);

            // プロパティのチェック
            CommonTestUtilities.PropCheck<ActionPlanTableBase>(test, check, true);
        }
    }
}