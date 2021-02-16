using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbBank.Lib.Models
{
    public enum TerminalState
    {
        AskForAccountName,
        ModifyAccountBalance,
        ShowTotalBankBalance,
        ExitApplication,
    }
}
