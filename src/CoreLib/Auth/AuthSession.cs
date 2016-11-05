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
    /// Represents a packet authentication session.
    /// </summary>
    public sealed class AuthSession
    {
        /// <summary>
        /// Gets the current session's HMAC key.
        /// </summary>
        public byte[] HmacKey { get; }


        /// <summary>
        /// Initializes a new instance of the AuthSession class using the default HMAC key.
        /// </summary>
        public AuthSession()
        {
            HmacKey = new byte[] { 0xC0, 0xD3, 0xBD, 0xC3, 0xB7, 0xCE, 0xB8, 0xB8 };
        }

        /// <summary>
        /// Initializes a new instance of the AuthSession class using the given HMAC key.
        /// </summary>
        /// <param name="hmacKey">The HMAC key which will be used in the auth session.</param>
        public AuthSession(byte[] hmacKey)
        {
            HmacKey = hmacKey;
        }

        
        /// <summary>
        /// Computes the HMAC for the specified packet data.
        /// </summary>
        /// <param name="partialBuffer">The whole packet buffer, except for the own HMAC.</param>
        /// <returns>The HMAC.</returns>
        public byte[] GetHmac(byte[] partialBuffer)
        {
            byte[] authData = Sequence.ReadBlock(partialBuffer, 2, partialBuffer.Length - 2);
            return MD5Hmac.ComputeHmac(authData, HmacKey, 10);
        }

        /// <summary>
        /// Checks the validity of the stored HMAC in the packet buffer.
        /// </summary>
        /// <param name="packetBuffer">The packet buffer the way it was received.</param>
        /// <returns>A boolean that indicates if the stored HMAC is whether or not valid.</returns>
        public bool VerifyHmac(byte[] packetBuffer)
        {
            byte[] storedHmac = Sequence.ReadBlock(packetBuffer, packetBuffer.Length - 10, 10);

            byte[] authData = Sequence.ReadBlock(packetBuffer, 2, packetBuffer.Length - 10 - 2);
            byte[] expectedHmac = MD5Hmac.ComputeHmac(authData, HmacKey, 10);

            return storedHmac.SequenceEqual(expectedHmac);
        }
    }
}