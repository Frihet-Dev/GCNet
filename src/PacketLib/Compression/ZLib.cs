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

using Ionic.Zlib;
using System.IO;

namespace GCNet.PacketLib.Compression
{
    /// <summary>
    /// Handles ZLib compression operations.
    /// </summary>
    internal static class ZLib
    {
        /// <summary>
        /// Compresses the specified block of data.
        /// </summary>
        /// <param name="data">The data to be compressed.</param>
        /// <returns>The compressed data.</returns>
        public static byte[] CompressData(byte[] data)
        {
            MemoryStream ms = new MemoryStream();

            using (ZlibStream compressor = new ZlibStream(ms, CompressionMode.Compress, CompressionLevel.Level1))
            {
                compressor.Write(data, 0, data.Length);
            }
            return ms.ToArray();
        }

        /// <summary>
        /// Decompresses the specified block of data.
        /// </summary>
        /// <param name="data">The data to be decompressed.</param>
        /// <returns>The decompressed data.</returns>
        public static byte[] DecompressData(byte[] data)
        {
            return ZlibStream.UncompressBuffer(data);
        }
    }
}