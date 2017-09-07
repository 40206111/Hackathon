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
    }
}
