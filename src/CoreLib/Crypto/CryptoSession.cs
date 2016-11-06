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

using GCNet.Util;
using System.Linq;

namespace GCNet.CoreLib
{
    /// <summary>
    /// Represents a packet encryption session.
    /// </summary>
    public sealed class CryptoSession
    {
        /// <summary>
        /// Gets the current session's encryption key.
        /// </summary>
        public byte[] Key { get; }


        /// <summary>
        /// Initializes a new instance of the CryptoSession class using the default encryption key.
        /// </summary>
        public CryptoSession()
        {
            Key = new byte[] { 0xC7, 0xD8, 0xC4, 0xBF, 0xB5, 0xE9, 0xC0, 0xFD };
        }

        /// <summary>
        /// Initializes a new instance of the CryptoSession class using the given encryption key.
        /// </summary>
        /// <param name="key">The encryption key which will be used in the crypto session.</param>
        public CryptoSession(byte[] key)
        {
            Key = key;
        }


        /// <summary>
        /// Encrypts the given packet payload data.
        /// </summary>
        /// <param name="payload">The payload data to be encrypted.</param>
        /// <param name="iv">The initialization vector (IV).</param>
        /// <returns>The encrypted payload data.</returns>
        public byte[] EncryptPacket(byte[] payload, byte[] iv)
        {
            byte[] paddedData = PadData(payload);
            return DesEncryption.EncryptData(paddedData, iv, Key);
        }

        /// <summary>
        /// Decrypts the specified packet buffer.
        /// </summary>
        /// <param name="packetBuffer">The packet buffer the way it was received.</param>
        /// <returns>The decrypted packet data.</returns>
        public byte[] DecryptPacket(byte[] packetBuffer)
        {
            byte[] iv = Sequence.ReadBlock(packetBuffer, 8, 8);
            byte[] encryptedData = Sequence.ReadBlock(packetBuffer, 16, packetBuffer.Length - 10 - 16);

            byte[] decryptedData = DesEncryption.DecryptData(encryptedData, iv, Key);
            int paddingLength = (decryptedData.Last() + 2);

            return Sequence.ReadBlock(decryptedData, 0, decryptedData.Length - paddingLength);
        }

        /// <summary>
        /// Pads the specified data accordingly to the encryption padding of Grand Chase packets.
        /// </summary>
        /// <param name="data">The data to be padded.</param>
        /// <returns>The padded data.</returns>
        private byte[] PadData(byte[] data)
        {
            // It's a little wrong, I'll soon fix it.
            
            int paddingLength = 8 + ((8 - data.Length % 8) % 8);
            byte[] padding = new byte[paddingLength];

            int i = 0;
            while (i < (paddingLength - 1))
            {
                padding[i] = (byte)i;
                i++;
            }
            padding[i] = (byte)(i - 1);

            return Sequence.Concat(data, padding);
        }
    }
}
