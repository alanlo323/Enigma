using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Enigma
{
    public class EnigmaMachine
    {
        public const string SUPPORT_CHARACTER = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 ";
        //public const string SUPPORT_CHARACTER = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        //public const string SUPPORT_CHARACTER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private EnigmaMachine() : this(ringSize: 0, connectorSize: 0)
        {
        }

        private EnigmaMachine(int ringSize, int connectorSize)
        {
            Rings = new LinkedList<Ring>();

            for (int i = 0; i < ringSize; i++)
            {
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    List<int> v = new List<int>();
                    LinkedList<Connector> connectors = new LinkedList<Connector>();
                    Ring ring = new Ring(connectors);

                    for (int j = 0; j < connectorSize; j++) v.Add(j);
                    v = v.OrderBy(x => Util.Random.GetInt32(0, v.Count, rng)).ToList();

                    for (int j = 0; j < connectorSize; j++)
                    {
                        connectors.AddLast(new Connector() { Out = v[j] });
                    }

                    if (i == ringSize - 1) ring.ConvertToReflector();

                    AddRing(ring);
                }
            }
        }

        public int RingCount { get => Rings.Count; }

        [JsonProperty]
        private LinkedList<Ring> Rings { get; }

        public static EnigmaMachine Create(int ringSize = 0, int connectorSize = 0)
        {
            return new EnigmaMachine(ringSize: ringSize, connectorSize: connectorSize);
        }

        public static EnigmaMachine CreateDefault()
        {
            return new EnigmaMachine(ringSize: 4, connectorSize: SUPPORT_CHARACTER.Length);
        }

        public void AddRing(Ring ring)
        {
            if (ring == null)
            {
                throw new ArgumentNullException("ring");
            }

            Rings.AddLast(ring);
            if (Rings.Last.Previous != null) Rings.Last.Previous.Value.OnLapFininshedTriggers += Rings.Last.Value.OnLapFininshed;
        }

        public char GetOutput(char c)
        {
            int intInput = SUPPORT_CHARACTER.IndexOf(c);
            int intOutput = GetOutput(intInput);
            return SUPPORT_CHARACTER.ToCharArray().ElementAt(intOutput);
        }

        public int GetOutput(int input)
        {
            try
            {
                Rings.First.Value.Rotate(1);

                var stack = new Stack<Ring>();
                var enumerator = Rings.GetEnumerator();
                var lastInput = input;
                while (enumerator.MoveNext())
                {
                    stack.Push(enumerator.Current);

                    lastInput = enumerator.Current.GetOutValue(lastInput);
                }

                //  Reverse
                if (stack.Count > 0) stack.Pop();
                while (stack.Count > 0)
                {
                    lastInput = stack.Pop().GetInValue(lastInput);
                }

                return lastInput;
            }
            catch (Exception ex)
            {
                Rings.First.Value.Rotate(-1);
                throw ex;
            }

            throw new NotImplementedException();
        }

        public int GetRingConnectorsCount(int index)
        {
            return Rings.ElementAt(index).ConnectorsCount;
        }

        public int GetRingDegree(int index)
        {
            return Rings.ElementAt(index).Degree;
        }

        public void Reset()
        {
            foreach (var ring in Rings)
            {
                ring.Reset();
            }
        }

        public void Resolve()
        {
            this.Validate();
            this.ResolveTriggers();
        }

        public void ResolveTriggers()
        {
            var enumerator = Rings.GetEnumerator();
            var lastItem = enumerator.Current;
            while (enumerator.MoveNext())
            {
                if (lastItem != null && lastItem.OnLapFininshedTriggers != enumerator.Current.OnLapFininshed)
                    lastItem.OnLapFininshedTriggers += enumerator.Current.OnLapFininshed;
                lastItem = enumerator.Current;
            }
        }

        public void UpdateRingDegree(int index, int degree)
        {
            var ring = Rings.ElementAt(index);
            ring.Rotate(degree - ring.Degree);
        }

        public void Validate()
        {
            foreach (var ring in Rings)
            {
                if (ring == Rings.Last.Value)
                {
                    if (!ring.IsReflector)
                        throw new NotSupportedException($"Last ring must be a reflector");
                }
                else if (ring.IsReflector)
                {
                    throw new NotSupportedException($"Unsupported reflector location at index:{Rings.ToList().IndexOf(ring)}");
                }
                ring.Validate();
            }
        }
    }
}