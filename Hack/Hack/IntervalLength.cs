using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum NoteType
{
    whole = 0,
    half = 1,
    quarter = 2,
    eighth = 3
};
namespace Hack
{
    class IntervalLength
    {

        int seed = 7;

        int currentEighth = 0;
        int totalEighths = 8;

        int[] noteLength = new int[] { 8, 4, 2, 1 };
        int[] noteProbability = new int[] { 5, 5, 5, 5 };

        List<NoteType> notesInBar = new List<NoteType>();
        int[] intervalCount = new int[] { 0, 0, 0, 0 };
        // Get/set the seed
        public int Seed
        {
            get { return seed; }
            set { seed = value; }
        }


        public void BarNotes()
        {
            // While more notes can fit into the bar
            while (currentEighth < totalEighths)
            {
                // Pick a note type, and then document its existance
                DocumentType(GetNoteType());
            }
        }
        // Calculates each notes probability of being played
        void CalculateProbabilitiesNormal()
        {
            // If off-beat tend heavily towards eighth notes
            if (currentEighth % 2 != 0)
            {
                noteProbability[(int)NoteType.whole] = 1;
                noteProbability[(int)NoteType.half] = 3;
                noteProbability[(int)NoteType.quarter] = 6;
                noteProbability[(int)NoteType.eighth] = 40;
            }
            else
            {
                noteProbability[(int)NoteType.whole] = 3;
                noteProbability[(int)NoteType.half] = 4;
                noteProbability[(int)NoteType.quarter] = 5;
                noteProbability[(int)NoteType.eighth] = 4;
            }

            // Divide own probability by 2 times number of note occurences
            noteProbability[(int)NoteType.half] /= intervalCount[(int)NoteType.half] * 2;
            // Leave note type quarter unnaffected
            // Multiply own probability by 1 plus the first 90 degrees of cos upside-down (cos increases the more of this type exist)
            noteProbability[(int)NoteType.eighth] *= (int)(2.0f - Math.Cos(((float)intervalCount[(int)NoteType.eighth] * Math.PI) / (2.0f * totalEighths)));

            for (int i = 0; i < 4; ++i)
            {
                // If the note length exceeds the time remaining in the bar don't allow it
                if (noteLength[i] + currentEighth > totalEighths)
                {
                    noteProbability[i] = 0;
                }
            }

        }
        // Returns the note type to be played at this point in the bar
        NoteType GetNoteType()
        {
            CalculateProbabilitiesNormal();
            int ratioTotal = RatioTotals();
            Random random = new Random(seed);
            float value = random.Next(0, 100);
            if (value <= NoteProbability(NoteType.whole, ratioTotal))
            {
                return NoteType.whole;
            }
            else if (value <= NoteProbability(NoteType.half, ratioTotal))
            {
                return NoteType.half;
            }
            else if (value <= NoteProbability(NoteType.quarter, ratioTotal))
            {
                return NoteType.quarter;
            }
            return NoteType.eighth;
        }
        // Accumulates the total value of all ratios so that each note's "stake" can be calculated
        int RatioTotals()
        {
            int total = 0;
            foreach (int prob in noteProbability)
            {
                total += prob;
            }
            return total;
        }
        // Returns the probability of this note and previous ones combined to allow for probablistic calculations
        int CumulativeProbabilities(int index)
        {
            int total = 0;
            for (int i = 0; i <= index; ++i)
            {
                total += noteProbability[i];
            }
            return total;
        }
        // Returns the float value representing a note's percentage bracket
        float NoteProbability(NoteType noteType, int ratioTotal)
        {
            return ((float)CumulativeProbabilities((int)NoteType.quarter) / ratioTotal) * 100.0f;
        }
        // Adjusts data based on last note type
        void DocumentType(NoteType noteType)
        {
            notesInBar.Add(noteType);
            intervalCount[(int)noteType]++;
            currentEighth += noteLength[(int)noteType];
        }
    }
}
