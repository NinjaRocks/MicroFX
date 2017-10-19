using System;
using DbUp.Engine;

namespace MicroFx.Data.Migration
{
    public class ScriptPreprocessor : IScriptPreprocessor
    {
        public string Process(string contents)
        {
            return contents;
        }
    }
}