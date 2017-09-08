using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack
{
    class NotePitch
    {
        Random rand = new Random();
        int[] pitches;
        public int[] GenerateNotes(int startPitch, int noteCount)
        {
            pitches = new int[noteCount];
            int currentPitch = startPitch;
            int variance = 3;
            for (int i = 0; i < noteCount; ++i)
            {
                pitches[i] = currentPitch + rand.Next(-variance, variance);
                if (pitches[i] >= 15)
                {
                    pitches[i] = 14;
                }
                else if (pitches[i] < 0)
                {
                    pitches[i] = 0;
                }
                currentPitch = pitches[i];
            }
            return pitches;
        }

    }
}
