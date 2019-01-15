using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AxShockwaveFlashObjects;
using VirtualTeachCarola.Base;

namespace VirtualTeachCarola
{
    internal class Device
    {
        int valueType = 0;
        System.Threading.Timer threadTime;
        AxShockwaveFlashObjects.AxShockwaveFlash flashContrl = null;
        string tipValue = "";
        RBData rbValue = new RBData();
        RBData bbValue = new RBData();
        DataRow target = null;
        Boolean canYouMen = false;

        public int ValueType { get => valueType; set => valueType = value; }
        public Timer ThreadTime { get => threadTime; set => threadTime = value; }
        public AxShockwaveFlash FlashContrl { get => flashContrl; set => flashContrl = value; }
        public RBData RbValue { get => rbValue; set => rbValue = value; }
        public RBData BbValue { get => bbValue; set => bbValue = value; }
        public string TipValue { get => tipValue; set => tipValue = value; }
        public DataRow Target { get => target; set => target = value; }
        public Boolean CanYouMen { get => canYouMen; set => canYouMen = value; }
    }
}
