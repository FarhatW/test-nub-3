using System;
using System.Threading.Tasks;
using jce.BusinessLayer.IManagers;
using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Resources;
using jce.DataAccess.Core;

namespace jce.BusinessLayer.SaveData
{
    public class SaveHistoryActionData :ISaveHistoryActionData
    {
   

        private readonly IHistoryActionManager _historyActionManager;

        public SaveHistoryActionData( IHistoryActionManager historyActionManager)
        {
            _historyActionManager = historyActionManager;
          
        }

        public async Task SaveHistory(HistoryActionResource historyAction)
        {

           await _historyActionManager.Add(historyAction);
          
        }
    }
}
