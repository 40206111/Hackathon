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
        int previousNote;
        List<float> buffer = new List<float>();
        int currentBar = 1;
        int totalBars;
        List<Structure> structure = new List<Structure>();
        Dictionary<Structure, List<BarType>> phraseStructure = new Dictionary<Structure, List<BarType>>();
        List<BarType> barTypes = new List<BarType>() { BarType.EarlyPeak, BarType.Peak, BarType.LatePeak, BarType.Rise, BarType.EarlyTrough, BarType.Trough, BarType.LateTrough, BarType.Fall };
        int givenSeed;

        public Maestro(int chromaticRoot, int keyStyle, float tempo)
        {
            this.chromaticRoot = chromaticRoot;
            this.tempo = tempo;
            scale = keyMaker.CreateScale(chromaticRoot, keyStyle);
            rootInScale = keyMaker.FindRoot(chromaticRoot, scale);
            noteDurations = NoteValues.GetDuration(tempo);
        }

        public List<float> CreateTrack(int phrases, waveform form, int seed)
        {
            givenSeed = seed;
            barMaker.Seed = seed;
            pitcher.Seed = seed;
            previousNote = rootInScale;
            totalBars = phrases * 4;
            CreateStructure(phrases);
            for (int i = 0; i < phrases; ++i)
            {
                CreateFour(form, i, phrases);
            }
            return buffer;
        }

        private void CreateFour(waveform form, int phraseNumber, int phrases)
        {
            List<BarType> currentPhrase = phraseStructure[structure[phraseNumber]];
            for (int i = 0; i < 4; ++i)
            {
                if (phraseNumber == 0 && i == 0)
                {
                    CreateBar(form, BarType.Start);
                }
                else if (phraseNumber == phrases - 1 && i == 3)
                {
                    CreateBar(form, BarType.End);
                }
                else
                {
                    CreateBar(form, currentPhrase[i]);
                }
            }
        }

        private void CreateBar(waveform form, BarType barType)
        {
            List<NoteType> notesInBar = new List<NoteType>();
            int[] pitchIndexes;
            if (currentBar == 1)
            {
                notesInBar = barMaker.BarNotes(barType);
                pitchIndexes = pitcher.GenerateNotes(barType, rootInScale + (scale.Length / 3), previousNote, notesInBar.Count, scale.Length);
            }
            else if (currentBar == totalBars)
            {
                notesInBar = barMaker.BarNotes(barType);
                pitchIndexes = pitcher.GenerateNotes(barType, rootInScale + (scale.Length / 3), previousNote, notesInBar.Count, scale.Length);
            }
            else
            {
                notesInBar = barMaker.BarNotes(barType);
                pitchIndexes = pitcher.GenerateNotes(barType, rootInScale + (scale.Length / 3), previousNote, notesInBar.Count, scale.Length);
            }
            previousNote = pitchIndexes.Last<int>();
            for (int i = 0; i < notesInBar.Count; ++i)
            {
                float noteDuration = noteDurations[(int)notesInBar[i]];
                buffer.AddRange(noteOutput.NoteFromA3(scale[pitchIndexes[i]], noteDuration, form));
            }
            currentBar++;
        }

        private List<BarType> CreatePhraseLayout()
        {
            int seed = givenSeed;
            List<BarType> types = new List<BarType>();
            for (int i = 0; i < 4; ++i)
            {
                Random rand = new Random(seed);
                int value = rand.Next(0, barTypes.Count - 1);
                types.Add(barTypes[value]);
                seed++;
            }
            return types;
        }

        private void CreateStructure(int phrases)
        {
            phraseStructure[Structure.A] = CreatePhraseLayout();
            if (phrases == 1)
            {
                structure.Add(Structure.A);
            }
            else if (phrases <= 2)
            {
                structure.Add(Structure.A);
                structure.Add(Structure.A);
            }
            else if (phrases == 3)
            {
                structure.Add(Structure.A);
                structure.Add(Structure.B);
                structure.Add(Structure.A);
                phraseStructure[Structure.B] = CreatePhraseLayout();
            }
            else if (phrases == 4)
            {
                structure.Add(Structure.A);
                structure.Add(Structure.A);
                structure.Add(Structure.B);
                structure.Add(Structure.A);
                phraseStructure[Structure.B] = CreatePhraseLayout();
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
                phraseStructure[Structure.B] = CreatePhraseLayout();
            }
        }
    }
}
