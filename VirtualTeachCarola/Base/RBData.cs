using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualTeachCarola.Base
{
    class RBData
    {
        string baseValue = "";
        float minValue = 0.0f;
        float maxValue = 0.0f;

        public void Reset()
        {
            baseValue = "";
            minValue = 0.0f;
            maxValue = 0.0f;
        }

        public string BaseValue { get => baseValue; set => baseValue = value; }
        public float MinValue { get => minValue; set => minValue = value; }
        public float MaxValue { get => maxValue; set => maxValue = value; }
    }
}
