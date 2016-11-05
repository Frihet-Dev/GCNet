//-----------------------------------------------------------------------
// GCNet - A Grand Chase Networking Library
// Copyright © 2016  SyntaxDev
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
//-----------------------------------------------------------------------

using GCNet.CoreLib;
using GCNet.PacketLib.Compression;
using GCNet.PacketLib.Reader;

namespace GCNet.PacketLib
{
    /// <summary>
    /// Represents an incoming packet.
    /// </summary>
    public sealed class InPacket
    {
        /// <summary>
        /// Gets the (decrypted and uncompressed) payload data of the current packet.
        /// </summary>
        public byte[] PayloadData { get; }

        /// <summary>
        /// Gets the size of the current packet.
        /// </summary>
        public short Size { get; private set; }
        /// <summary>
        /// Gets the prefix of the current packet.
        /// </summary>
        public short Prefix { get; private set; }
        /// <summary>
        /// Gets the count in the current packet header.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the current packet ID.
        /// </summary>
        public short Id { get; private set; }


        /// <summary>
        /// Initializes a new instance of InPacket from the given packet buffer and crypto session.
        /// </summary>
        /// <param name="packetBuffer">The packet buffer the way it was received.</param>
        /// <param name="crypto">The current crypto session.</param>
        public InPacket(byte[] packetBuffer, CryptoSession crypto)
        {
            ParseHeader(packetBuffer);
            PayloadData = ProcessData(packetBuffer, crypto);
            
            PayloadReader reader = new PayloadReader(PayloadData);
            Id = reader.ReadInt16();
        }


        /// <summary>
        /// Processes the current packet data and returns the raw packet payload.
        /// </summary>
        /// <param name="packetBuffer">The packet buffer the way it was received.</param>
        /// <param name="crypto">The current crypto session.</param>
        /// <returns>The packet payload (decrypted and uncompressed).</returns>
        private static byte[] ProcessData(byte[] packetBuffer, CryptoSession crypto)
        {
            byte[] data = crypto.DecryptPacket(packetBuffer);

            PayloadReader reader = new PayloadReader(data, 6);
            if (reader.ReadBool())
            {
                data = PayloadCompression.Decompress(data);
            }
            return data;
        }

        /// <summary>
        /// Parses the header of the current packet and assigns the read values to the proper variables.
        /// </summary>
        /// <param name="packetBuffer">The packet buffer the way it was received.</param>
        private void ParseHeader(byte[] packetBuffer)
        {
            HeaderReader reader = new HeaderReader(packetBuffer);

            Size = reader.ReadInt16();
            Prefix = reader.ReadInt16();
            Count = reader.ReadInt32();
        }
    }
}