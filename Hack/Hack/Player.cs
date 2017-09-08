using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System.IO;
using System.Media;

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
        private List<float> workingBuffer1;
        private List<float> workingBuffer2;
        private short[] convertedBuffer;

        private WaveHeader header;
        private WaveFormatChunk format;
        private WaveDataChunk data;

        public string FilePath = "test.wav";




        public Player()
        {

            // testing stuff
            
            BackingTrack bt = new BackingTrack();
            int[] testScale = new int[5] { 0, 2, 4, 7, 9 };
            workingBuffer1 = bt.makeTrack(testScale, 0, 120.0f, 1, waveform.sine, 4.0f/4.0f);


            Maestro m = new Maestro();
            workingBuffer2 = m.CreateBar(waveform.sine);
            workingBuffer2.AddRange(m.CreateBar(waveform.sine));
            workingBuffer2.AddRange(m.CreateBar(waveform.sine));
            workingBuffer2.AddRange(m.CreateBar(waveform.sine));

            workingBuffer = MixerClass.Mix(workingBuffer1, workingBuffer2, 1.0f, 1.0f);
            workingBuffer = MixerClass.Normalize(workingBuffer);
            
            /*

            Maestro m = new Maestro();
            workingBuffer = m.CreateBar(waveform.sine);
            workingBuffer.AddRange(m.CreateBar(waveform.sine));
            workingBuffer.AddRange(m.CreateBar(waveform.sine));
            workingBuffer.AddRange(m.CreateBar(waveform.sine));
            
            workingBuffer = MixerClass.Normalize(workingBuffer);
            */
            // testing a scale
            /*
            KeySignature k = new KeySignature();
            NoteGenerator ng = new NoteGenerator();
            workingBuffer = new List<float>();
            int[] scale = k.CreateScale(0, 2);
            for (int i = 0; i < 5; i++)
                workingBuffer.AddRange(ng.NoteFromA3(scale[i], 0.7f, waveform.sine));
            for (int i = 4 - 1; i >= 0; i--)
                workingBuffer.AddRange(ng.NoteFromA3(scale[i], 0.7f, waveform.sine));
                */
            save(FilePath);
            Play();
        }



        public void Play()
        {
            SoundPlayer player = new SoundPlayer(FilePath);
            player.Play();
        }



        // Saves the track to a wav file. A working buffer must exist.
        public void save(string filePath)
        {
            header = new WaveHeader();
            format = new WaveFormatChunk();
            data = new WaveDataChunk();

            convert();

            data.shortArray = convertedBuffer;
            // Calculate data chunk size in bytes
            data.dwChunkSize = (uint)(data.shortArray.Length * (format.wBitsPerSample / 8));


            FileStream f = new FileStream(filePath, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(f);
            // Write the header
            bw.Write(header.sGroupID.ToCharArray());
            bw.Write(header.dwFileLength);
            bw.Write(header.sRiffType.ToCharArray());

            // Write the format chunk
            bw.Write(format.sChunkID.ToCharArray());
            bw.Write(format.dwChunkSize);
            bw.Write(format.wFormatTag);
            bw.Write(format.wChannels);
            bw.Write(format.dwSamplesPerSec);
            bw.Write(format.dwAvgBytesPerSec);
            bw.Write(format.wBlockAlign);
            bw.Write(format.wBitsPerSample);

            // Write the data chunk
            bw.Write(data.sChunkID.ToCharArray());
            bw.Write(data.dwChunkSize);
            foreach (short dataPoint in data.shortArray)
            {
                bw.Write(dataPoint);
            }

            bw.Seek(4, SeekOrigin.Begin);
            uint filesize = (uint)bw.BaseStream.Length;
            bw.Write(filesize - 8);

            // Clean up
            bw.Close();
            f.Close();
        }



        // Converts the float values in th working buffer to pairs of bytes and stores them in xna buffer
        private void convert()
        {
            int bufferSize = workingBuffer.Count;
            convertedBuffer = new short[bufferSize];

            for (int i = 0; i < bufferSize; i++)
            {
                float floatSample = MathHelper.Clamp(workingBuffer[i], -1.0f, 1.0f);
                short shortSample = (short)(floatSample >= 0.0f ? short.MaxValue * floatSample : short.MinValue * floatSample * -1);
                convertedBuffer[i] = shortSample;
            }
        }




    }
}
