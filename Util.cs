using System;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Enigma
{
    internal class Util
    {
        public class Random
        {
            private static RandomNumberGenerator _CprytoRNG;

            public static RandomNumberGenerator CprytoRNG
            {
                get
                {
                    if (_CprytoRNG == null) _CprytoRNG = RandomNumberGenerator.Create();
                    return _CprytoRNG;
                }
            }

            // Return a random integer between a min and max value.
            public static int GetInt32(int min, int max, RandomNumberGenerator rng = null)
            {
                if (rng == null) rng = CprytoRNG;

                // Generate four random bytes
                byte[] four_bytes = new byte[4];
                rng.GetBytes(four_bytes);

                // Convert the bytes to a UInt32
                UInt32 scale = BitConverter.ToUInt32(four_bytes, 0);

                // And use that to pick a random number >= min and < max
                return (int)(min + (max - min) * (scale / (uint.MaxValue + 1.0)));
            }
        }

        public class Timer
        {
            public static void Wait(int milliseconds)
            {
                var timer1 = new System.Windows.Forms.Timer();
                if (milliseconds == 0 || milliseconds < 0) return;

                // Console.WriteLine("start wait timer");
                timer1.Interval = milliseconds;
                timer1.Enabled = true;
                timer1.Start();

                timer1.Tick += (s, e) =>
                {
                    timer1.Enabled = false;
                    timer1.Stop();
                    // Console.WriteLine("stop wait timer");
                };

                while (timer1.Enabled)
                {
                    Application.DoEvents();
                }
            }
        }
    }
}