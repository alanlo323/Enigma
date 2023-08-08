using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Enigma
{
    public class Ring
    {
        [JsonIgnore]
        public Del_OnLapFininshed OnLapFininshedTriggers;

        public Ring(LinkedList<Connector> connectors, int degree = 0)
        {
            Connectors = connectors ?? throw new ArgumentNullException("connectors");
            Degree = degree;
        }

        public delegate void Del_OnLapFininshed(Ring ring, Lap lap);

        public enum Lap
        {
            Positive,
            Negative
        }

        public int ConnectorsCount { get => Connectors.Count; }

        public int Degree { get; private set; }

        [JsonProperty]
        public bool IsReflector { get; private set; }

        [JsonProperty]
        private LinkedList<Connector> Connectors { get; }

        public void ConvertToReflector()
        {
            this.IsReflector = true;
            Queue<int> pendingConnector = new Queue<int>();

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                while (!IsValidReflector())
                {
                    var v = Connectors.ToList(); ;
                    v = v.OrderBy(x => Util.Random.GetInt32(0, v.Count, rng)).ToList();

                    var connectors = v.ToArray();
                    for (int i = 0; i < connectors.Length; i++)
                    {
                        if (!IsConnectorPaired(connectors, i, allowSelfPaired: false))
                        {
                            if (!IsConnectorPaired(connectors, connectors[i].Out, allowSelfPaired: false))
                            {
                                pendingConnector.Enqueue(connectors[connectors[i].Out].Out);
                                connectors[connectors[i].Out].Out = i;
                            }
                            else
                            {
                                while (pendingConnector.Count > 0 && IsConnectorPaired(connectors, pendingConnector.Peek(), allowSelfPaired: false))
                                {
                                    pendingConnector.Dequeue();
                                }

                                if (pendingConnector.Count > 0)
                                {
                                    connectors[i].Out = pendingConnector.Dequeue();
                                    pendingConnector.Enqueue(connectors[connectors[i].Out].Out);
                                    connectors[connectors[i].Out].Out = connectors[i].Out;
                                }
                                else
                                {
                                    pendingConnector.Enqueue(i);
                                }
                            }
                        }
                        connectors[connectors[i].Out].Out = i;
                    }

                    Connectors.Clear();
                    for (int j = 0; j < connectors.Length; j++) Connectors.AddLast(new Connector() { Out = connectors[j].Out });
                }
            }

            Validate();
        }

        public int GetInValue(int outValue)
        {
            return GetValue(outValue: outValue);
        }

        public int GetOutValue(int inValue)
        {
            return GetValue(inValue: inValue);
        }

        public void OnLapFininshed(Ring ring, Lap lap)
        {
            switch (lap)
            {
                case Lap.Positive:
                    Rotate(1);
                    break;

                case Lap.Negative:
                    Rotate(-1);
                    break;
            }
        }

        public void Reset()
        {
            Rotate(Degree * -1, triggerChainEffect: false);
        }

        public void Rotate(int offset, bool triggerChainEffect = true)
        {
            while (offset != 0)
            {
                if (offset > 0)
                {
                    var first = Connectors.First;
                    Connectors.RemoveFirst();
                    Connectors.AddLast(first);

                    Degree++;
                    if (Degree >= Connectors.Count)
                    {
                        Degree = 0;
                        if (triggerChainEffect && OnLapFininshedTriggers?.Target != null)
                            OnLapFininshedTriggers(this, Lap.Positive);
                    }

                    offset--;
                }
                else
                {
                    var last = Connectors.Last;
                    Connectors.RemoveLast();
                    Connectors.AddFirst(last);

                    Degree--;
                    if (Degree < 0)
                    {
                        Degree = Connectors.Count - 1;
                        if (triggerChainEffect && OnLapFininshedTriggers?.Target != null)
                            OnLapFininshedTriggers(this, Lap.Negative);
                    }

                    offset++;
                }
            }
        }

        public void Validate()
        {
            if (IsReflector)
            {
                var connectors = Connectors.ToArray();
                for (int i = 0; i < connectors.Length; i++)
                {
                    if (connectors[connectors[i].Out].Out != i)
                        throw new BadConnectorException($"Ring is marked as reflector but connectors are not paired ({i}:{connectors[i].Out} {connectors[i].Out}:{connectors[connectors[i].Out].Out})");
                    if (connectors[connectors[i].Out].Out == connectors[i].Out)
                        throw new BadConnectorException($"Ring is marked as reflector but connectors are self paired ({i}:{connectors[i].Out} {connectors[i].Out}:{connectors[connectors[i].Out].Out})");
                }
            }
        }

        public bool IsValidReflector()
        {
            if (IsReflector)
            {
                var connectors = Connectors.ToArray();
                for (int i = 0; i < connectors.Length; i++)
                {
                    if (connectors[connectors[i].Out].Out != i)
                        return false;
                    if (connectors[connectors[i].Out].Out == connectors[i].Out)
                        return false;
                }
            }

            return true;
        }

        private int GetValue(int? inValue = null, int? outValue = null)
        {
            if (inValue.HasValue && outValue.HasValue)
            {
                throw new NotSupportedException("");
            }

            if (!inValue.HasValue && !outValue.HasValue)
            {
                throw new ArgumentNullException("inValue & outValue");
            }

            if (inValue.HasValue)
            {
                var matchedConnector = Connectors.ElementAt(inValue.Value);
                return matchedConnector.Out;
            }
            if (outValue.HasValue)
            {
                var matchedConnector = Connectors
                    .Select((c, index) => (c, index))
                    .First(x => x.c.Out == outValue.Value);
                return matchedConnector.index;
            }

            throw new NotImplementedException();
        }

        private bool IsConnectorPaired(Connector[] connectors, int index, bool allowSelfPaired = true)
        {
            bool result = connectors[connectors[index].Out].Out == index;
            if (!allowSelfPaired) result &= connectors[index].Out != connectors[connectors[index].Out].Out;
            return result;
        }

        public class BadConnectorException : InvalidOperationException
        {
            public BadConnectorException(string message) : base(message)
            {
            }
        }
    }
}