using System;
using System.Collections.Generic;
using System.Text;

namespace SageCRM.AspNet
{
    public class CommObject
    {

        public string Action = "Meeting";
        public string Status = "Pending";
        public string Priority = "Normal";
        public string Private = "";
        //regarding
        public string CaseId = "";
        public string OpportunityId = "";
        public string OrderId = "";
        public string QuoteId = "";
        //end regarding
        public DateTime CDateTime=DateTime.Now;
        public DateTime CToDateTime = DateTime.Now.AddMinutes(30);
        public string NotifyDelta = "";
        public DateTime NotifyTime = DateTime.Now;
        public string Type = "Appointment";//can also be Task
        public string CommunicationId = "";
        public string ChannelId = "";

        public string[] UserList;
        public string[] CompanyList;
        public string[] PersonList;

        public string[] ExtraFieldsList; //holds any new crm custom fields or fields that we have not created a type for

        //constructor
        public CommObject()
        {

        }
        public bool Update()
        {
            return Update(CommunicationId);
        }
        public bool Update(string CommunicationId)
        {

            return true;
        }
        public bool Insert()
        {

            return true;
        }
        public bool Delete()
        {
            return Delete(this.CommunicationId);
        }
        public bool Delete(string CommunicationId)
        {
            return true;
        }
        //set the status to Cancelled
        public bool Cancel()
        {
            return true;

        }
        //set the status to Complete
        public bool Complete()
        {
            return true;

        }

    }
}
