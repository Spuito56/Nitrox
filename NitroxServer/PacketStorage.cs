using NitroxModel.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NitroxServer
{
    public class PacketStorage
    {
        private HashSet<Type> packetPersistanceBlacklist;
        private List<Packet> packetHistory;

        public PacketStorage()
        {
            packetHistory = new List<Packet>();

            packetPersistanceBlacklist = new HashSet<Type>
            {
                typeof(Authenticate),
                typeof(AnimationChangeEvent),
                typeof(Movement),
                typeof(VehicleMovement),
                typeof(ItemPosition),
            };
        }

        public void Persist(Packet packet)
        {
            if (packetPersistanceBlacklist.Contains(packet.GetType()))
            {
                return;
            }
            
            Console.WriteLine("Persisting packet: " + packet.ToString());

            lock(packetHistory)
            {
                packetHistory.Add(packet);
            }
        }

        public List<Packet> GetHistory()
        {
            lock (packetHistory)
            {
                return new List<Packet>(packetHistory);
            }
        }
    }
}
