using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack
{
    class BackingTrack
    {
        public List<float> makeTrack(int[] scale, int keyIndex, float tempo, int nOf4Bars, waveform form, float timeSignature)
        {
            int[] wrapped;
            wrapped = getIndexes(keyIndex, scale, scale.Length/3);
            List<float> buffer = new List<float>();
            List<float> fourBars = new List<float>();
            NoteGenerator ng = new NoteGenerator();
            Random r = new Random();
            for (int i = 0; i < 4; i++)
            {
                fourBars.AddRange(ng.NoteFromA3(wrapped[r.Next(5)], (60 / tempo) * timeSignature * 4.0f, form));
            }
            for (int i = 0; i < nOf4Bars; i++)
                buffer.AddRange(fourBars);
            return buffer;
        }



        // hacks
        private int[] getIndexes(int key, int[] scale, int octaveLength)
        {
            int scaleLength = octaveLength;
            int[] wrapped = new int[scaleLength];
            for (int i = 0; i < scaleLength; i++)
            {
                if (key + i >= scaleLength)
                    wrapped[i] = scale[i + key - scaleLength];
                else
                    wrapped[i] = scale[i + key];
            }
            return wrapped;
        }


    }
}
