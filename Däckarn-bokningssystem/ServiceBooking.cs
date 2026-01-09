using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Däckarn_bokningssystem
{
    internal class ServiceBooking
    {
        public string CustomerName { get; set; }
        public string RegNr { get; set; }
        public ServiceType ServiceType { get; set; }
        public DateTime ServiceTime { get; set; }


        public ServiceBooking(string customerName, string regNr, ServiceType serviceType, DateTime serviceTime)
        {
            CustomerName = customerName;
            RegNr = regNr;
            ServiceType = serviceType;
            ServiceTime = serviceTime;
        }

        public override string ToString()
        {
            return $"Kund: {CustomerName} | RegNr: {RegNr} | Tjänst: {ServiceType} | datum och tid: {ServiceTime}";
        }

    }
}
