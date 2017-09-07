using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack
{
    class KeySignature
    {
        float[] scale = new float[21];
        int[] majorIntervals = new int[] { 2, 2, 1, 2, 2, 2, 1 };
        int[] minorIntervals = new int[] { 2, 1, 2, 2, 1, 2, 2 };

        public void CreateScale(int start, float[] allNotes)
        {
            int interval = 0;
            int i = GetLowest(start);
        }

        int GetLowest(int start)
        {

            return start;
        }
    }
}
