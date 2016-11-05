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

namespace GCNet.Util.Endianness
{
    /// <summary>
    /// Provides handling functions for little endian data.
    /// </summary>
    internal static class LittleEndian
    {
        /// <summary>
        /// Converts 2 bytes from an array of bytes to a 16-bit integer at a specified index.
        /// </summary>
        /// <param name="bytes">The source byte array.</param>
        /// <param name="index">The index where the integer value begins.</param>
        /// <returns>A 16-bit integer.</returns>
        public static short GetInt16(byte[] bytes, int index)
        {
            return (short)
                ((bytes[index]) | (bytes[index + 1] << 8));
        }

        /// <summary>
        /// Converts 4 bytes from an array of bytes to a 32-bit integer at a specified index.
        /// </summary>
        /// <param name="bytes">The source byte array.</param>
        /// <param name="index">The index where the integer value begins.</param>
        /// <returns>A 32-bit integer.</returns>
        public static int GetInt32(byte[] bytes, int index)
        {
            return (bytes[index]) |
                   (bytes[index + 1] << 8) |
                   (bytes[index + 2] << 16) |
                   (bytes[index + 3]) << 24;
        }

        /// <summary>
        /// Converts a 16-bit integer to its little endian representation in bytes.
        /// </summary>
        /// <param name="int16">The integer to be converted.</param>
        /// <returns>The little endian representation of the integer in bytes.</returns>
        public static byte[] GetBytes(short int16)
        {
            var bytes = new byte[sizeof(short)];

            bytes[0] = (byte)(int16);
            bytes[1] = (byte)(int16 >> 8);

            return bytes;
        }

        /// <summary>
        /// Converts a 32-bit integer to its little endian representation in bytes.
        /// </summary>
        /// <param name="int32">The integer to be converted.</param>
        /// <returns>The little endian representation of the integer in bytes.</returns>
        public static byte[] GetBytes(int int32)
        {
            var bytes = new byte[sizeof(int)];

            bytes[0] = (byte)(int32);
            bytes[1] = (byte)(int32 >> 8);
            bytes[2] = (byte)(int32 >> 16);
            bytes[3] = (byte)(int32 >> 24);

            return bytes;
        }
    }
}