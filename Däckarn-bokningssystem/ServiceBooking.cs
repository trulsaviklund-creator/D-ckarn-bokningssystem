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
        //skapade properties som innehåller namn, regnr, tjänstetyp och tidpunkt för bokningen
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

        //metod för att skriva ut bokningsinformationen, vi har gjort om ToString metoden för att skriva ut en string istället för objektets position
        public override string ToString() 
        {
            return $"Kund: {CustomerName} | RegNr: {RegNr} | Tjänst: {ServiceType} | datum och tid: {ServiceTime}";
        }

    }
}
