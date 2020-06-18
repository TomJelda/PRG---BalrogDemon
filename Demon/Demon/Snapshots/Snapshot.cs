using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demon.Snapshots
{
    public class Snapshot 
    {
        public string Path { get; set; }

        public string Type { get; set; }

        public DateTime? Edited { get; set; }
    }

    public class ComparePathIfFolderComparer : IEqualityComparer<Snapshot>
    {
        public bool Equals(Snapshot x, Snapshot y)
        {
            bool isFolder = x.Type == "folder" && y.Type == "folder";

            if (isFolder && x.Path == y.Path)
                return true;
            else
                return x.Edited == y.Edited;

            //return x.Path == y.Path;
        }

        public int GetHashCode(Snapshot obj)
        {
            return obj.Path.GetHashCode();
        }
    }
}
