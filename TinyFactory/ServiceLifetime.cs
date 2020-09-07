using System;
using System.Collections.Generic;
using System.Text;

namespace TinyFactory
{
    public enum ServiceLifetime
    {
        Singleton = 0x01,
        Transient = 0x02,
        HostedService = 0x03,
        BroadcastService = 0x04,
        SingletonFirstLoader = 0x05,
        TransientFirstLoader = 0x06,
    }
}
