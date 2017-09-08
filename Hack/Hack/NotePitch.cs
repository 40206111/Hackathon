using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack
{
    class NotePitch
    {
        int seed = 0;
        int[] pitches;
        public int[] GenerateNotes(BarType barType, int startPitch, int previousPitch, int noteCount, int notesInScale)
        {
            pitches = new int[noteCount];
            int currentPitch = previousPitch;
            int[] variance = new int[2];
            int arraySize = notesInScale;
            for (int i = 0; i < noteCount; ++i)
            {
                variance = VarianceByBar(barType, i, noteCount);

                Random rand = new Random(seed);
                pitches[i] = previousPitch + rand.Next(variance[0], variance[1]);
                seed++;
                if (barType == BarType.Start && i == 0)
                {
                    pitches[i] = startPitch;
                }
                else if (barType == BarType.End && i == noteCount - 1)
                {
                    pitches[i] = startPitch;
                }

                if (pitches[i] >= arraySize)
                {
                    pitches[i] = arraySize - 1;
                }
                else if (pitches[i] < 0)
                {
                    pitches[i] = 0;
                }
                previousPitch = pitches[i];
            }
            return pitches;
        }

        int[] VarianceByBar(BarType barType, int currentNote, int totalNotes)
        {
            int[] variance = new int[2];
            switch (barType)
            {
                case BarType.Start:
                    if (currentNote == 0)
                    {
                        variance[0] = 0;
                        variance[1] = 0;
                    }
                    else if (currentNote >= totalNotes / 4)
                    {
                        variance[0] = -1;
                        variance[1] = 1;
                    }
                    break;
                case BarType.End:
                    if (currentNote == 0)
                    {
                        variance[0] = -1;
                        variance[1] = 1;
                    }
                    else if (currentNote == totalNotes - 1)
                    {
                        variance[0] = 0;
                        variance[1] = 0;
                    }
                    break;
                case BarType.Fall:
                    variance[0] = -2;
                    variance[1] = 0;
                    break;
                case BarType.Rise:
                    variance[0] = 0;
                    variance[1] = 2;
                    break;
                case BarType.EarlyPeak:
                    if (currentNote < totalNotes / 3.0f)
                    {
                        variance[0] = 1;
                        variance[1] = 3;
                    }
                    else
                    {
                        variance[0] = -2;
                        variance[1] = 0;
                    }
                    break;
                case BarType.Peak:
                    if (currentNote < totalNotes / 2.0f)
                    {
                        variance[0] = 0;
                        variance[1] = 2;
                    }
                    else
                    {
                        variance[0] = -2;
                        variance[1] = 0;
                    }
                    break;
                case BarType.LatePeak:
                    if (currentNote < (totalNotes * 2.0f) / 3.0f)
                    {
                        variance[0] = 0;
                        variance[1] = 2;
                    }
                    else
                    {
                        variance[0] = -3;
                        variance[1] = -1;
                    }
                    break;
                case BarType.EarlyTrough:
                    if (currentNote < totalNotes / 3.0f)
                    {
                        variance[0] = -3;
                        variance[1] = -1;
                    }
                    else
                    {
                        variance[0] = 0;
                        variance[1] = 2;
                    }
                    break;
                case BarType.Trough:
                    if (currentNote < totalNotes / 2.0f)
                    {
                        variance[0] = -2;
                        variance[1] = 0;
                    }
                    else
                    {
                        variance[0] = 0;
                        variance[1] = 2;
                    }
                    break;
                case BarType.LateTrough:
                    if (currentNote < (totalNotes*2.0f) / 3.0f)
                    {
                        variance[0] = -2;
                        variance[1] = 0;
                    }
                    else
                    {
                        variance[0] = 1;
                        variance[1] = 3;
                    }
                    break;
                default:
                    variance[0] = -1;
                    variance[1] = 1;
                    break;

            }
            return variance;
        }
    }

}
