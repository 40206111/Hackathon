using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack
{
    class KeySignature
    {
        int[] scale;
        int[] majorIntervals = new int[] { 2, 2, 1, 2, 2, 2, 1 };
        int[] minorIntervals = new int[] { 2, 1, 2, 2, 1, 2, 2 };
        int[] majPentatonicIntervals = new int[] { 2, 2, 3, 2, 3 };
        int[] minPentatonicIntervals = new int[] { 3, 2, 2, 3, 2 };
        int[] usedScale;
        int interval = 0;
        int scaleType = 0;


        public int[] CreateScale(int start, int scaleType)
        {
            this.scaleType = scaleType;
            ChooseScale();
            scale = new int[usedScale.Length * 3];
            int lowStart = GetLowest(start);
            for (int i = 0; i < usedScale.Length * 3; ++i)
            {
                scale[i] = lowStart;
                lowStart += usedScale[Interval];
                Interval++;
            }
            return scale;
        }

        void ChooseScale()
        {
            switch (scaleType)
            {
                case (0):
                    usedScale = majorIntervals;
                    break;
                case (1):
                    usedScale = minorIntervals;
                    break;
                case (2):
                    usedScale = majPentatonicIntervals;
                    break;
                case (3):
                    usedScale = minPentatonicIntervals;
                    break;
                default:
                    usedScale = majorIntervals;
                    break;
            }
        }

        int GetLowest(int start)
        {
            bool flag = true;
            while (flag)
            {
                if(start <= 0)
                {
                    flag = false;
                }
                else
                {
                    Interval--;
                    if (start >= usedScale[Interval])
                    {
                        start -= usedScale[Interval];
                    }
                    else
                    {
                        Interval++;
                        flag = false;
                    }
                }
            }
            return start;
        }

        int Interval
        {
            get { return interval; }
            set
            {
                while (value < 0)
                {
                    value += usedScale.Length;
                }

                if (value >= usedScale.Length)
                {
                    interval = value % usedScale.Length;
                }
                else
                {
                    interval = value;
                }
            }
        }

        public int FindRoot(int rootNote, int[] scale)
        {
            for (int i = 0; i < scale.Length; ++i)
            {
                if (rootNote == scale[i])
                {
                    return i;
                }
            }
            return 0;
        }
    }
}