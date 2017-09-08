using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack
{
    public static class MixerClass
    {


        // Add attack to the waveform. Should only be used on single notes
        public static List<float> AddAttack(List<float> buffer)
        {
            int length = (int)(44100.0f / 1000.0f * 200.0f);
            float scale = 0.6f;
            float tmp;
            for (int i = 0; i < length; i++)
            {
                if (i < length / 3)
                    tmp = (float)Math.Sin(3.0f * (float)Math.PI / 4.0f / (length / 3));
                else
                    tmp = (float)Math.Sin(length * 2.0f / 3.0f) * (1.0f + (1.0f - (float)Math.Cos((i - (length / 3.0f)) * 3.0f / (4.0f * length))));
                buffer[i] *= (tmp + 1.0f) * scale + 1.0f;
            }
            buffer = Normalize(buffer);
            return buffer;
        }


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

        //Reverb buffer
        public static List<float> Reverberation(List<float> buffer, float reverbIntensity)
        {
            
            int length = (int)(44100.0f / 1000.0f * 40.0f);
            List<float> reverbBuffer = new List<float>();
            for (int i = 0; i < buffer.Count; i++)
            {
                if (i < length)
                {
                    reverbBuffer.Add(0.0f);
                }
                else
                {
                    reverbBuffer.Add(buffer[i - length]);
                }
            }

            reverbBuffer = Mix(buffer, reverbBuffer, 1.0f, reverbIntensity);
            reverbBuffer = Normalize(reverbBuffer);

                return reverbBuffer;
        }



        //Distortion
        public static List<float> Distortion(List<float> bufferToClip, float distortionLevel)
        {
            //Clipping the buffer
            for (int i = 1; i < bufferToClip.Count; i++)
            {
                bufferToClip[i] = bufferToClip[i] * distortionLevel;
                if (bufferToClip[i] > 1.0f)
                {
                    bufferToClip[i] = 1.0f;
                }
                else if (bufferToClip[i] < (-1.0f))
                {
                    bufferToClip[i] = -1.0f;
                }
            }
            return bufferToClip;
        }



    }
}