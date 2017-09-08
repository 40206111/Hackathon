using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum BarType
{
    Start,
    End,
    EarlyPeak,
    Peak,
    LatePeak,
    EarlyTrough,
    Trough,
    LateTrough,
    Rise,
    Fall,
    ThemeA,
    ThemeB
};

public enum Structure
{
    A, B
};

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
        int rootInScale;
        int chromaticRoot;
        List<float> buffer = new List<float>();
        int currentBar = 1;
        int totalBars;
        List<Structure> structure = new List<Structure>();
        Dictionary<Structure, List<BarType>> phraseStructure = new Dictionary<Structure, List<BarType>>();

        public Maestro(int chromaticRoot, int keyStyle, float tempo)
        {
            this.chromaticRoot = chromaticRoot;
            this.tempo = tempo;
            scale = keyMaker.CreateScale(chromaticRoot, keyStyle);
            rootInScale = keyMaker.FindRoot(chromaticRoot, scale);
            noteDurations = NoteValues.GetDuration(tempo);
        }

        public List<float> CreateTrack(int phrases, waveform form)
        {
            totalBars = phrases * 4;
            CreateStructure(phrases);
            for (int i = 0; i < phrases; ++i)
            {
                CreateFour(form, i);
            }
            return buffer;
        }

        private void CreateFour(waveform form, int phraseNumber)
        {
            List<BarType> currentPhrase = phraseStructure[structure[phraseNumber]];
            for (int i = 0; i < 4; ++i)
            {
                CreateBar(form, currentPhrase[i]);
            }
        }

        private void CreateBar(waveform form, BarType barType)
        {
            List<NoteType> notesInBar = new List<NoteType>();
            int[] pitchIndexes;
            if (currentBar == 1)
            {
                notesInBar = barMaker.BarNotes();
                pitchIndexes = pitcher.GenerateNotes((scale.Length * 2) / 3, notesInBar.Count, scale.Length);
            }
            else if (currentBar == totalBars)
            {
                notesInBar = barMaker.BarNotes();
                pitchIndexes = pitcher.GenerateNotes((scale.Length * 2) / 3, notesInBar.Count, scale.Length);
            }
            else
            {
                notesInBar = barMaker.BarNotes();
                pitchIndexes = pitcher.GenerateNotes((scale.Length * 2) / 3, notesInBar.Count, scale.Length);
            }
            for (int i = 0; i < notesInBar.Count; ++i)
            {
                float noteDuration = noteDurations[(int)notesInBar[i]];
                buffer.AddRange(noteOutput.NoteFromA3(scale[pitchIndexes[i]], noteDuration, form));
            }
            currentBar++;
        }

        private void CreateStructure(int phrases)
        {
            if (phrases <= 2)
            {
                structure.Add(Structure.A);
                structure.Add(Structure.A);
            }
            else if (phrases == 3)
            {
                structure.Add(Structure.A);
                structure.Add(Structure.B);
                structure.Add(Structure.A);
            }
            else if (phrases == 4)
            {
                structure.Add(Structure.A);
                structure.Add(Structure.A);
                structure.Add(Structure.B);
                structure.Add(Structure.A);
            }
            else if (phrases > 4)
            {
                structure.Add(Structure.A);
                for (int i = 0; i < phrases - 2; ++i)
                {
                    Random rand = new Random(i);
                    int structureValue = rand.Next(0, 2);
                    if (structureValue != 0)
                    {
                        structure.Add(Structure.A);
                    }
                    else
                    {
                        structure.Add(Structure.B);
                    }
                }
                structure.Add(Structure.A);
            }
        }
    }
}
