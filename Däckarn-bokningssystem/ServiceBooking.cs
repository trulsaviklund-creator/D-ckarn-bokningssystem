using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Däckarn_bokningssystem
{
    internal class ServiceBooking
    {
        public int ServiceType { get; set; }
        public DateTime ServiceDate { get; set; }
        public string RegNr { get; set; }
        public string CustomerName { get; set; }

        public ServiceBooking (int serviceType, DateTime serviceDate, string regNr, string customerName)
        {
            ServiceType = serviceType;
            ServiceDate = serviceDate;
            RegNr = regNr;
            CustomerName = customerName;
        }

        public override string ToString()
        {
            return $"Kund: {CustomerName}, RegNr: {RegNr}, Tjänst: {ServiceType}, Datum: {ServiceDate}";
        }

    }
}
