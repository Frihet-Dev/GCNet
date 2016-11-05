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

using System.Collections.Generic;
using GCNet.CoreLib;
using GCNet.PacketLib.Compression;
using GCNet.PacketLib.Writer;
using GCNet.Util;
using GCNet.Util.Endianness;

namespace GCNet.PacketLib
{
    /// <summary>
    /// Represents an outgoing packet.
    /// </summary>
    public sealed class OutPacket
    {
        /// <summary>
        /// Gets the packet buffer of the current outgoing packet.
        /// </summary>
        public byte[] Data { get; }


        /// <summary>
        /// Initializes a new instance of OutPacket for the keys definition packet (ID 0x0001) from the given
        /// payload data, crypto session, auth session and count.
        /// </summary>
        /// <param name="payloadData">The raw payload data.</param>
        /// <param name="crypto">The crypto session to be used.</param>
        /// <param name="auth">The auth session to be used.</param>
        /// <param name="firstCount">The first packet count.</param>
        public OutPacket(byte[] payloadData, CryptoSession crypto, AuthSession auth, int firstCount)
        {
            HeaderWriter headerWriter = new HeaderWriter();
            headerWriter.WriteData((short)(0));
            headerWriter.WriteData(firstCount);

            Data = AssemblePacket(payloadData, crypto, auth, headerWriter.GetHeader());
        }

        /// <summary>
        /// Initializes a new instance of OutPacket from the given payload data, crypto session, prefix and count.
        /// </summary>
        /// <param name="payloadData">The raw payload data.</param>
        /// <param name="crypto">The crypto session to be used.</param>
        /// <param name="auth">The auth session to be used.</param>
        /// <param name="prefix">The packet prefix.</param>
        /// <param name="count">The current count of sent packets.</param>
        public OutPacket(byte[] payloadData, CryptoSession crypto, AuthSession auth, short prefix, int count)
        {
            HeaderWriter headerWriter = new HeaderWriter();
            headerWriter.WriteData(prefix);
            headerWriter.WriteData(count);

            Data = AssemblePacket(payloadData, crypto, auth, headerWriter.GetHeader());
        }


        /// <summary>
        /// Assembles the current outgoing packet from the given payload data, crypto session, auth session and partial header.
        /// </summary>
        /// <param name="payloadData">The raw payload data.</param>
        /// <param name="crypto">The crypto session being used.</param>
        /// <param name="auth">The auth session being used.</param>
        /// <param name="partialHeader">The outer packet header, except for the 2 first bytes (packet size).</param>
        /// <returns>The assembled packet, ready to be sent.</returns>
        private static byte[] AssemblePacket(byte[] payloadData, CryptoSession crypto, AuthSession auth, byte[] partialHeader)
        {
            PayloadReader reader = new PayloadReader(payloadData, 6);
            if (reader.ReadBool())
            {
                payloadData = PayloadCompression.Compress(payloadData);
            }

            FixSize(payloadData);

            byte[] iv = Generate.IV();
            byte[] encryptedData = crypto.EncryptPacket(payloadData, iv);

            HeaderWriter headerWriter = new HeaderWriter();
            headerWriter.WriteData((short)(16 + encryptedData.Length + 10));
            headerWriter.WriteData(partialHeader);
            headerWriter.WriteData(iv);

            byte[] partialBuffer = Sequence.Concat(headerWriter.GetHeader(), encryptedData);
            byte[] hmac = auth.GetHmac(partialBuffer);

            return Sequence.Concat(partialBuffer, hmac);
        }

        /// <summary>
        /// Corrects the size contained in the payload data.
        /// </summary>
        /// <param name="payloadData">The raw payload data.</param>
        private static void FixSize(IList<byte> payloadData)
        {
            byte[] dataSizeBytes = BigEndian.GetBytes(payloadData.Count - 10);

            payloadData[2] = dataSizeBytes[0];
            payloadData[3] = dataSizeBytes[1];
            payloadData[4] = dataSizeBytes[2];
            payloadData[5] = dataSizeBytes[3];
        }
    }
}