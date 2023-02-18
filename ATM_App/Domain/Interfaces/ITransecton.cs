using ATM_App.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_App.Domain.Interfaces
{
    public interface ITransecton
    {
        void InsertTransection(long _UserBankAccountID, TransectionType _tranType, decimal _tranAmount, string _desc);
        void ViewTransection();
    }
}
