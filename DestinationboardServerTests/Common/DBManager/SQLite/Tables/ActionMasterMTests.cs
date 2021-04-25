using Microsoft.VisualStudio.TestTools.UnitTesting;
using DestinationboardServer.Common.DBManager.SQLite.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinationboardServer.Common.DBManager.SQLite.TablesBase.Tests;
using DestinationboardServerTests.Common.DBManager.SQLite;
using DestinationboardServer.Common.DBManager.SQLite.TablesBase;

namespace DestinationboardServer.Common.DBManager.SQLite.Tables.Tests
{
    [TestClass()]
    public class ActionMasterMTests
    {
        [TestMethod()]
        public void TableToReplyTest()
        {
            var test = ActionMasterBaseTests.TestData1();

            var grpc_obj  = ActionMasterM.TableToReply(test);

            CommonTestUtilities.gRPCPropCheck<ActionMasterBase, ActionMasterReply>(test, grpc_obj, true);
        }

        [TestMethod()]
        public void ReplyToTableTest()
        {
            var test = ActionMasterBaseTests.TestData1();

            var grpc_obj = ActionMasterM.TableToReply(test);

            test = ActionMasterM.ReplyToTable(grpc_obj);

            CommonTestUtilities.gRPCPropCheck<ActionMasterBase, ActionMasterReply>(test, grpc_obj, true);
        }

        [TestMethod()]
        public void TableToRequestTest()
        {
            var test = ActionMasterBaseTests.TestData1();

            var grpc_obj = ActionMasterM.TableToRequest(test);

            CommonTestUtilities.gRPCPropCheck<ActionMasterBase, ActionMasterRequest>(test, grpc_obj, true);
        }

        [TestMethod()]
        public void RequestToTableTest()
        {
            var test = ActionMasterBaseTests.TestData1();

            var grpc_obj = ActionMasterM.TableToRequest(test);

            test = ActionMasterM.RequestToTable(grpc_obj);

            CommonTestUtilities.gRPCPropCheck<ActionMasterBase, ActionMasterRequest>(test, grpc_obj, true);
        }

        [TestMethod()]
        public void UpdateListTest()
        {
            RegistActionsRequest tmp = new RegistActionsRequest();

            for (int index = 0; index < 100; index++)
            {
                var test = ActionMasterBaseTests.SampleData(
                    "action_id" + index.ToString(), "action_name" + index.ToString(), DateTime.Today.AddDays(index), 
                    "create_user" + index.ToString(), index, DateTime.Today.AddDays(index), "update_user" + index.ToString());

                for (int index2 = 0; index2 < 100; index2++)
                {
                    var dest = DestinationMasterBaseTests.SampleData(
                        "destination_id" + index.ToString("000") + index2.ToString("000"), index, "destination_name" + index2.ToString(), "action_id" + index.ToString(),
                        DateTime.Today.AddDays(index2), "create_user" + index2.ToString(), DateTime.Today.AddDays(index2), "update_user" + index2.ToString());
                    tmp.DestinationMasterList.Add(ActionMasterM.TableToRequest(dest));
                }
                tmp.ActionMasterList.Add(ActionMasterM.TableToRequest(test));
            }

            Assert.IsTrue(ActionMasterM.UpdateList(tmp));
        }
    }
}