using DestinationboardServer.Common.DBManager.SQLite.Tables;
using System;

namespace DestinationboardServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ActionPlanTable_M tmp = new ();
            tmp.StaffID = "SampleStaff";
            tmp.ActionID = "SampleActopm";
            tmp.ActionName = "SampleName";

            ActionPlanTable_M.Insert(tmp);



            ActionPlanTable_M tmp2 = new ActionPlanTable_M();
            tmp2.StaffID = "SampleStaff";
            tmp2.StaffName = "SampleStaff";
            tmp2.ActionID = "SampleActopm2";
            tmp2.ActionName = "SampleName";
            tmp2.DestinationID = "SampleName";
            tmp2.DestinationName = "SampleName";
            tmp2.FromTime = DateTime.Now;
            tmp2.ToTime = DateTime.Now;
            tmp2.Memo = "memomemomemomemo";

            ActionPlanTable_M.Update(tmp, tmp2);

            var sel = ActionPlanTable_M.Select();

            ActionPlanTable_M.Delete(tmp2);

        }
    }
}
