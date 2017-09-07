using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack
{
    class Maestro
    {
        KeySignature keyMaker = new KeySignature();
        IntervalLength barMaker = new IntervalLength();
        NoteGenerator noteOutput = new NoteGenerator();
        NotePitch pitcher = new NotePitch();
        float tempo = 120;
        float[] noteDurations = new float[4];
        int[] scale;

        public List<float> CreateBar(waveform form)
        {
            List<float> buffer = new List<float>();
            scale = keyMaker.CreateScale(0, 2);
            noteDurations = NoteValues.GetDuration(tempo);
            List<NoteType> notesInBar = barMaker.BarNotes();
            int[] pitchIndexes = pitcher.GenerateNotes(10, notesInBar.Count);
            for (int i = 0; i < notesInBar.Count; ++i)
            {
                float noteDuration = noteDurations[(int)notesInBar[i]];
                buffer.AddRange(noteOutput.NoteFromA3(scale[pitchIndexes[i]], noteDuration, form));
            }
            return buffer;
        }
    }
}
