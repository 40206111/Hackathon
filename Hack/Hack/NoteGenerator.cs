using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack
{
    class NoteGenerator
    {
        private float[] notes;
        private int fadeTime = (int)(44100.0f / 1000.0f * 10.0f); // last number is fade time in miliseconds


        public NoteGenerator()
        {
            // all notes in an aray starting with A2
            notes = new float[36];
            notes[0] = 220.00f;
            notes[1] = 233.08f;
            notes[2] = 246.94f;
            notes[3] = 261.63f;
            notes[4] = 277.18f;
            notes[5] = 293.66f;
            notes[6] = 311.13f;
            notes[7] = 329.63f;
            notes[8] = 349.23f;
            notes[9] = 369.99f;
            notes[10] = 392.00f;
            notes[11] = 415.30f;
            notes[12] = 440.00f;
            notes[13] = 466.16f;
            notes[14] = 493.88f;
            notes[15] = 523.25f;
            notes[16] = 554.37f;
            notes[17] = 587.33f;
            notes[18] = 622.25f;
            notes[19] = 659.25f;
            notes[20] = 698.46f;
            notes[21] = 739.99f;
            notes[22] = 783.99f;
            notes[23] = 830.61f;
            notes[24] = 880.00f;
            notes[25] = 932.33f;
            notes[26] = 987.77f;
            notes[27] = 1046.50f;
            notes[28] = 1108.73f;
            notes[29] = 1174.66f;
            notes[30] = 1244.51f;
            notes[31] = 1318.51f;
            notes[32] = 1396.91f;
            notes[33] = 1479.98f;
            notes[34] = 1567.98f;
            notes[35] = 1661.22f;
        }

        public List<float> NoteFromA3(int interval, float duration, waveform form)
        {
            List<float> buff = new List<float>();
            for (int i = 0; i < 44100 * duration; i++)
            {
                // Time value for the oscilator might need tweaking
                switch (form)
                {
                    case waveform.sine:
                        buff.Add(Oscillator.Sine(notes[interval], i / 44100.0f));
                        break;
                    case waveform.sawtooth:
                        buff.Add(Oscillator.SawTooth(notes[interval], i / 44100.0f));
                        break;
                    case waveform.square:
                        buff.Add(Oscillator.Square(notes[interval], i / 44100.0f));
                        break;
                    case waveform.triangle:
                        buff.Add(Oscillator.Triangle(notes[interval], i / 44100.0f));
                        break;
                    default:
                        break;
                }
                
                // fade in fade out
                if (i < fadeTime)
                    buff[i] *= (i / (float)fadeTime);
                if (44100.0f * duration - i < fadeTime)
                    buff[i] *= ((44100.0f * duration - i) / (float)fadeTime);

            }

            // test
            buff = MixerClass.AddAttack(buff);

            return buff;
        }
    }
}
