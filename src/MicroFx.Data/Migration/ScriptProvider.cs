using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using DbUp.Engine;
using DbUp.Engine.Transactions;

namespace MicroFx.Data.Migration
{
    public class ScriptProvider : IScriptProvider
    {
        private readonly string folder;
        private readonly string directory;

        public ScriptProvider(string folder)
        {
            this.folder = folder;
            this.directory = ConfigurationManager.AppSettings["ScriptRootDirectory"] ??
                             AppDomain.CurrentDomain.BaseDirectory + @"\DbScripts";
        }

        public IEnumerable<SqlScript> GetScripts(IConnectionManager connectionManager)
        {
            var scripts = new List<SqlScript>();

            var path = Path.Combine(directory, folder);

            var files = Directory.GetFiles(path, "*.Sql");

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Exists)
                    scripts.Add(new SqlScript(fileInfo.Name, File.ReadAllText(file)));
            }

            return scripts;
        }
    }
}