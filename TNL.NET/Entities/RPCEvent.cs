﻿using System;

namespace TNL.NET.Entities
{
    using Structs;
    using Utils;

    public enum RPCDirection
    {
        RPCDirAny            = 1,
        RPCDirServerToClient = 2,
        RPCDirClientToServer = 3
    }

    public enum RPCGuaranteeType
    {
        RPCGuaranteedOrdered = 0,
        RPCGuaranteed        = 1,
        RPCUnguaranteed      = 2
    }

    public abstract class RPCEvent : NetEvent
    {
        public Functor Functor { get; set; }

        protected RPCEvent(RPCGuaranteeType gType, RPCDirection dir)
            : base((GuaranteeType) gType, (EventDirection) dir)
        {
        }

        public override void Pack(EventConnection ps, BitStream stream)
        {
            Functor.Write(stream);
        }

        public override void Unpack(EventConnection ps, BitStream stream)
        {
            Functor.Read(stream);
        }

        public abstract Boolean CheckClassType(Object obj);

        public override void Process(EventConnection ps)
        {
            if (CheckClassType(ps))
                Functor.Dispatch(ps);
        }
    }
}
