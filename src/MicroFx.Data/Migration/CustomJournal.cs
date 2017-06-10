using System;
using DbUp.Engine;

namespace MicroFx.Data.Migration
{
    public class CustomJournal : IJournal
    {
        public string[] GetExecutedScripts()
        {
            throw new NotImplementedException();
        }

        public void StoreExecutedScript(SqlScript script)
        {
            throw new NotImplementedException();
        }
    }
}