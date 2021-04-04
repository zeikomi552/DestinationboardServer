using DestinationboardCommunicationLibrary.Communication;
using DestinationboardServer.Common.DBManager.SQLite.Tables;
using DestinationboardServer.Common.DBManager.SQLite.TablesBase;
using System;

namespace DestinationboardServer
{
    class Program
    {
        public event EventHandler RecieveStaff;

        static void Main(string[] args)
        {
            DestinationbardCommunicationAPIService service 
                = new DestinationbardCommunicationAPIService("127.0.0.1", 552);

            service.RecieveRegstStaffEvent += Service_RecieveRegstStaffEvent;


            service.Listen();

            Console.ReadLine();


        }

        private static void Service_RecieveRegstStaffEvent(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            RegstStaffRequest request = ((EventArgsRcv)e).Request as RegstStaffRequest;
            RegstStaffReply reply = ((EventArgsRcv)e).Replay as RegstStaffReply;

            try
            {
                StaffMasterM.UpdateList(request);

            }
            catch (Exception ex)
            {
                reply.ErrorCode = 2;

            }

        }
    }
}
