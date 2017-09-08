using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack
{
    public static class MixerClass
    {


        //Mixing 2 buffers together
        public static List<float> Mix(List<float> FirstBuffer, List<float> SecondBuffer, float FirstWeighting, float SecondWeighting)
        {
            List<float> ReturnBuffer = new List<float>();
            //Looping through each first and second buffer values and adding them to a new buffer
            for (int i = 0; i < FirstBuffer.Count; i++)
            {
                ReturnBuffer.Add((FirstWeighting * FirstBuffer[i]) + (SecondWeighting * SecondBuffer[i]));
            }
            return ReturnBuffer;


        }

        //Normalising the buffer
        public static List<float> Normalize(List<float> BufferToNormalize)
        {
            List<float> NormalizedBuffer = new List<float>();
            float largestAbsValue = 0;
            for(int i = 1; i < BufferToNormalize.Count; i++)
            {

                //Finding the largest absolute value
                if (Math.Abs(BufferToNormalize[i]) > largestAbsValue)
                {
                    largestAbsValue = Math.Abs(BufferToNormalize[i]);
                }
            }

            //Normalising the buffer
            for (int i = 0; i < BufferToNormalize.Count; i++ )
            {
                NormalizedBuffer.Add(BufferToNormalize[i] / largestAbsValue);
            }


            return NormalizedBuffer;
        }

    }
}