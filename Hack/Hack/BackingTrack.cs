using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack
{
    class BackingTrack
    {
        public List<float> makeTrack(int[] scale, int keyIndex, float tempo, int nOfMeasures, waveform form)
        {
            int[] wrapped;
            wrapped = getIndexes(keyIndex, scale);
            List<float> buffer = new List<float>();
            List<float> measure = new List<float>();
            NoteGenerator ng = new NoteGenerator();
            Random r = new Random();
            for (int i = 0; i < 4; i++)
            {
                measure.AddRange(ng.NoteFromA3(wrapped[r.Next(5)], (60 / tempo) * 4.0f, form));
            }
            for (int i = 0; i < nOfMeasures; i++)
                buffer.AddRange(measure);
            return buffer;
        }



        // hacks
        private int[] getIndexes(int key, int[] scale)
        {
            int[] wrapped = new int[5];
            for (int i = 0; i < 5; i++)
            {
                if (key + i > 5)
                    wrapped[i] = scale[i - 5];
                else
                    wrapped[i] = scale[i];
            }
            return wrapped;
            
        }


    }
}
