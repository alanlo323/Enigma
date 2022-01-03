using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Queue<int> pendingConnector = new Queue<int>();
            var connectors = Connectors.ToArray();
            for (int i = 0; i < connectors.Length; i++)
            {
                if (!IsConnectorPaired(connectors, i))
                {
                    if (!IsConnectorPaired(connectors, connectors[i].Out))
                    {
                        pendingConnector.Enqueue(connectors[i].Out);
                        connectors[i].Out = i;
                    }
                    else
                    {
                        while (pendingConnector.Count > 0 && IsConnectorPaired(connectors, pendingConnector.Peek()))
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

            IsReflector = true;
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
                }
            }
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
                var matchedConnector = Connectors.ElementAt((int)inValue);
                return matchedConnector.Out;
            }
            if (outValue.HasValue)
            {
                var matchedConnector = Connectors
                    .Select((c, index) => (c, index))
                    .First(x => x.c.Out == (int)outValue);
                return matchedConnector.index;
            }

            throw new NotImplementedException();
        }

        private bool IsConnectorPaired(Connector[] connectors, int index)
        {
            return connectors[connectors[index].Out].Out == index;
        }

        public class BadConnectorException : InvalidOperationException
        {
            public BadConnectorException(string message) : base(message)
            {
            }
        }
    }
}