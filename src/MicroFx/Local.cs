using System;
using System.Collections;
using System.Web;

namespace MicroFx
{
    public static class Local
    {
        static readonly ILocalData current = new LocalData();

        static readonly object localDataHashtableKey = new object();

        private class LocalData : ILocalData
        {

            [ThreadStatic]
            static Hashtable threadHashtable;

            private static Hashtable LocalHashtable
            {
                get
                {
                    if (!RunningInWeb)
                    {
                        return threadHashtable ??

                        (
                            threadHashtable = new Hashtable()
                        );
                    }

                    var webHashtable = HttpContext.Current.Items[localDataHashtableKey] as Hashtable;

                    if (webHashtable == null)
                    {
                        HttpContext.Current.Items[localDataHashtableKey] = webHashtable = new Hashtable();

                    }
                    return webHashtable;
                }
            }

            public object this[object key]
            {
                get { return LocalHashtable[key]; }
                set { LocalHashtable[key] = value; }

            }

            public void Clear()
            {
                LocalHashtable.Clear();
            }
        }


       
        public static ILocalData Data
        {
            get { return current; }

        }

        public static bool RunningInWeb
        {
            get { return HttpContext.Current != null; }

        }
    }

    public interface ILocalData
    {
        object this[object localContainerKey] { get; set; }
    }
}
