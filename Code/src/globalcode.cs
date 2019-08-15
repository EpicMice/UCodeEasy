using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gamecore
{
    public class Indexer
    {

        public string indexname;
        public Stack<string> used;
        public Dictionary<string, string> currentlyInUse;

        public Indexer(string indexname)
        {
            this.indexname = indexname;
            used = new Stack<string>();
            currentlyInUse = new Dictionary<string, string>();
        }

        public string GetIndex()
        {
            string index;

            if (used.Count > 0)
            {
                index = used.Pop();
                currentlyInUse.Add(index, index);
            }
            else
            {
                currentlyInUse.Add(index = indexname + ":" + currentlyInUse.Count, index);
            }

            return index;
        }

        public void RenewIndex(string index)
        {
            currentlyInUse.Remove(index);
            used.Push(index);
        }

    }
}
