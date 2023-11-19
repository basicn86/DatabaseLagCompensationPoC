using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLagCompensationPoC
{
    public class CacheDatabase
    {
        List<Msg> msgCache = new List<Msg>();
        private Database db;
        List<Task> updates = new List<Task>();

        public CacheDatabase()
        {
            db = new Database();
        }

        public async Task<List<Msg>> GetMessages()
        {
            if (msgCache.Count == 0)
            {
                msgCache = await db.GetMessages();
            }
            return msgCache;
        }

        //update message, just set the content of it to "The contents have been updated" for simplicity sake
        public async Task UpdateMessage(int id)
        {
            var msg = msgCache.FirstOrDefault(m => m.Id == id);
            if (msg != null)
            {
                msg.Content = "The contents have been updated";
            }
            var task = db.UpdateMessage(id);
            updates.Add(task);
        }

        //wait until completed
        public async Task WaitForUpdates()
        {
            await Task.WhenAll(updates);
        }

        public void ResetTable()
        {
            db.ResetTable();
        }
    }
}
