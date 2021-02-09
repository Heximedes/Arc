namespace Arc.Core.Distributed.Nodes
{
    public class ServerNodeServiceInfo : NodeServiceInfo
    {
        public short Version { get; set; }
        public string MinorVersion { get; set; }
        public byte Locale { get; set; }
        public string AKey { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
