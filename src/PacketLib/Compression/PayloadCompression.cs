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

namespace GCNet.PacketLib.Compression
{
    /// <summary>
    /// Handles packet payload compression.
    /// </summary>
    internal static class PayloadCompression
    {
        /// <summary>
        /// Compresses the given payload data.
        /// </summary>
        /// <param name="payload">The uncompressed payload data.</param>
        /// <returns>The compressed payload data.</returns>
        public static byte[] Compress(byte[] payload)
        {
            byte[] innerHeader = Sequence.ReadBlock(payload, 0, 11);
            byte[] uncompressedData = Sequence.ReadBlock(payload, 11, payload.Length - 11);
            var end = new byte[3];

            byte[] compressedData = ZLib.CompressData(uncompressedData);
            
            return Sequence.Concat(innerHeader, compressedData, end);
        }

        /// <summary>
        /// Decompresses the given payload data.
        /// </summary>
        /// <param name="compressedPayload">The compressed payload data.</param>
        /// <returns>The decompressed payload data.</returns>
        public static byte[] Decompress(byte[] compressedPayload)
        {
            byte[] innerHeader = Sequence.ReadBlock(compressedPayload, 0, 11);
            byte[] compressedData = Sequence.ReadBlock(compressedPayload, 11, compressedPayload.Length - 11 - 3);

            byte[] decompressedData = ZLib.DecompressData(compressedData);

            return Sequence.Concat(innerHeader, decompressedData);
        }
    }
}