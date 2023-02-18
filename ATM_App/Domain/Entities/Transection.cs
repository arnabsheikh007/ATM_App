using ATM_App.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_App.Domain.Entities
{
    public class Transection
    {
        public long TransectionID { get; set; }
        public long UserBankAccount { get; set; }
        public DateTime TransectionDate { get; set; }
        public TransectionType TransectionType { get; set; }
        public string Description { get; set; }
        public decimal TransectionAmount { get; set; }

    }
}
