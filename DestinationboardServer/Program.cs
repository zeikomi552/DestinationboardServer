using DestinationboardCommunicationLibrary.Communication;
using DestinationboardServer.Common;
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
                = new DestinationbardCommunicationAPIService(CommonValues.GetInstance().ServerName, CommonValues.GetInstance().Port);

            service.RecieveRegistStaffEvent += Service_RecieveRegstStaffEvent;
            service.RecieveGetRegistStaffEvent += Service_RecieveGetRegistStaffEvent;



            service.Listen();

            Console.ReadLine();


        }

        private static void Service_RecieveGetRegistStaffEvent(object sender, EventArgs e)
        {
            GetStaffsRequest request = ((gRPCArgsRcv)e).Request as GetStaffsRequest;
            GetStaffsReply reply = ((gRPCArgsRcv)e).Replay as GetStaffsReply;

            try
            {

                var list = StaffMasterM.Select();
                
                foreach (var item in list)
                {
                    StaffMasterReply tmp = new StaffMasterReply();
                    tmp.StaffID = item.StaffID;
                    tmp.StaffName = item.StaffName;
                    tmp.CreateDate = item.CreateDate.ToString("yyyy/MM/dd");
                    tmp.CreateUser = item.CreateUser;
                    tmp.Display = item.Display;
                    reply.StaffInfoList.Add(tmp);
                }

            }
            catch (Exception)
            {
                
            }
        }



        private static void Service_RecieveRegstStaffEvent(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            RegistStaffRequest request = ((gRPCArgsRcv)e).Request as RegistStaffRequest;
            RegistStaffReply reply = ((gRPCArgsRcv)e).Replay as RegistStaffReply;

            try
            {
                StaffMasterM.UpdateList(request);

            }
            catch (Exception)
            {
                reply.ErrorCode = 2;

            }

        }
    }
}
