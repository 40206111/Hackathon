using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack
{
    public static class Oscillator
    {
        public static float Sine(float freq, float time)
        {
            return (float)Math.Sin(freq * time * 2.0f * Math.PI);
        }
        public static float Triangle(float freq, float time)
        {
            return (float)Math.Abs(2 * (time * freq - Math.Floor(time * freq + 0.5))) * 2.0f - 1.0f;
        }
        public static float SawTooth(float freq, float time)
        {
            return (float)(2.0f * (time * freq - Math.Floor(time * freq + 0.5)));
        }
        public static float Square(float freq, float time)
        {
            return (float)Math.Sin(freq * time * 2 * Math.PI) >= 0 ? 1 : -1;
        }
    }
}
