using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoL.Model
{
    public class RequestData
    {
        public int RequestId { get; set; }
        public DateTime StartDate { get; set; }
        public string OrgTechType { get; set; }
        public string OrgTechModel { get; set; }
        public string ProblemDescription { get; set; }
        public string ClientFio { get; set; }
        public long ClientPhone { get; set; }
        public string RequestStatus { get; set; }

        public int ClientId { get; set; } 
        public int MasterId { get; set; }
    }
}
