using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmSample.Model;

namespace MvvmSample.Reader
{
    public static class NameReader
    {
        public static List<NameSelectionEntry> GetNameEntries()
        {
            List<NameSelectionEntry> names = new List<NameSelectionEntry>
            {
                new NameSelectionEntry("John"),
                new NameSelectionEntry("Kenny"),
                new NameSelectionEntry("Peter"),
                new NameSelectionEntry("Carl"),
                new NameSelectionEntry("Nick"),
                new NameSelectionEntry("Terry"),
                new NameSelectionEntry("Richard"),
                new NameSelectionEntry("Denny"),
                new NameSelectionEntry("Benny"),
                new NameSelectionEntry("Henry")
            };


            return names;
        }
    }
}
