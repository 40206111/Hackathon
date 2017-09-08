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
        List<float> buffer = new List<float>();
        int key;
        int currentBar = 0;
        int totalBars;

        public Maestro(int key, int keyStyle, float tempo)
        {
            this.key = key;
            this.tempo = tempo;
            scale = keyMaker.CreateScale(key, keyStyle);
            noteDurations = NoteValues.GetDuration(tempo);
        }

        public List<float> CreateTrack(int phrases, waveform form)
        {
            totalBars = phrases * 4;
            for (int i = 0; i < phrases; ++i)
            {
                CreateFour(form);
            }
            return buffer;
        }

        private void CreateFour(waveform form)
        {
            for (int i = 0; i < 4; ++i)
            {
                CreateBar(form);
            }
        }

        private void CreateBar(waveform form)
        {
            List<NoteType> notesInBar = barMaker.BarNotes();
            int[] pitchIndexes = pitcher.GenerateNotes(10, notesInBar.Count);
            for (int i = 0; i < notesInBar.Count; ++i)
            {
                float noteDuration = noteDurations[(int)notesInBar[i]];
                buffer.AddRange(noteOutput.NoteFromA3(scale[pitchIndexes[i]], noteDuration, form));
            }
        }
    }
}
