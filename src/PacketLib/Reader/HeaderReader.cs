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

using GCNet.Util.Endianness;

namespace GCNet.PacketLib.Reader
{
    /// <summary>
    /// Represents a packet reader for header data.
    /// </summary>
    internal class HeaderReader : BaseReader
    {
        /// <summary>
        /// Initializes a new instance of HeaderReader using the specified data.
        /// </summary>
        /// <param name="packetBuffer">The packet buffer the way it was received.</param>
        public HeaderReader(byte[] packetBuffer) : base(packetBuffer) { }

        
        /// <summary>
        /// Reads a 16-bit integer from the current header data and advances the current position by 2 bytes.
        /// </summary>
        /// <returns>The next 16-bit integer.</returns>
        public short ReadInt16()
        {
            short readShort = LittleEndian.GetInt16(Data, Position);
            Position += sizeof(short);

            return readShort;
        }

        /// <summary>
        ///  Reads a 32-bit integer from the current header data and advances the current position by 4 bytes.
        /// </summary>
        /// <returns>The next 32-bit integer.</returns>
        public int ReadInt32()
        {
            int readInt = LittleEndian.GetInt32(Data, Position);
            Position += sizeof(int);

            return readInt;
        }
    }
}