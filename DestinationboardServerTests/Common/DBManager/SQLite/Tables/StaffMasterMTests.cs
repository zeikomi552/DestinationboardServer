using Microsoft.VisualStudio.TestTools.UnitTesting;
using DestinationboardServer.Common.DBManager.SQLite.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinationboardServer.Common.DBManager.SQLite.TablesBase;
using DestinationboardServer.Common.DBManager.SQLite.TablesBase.Tests;
using DestinationboardServerTests.Common.DBManager.SQLite;

namespace DestinationboardServer.Common.DBManager.SQLite.Tables.Tests
{
    [TestClass()]
    public class StaffMasterMTests
    {
        [TestMethod()]
        public void SelectTest()
        {
            var test = StaffMasterBaseTests.TestData1();

            StaffMasterBase.Delete(test);
            StaffMasterBase.Insert(test);

            var tmp = StaffMasterM.Select(test.StaffID);

            // プロパティのチェック
            CommonTestUtilities.PropCheck<StaffMasterBase>(test, tmp, true);
        }

        [TestMethod()]
        public void UpdateListTest()
        {
            RegistStaffRequest request = new RegistStaffRequest();
            for (int index = 0; index < 100; index++)
            {
                var row_data = StaffMasterBaseTests.SampleData("staff_id" + index.ToString(), index, "staff_name" + index.ToString(), true,
                    DateTime.Today.AddDays(index), "create_user" + index.ToString(), "qr_code" + index.ToString(), "felica_id" + index.ToString());

                var req = StaffMasterM.TableToRequest(row_data);
                request.StaffInfoList.Add(req);
            }


            StaffMasterM.UpdateList(request);

            var staff_list = StaffMasterBase.Select();

            foreach (var tmp in request.StaffInfoList)
            {
                var check_tmp = StaffMasterM.RequestToTable(tmp);
                var test_tmp = (from x in staff_list
                                where x.StaffID.Equals(tmp.StaffID)
                                select x).FirstOrDefault();
                // プロパティのチェック
                CommonTestUtilities.PropCheck<StaffMasterBase>(test_tmp, check_tmp, true);
            }

        }
    }
}