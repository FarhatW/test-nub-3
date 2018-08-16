using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using jce.Common.Resources;
using jce.DataAccess.Core;

namespace jce.BusinessLayer.SaveData
{
   public interface ISaveHistoryActionData
    {
//        IUnitOfWork UnitOfWork { get; }
        Task SaveHistory(HistoryActionResource historyAction);
     
    }
}
