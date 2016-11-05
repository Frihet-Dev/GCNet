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
using System.Security.Cryptography;

namespace GCNet.CoreLib
{
    /// <summary>
    /// Handles MD5 HMAC generation.
    /// </summary>
    internal static class MD5Hmac
    {
        /// <summary>
        /// Computes the HMAC for the specified data using the given key and size.
        /// </summary>
        /// <param name="data">The array of bytes which will be used in the hashing operation.</param>
        /// <param name="hmacKey">The HMAC key which will be used in the hashing operation.</param>
        /// <param name="digestSize">The resulting HMAC size.</param>
        /// <returns>The HMAC in the specified size.</returns>
        public static byte[] ComputeHmac(byte[] data, byte[] hmacKey, int digestSize)
        {
            HMACMD5 hmac = new HMACMD5(hmacKey);
            
            byte[] fullHmac = hmac.ComputeHash(data);
            return Sequence.ReadBlock(fullHmac, 0, digestSize);
        }
    }
}