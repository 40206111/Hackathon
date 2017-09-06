using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace Hack
{
    class Player
    {
        private DynamicSoundEffectInstance instance;

        private List<float> workingBuffer;
        private byte[] xnaBuffer;




        public Player()
        {
            instance = new DynamicSoundEffectInstance(44100, AudioChannels.Mono);


            // testing
            NoteGenerator n = new NoteGenerator();
            workingBuffer = n.NoteFromA3(0, 1, waveform.sine);
            convert();
            instance.SubmitBuffer(xnaBuffer);
            instance.Play();
        }

        // Converts the float values in th working buffer to pairs of bytes and stores them in xna buffer
        private void convert()
        {
            int bufferSize = workingBuffer.Count * 2;
            xnaBuffer = new byte[bufferSize];
            
            for (int i = 0; i < bufferSize / 2; i++)
            {
                float floatSample = MathHelper.Clamp(workingBuffer[i], -1.0f, 1.0f);
                short shortSample = (short) (floatSample >= 0.0f ? short.MaxValue * floatSample : short.MinValue * floatSample * -1);

                if (!BitConverter.IsLittleEndian)
                {
                    xnaBuffer[i * 2] = (byte)(shortSample >> 8);
                    xnaBuffer[i * 2 + 1] = (byte)shortSample;
                }
                else
                {
                    xnaBuffer[i * 2] = (byte)shortSample;
                    xnaBuffer[i * 2 + 1] = (byte)(shortSample >> 8);
                }
            }
        }




    }
}
