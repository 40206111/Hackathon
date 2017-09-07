using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace Hack
{

    // Three wrapper classes for the three main sections of the wav file
    class WaveHeader
    {
        public string sGroupID; // RIFF
        public uint dwFileLength; // total file length minus 8, which is taken up by RIFF
        public string sRiffType; // always WAVE

        /// <summary>
        /// Initializes a WaveHeader object with the default values.
        /// </summary>
        public WaveHeader()
        {
            dwFileLength = 0;
            sGroupID = "RIFF";
            sRiffType = "WAVE";
        }
    }

    class WaveFormatChunk
    {
        public string sChunkID;         // Four bytes: "fmt "
        public uint dwChunkSize;        // Length of header in bytes
        public ushort wFormatTag;       // 1 (MS PCM)
        public ushort wChannels;        // Number of channels
        public uint dwSamplesPerSec;    // Frequency of the audio in Hz... 44100
        public uint dwAvgBytesPerSec;   // for estimating RAM allocation
        public ushort wBlockAlign;      // sample frame size, in bytes
        public ushort wBitsPerSample;    // bits per sample

        /// <summary>
        /// Initializes a format chunk with the following properties:
        /// Sample rate: 44100 Hz
        /// Channels: Mono
        /// Bit depth: 16-bit
        /// </summary>
        public WaveFormatChunk()
        {
            sChunkID = "fmt ";
            dwChunkSize = 16;
            wFormatTag = 1;
            wChannels = 1;
            dwSamplesPerSec = 44100;
            wBitsPerSample = 16;
            wBlockAlign = (ushort)(wChannels * (wBitsPerSample / 8));
            dwAvgBytesPerSec = dwSamplesPerSec * wBlockAlign;
        }
    }

    class WaveDataChunk
    {
        public string sChunkID;     // "data"
        public uint dwChunkSize;    // Length of header in bytes
        public short[] shortArray;  // 16-bit audio

        /// <summary>
        /// Initializes a new data chunk with default values.
        /// </summary>
        public WaveDataChunk()
        {
            shortArray = new short[0];
            dwChunkSize = 0;
            sChunkID = "data";
        }
    }


    ////////////////////////////////////////////////////////////////////



    class Player
    {
      //  private DynamicSoundEffectInstance instance;

        private List<float> workingBuffer;
        private short[] convertedBuffer;




        public Player()
        {




       //     instance = new DynamicSoundEffectInstance(44100, AudioChannels.Mono);

/*
            // testing
            NoteGenerator n = new NoteGenerator();
            workingBuffer = n.NoteFromA3(0, 1, waveform.sine);
            convert();
            instance.SubmitBuffer(convertedBuffer);
            instance.Play();
            */
        }



        // Converts the float values in th working buffer to pairs of bytes and stores them in xna buffer
        private void convert()
        {
            int bufferSize = workingBuffer.Count;
            convertedBuffer = new short[bufferSize];
            
            for (int i = 0; i < bufferSize; i++)
            {
                float floatSample = MathHelper.Clamp(workingBuffer[i], -1.0f, 1.0f);
                short shortSample = (short) (floatSample >= 0.0f ? short.MaxValue * floatSample : short.MinValue * floatSample * -1);


                convertedBuffer[i] = shortSample;
                /*
                if (!BitConverter.IsLittleEndian)
                {
                    convertedBuffer[i * 2] = (byte)(shortSample >> 8);
                    convertedBuffer[i * 2 + 1] = (byte)shortSample;
                }
                else
                {
                    convertedBuffer[i * 2] = (byte)shortSample;
                    convertedBuffer[i * 2 + 1] = (byte)(shortSample >> 8);
                }
                */
            }
        }




    }
}
