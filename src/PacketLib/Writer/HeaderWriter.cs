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

namespace GCNet.PacketLib.Writer
{
    /// <summary>
    /// Represents a packet writer for header data.
    /// </summary>
    internal sealed class HeaderWriter : BaseWriter
    {
        /// <summary>
        /// Gets the current header data.
        /// </summary>
        /// <returns>The current header data.</returns>
        public byte[] GetHeader()
        {
            return Data;
        }

        /// <summary>
        /// Writes the specified 16-bit integer to the current header data.
        /// </summary>
        /// <param name="int16">The 16-bit integer to be written.</param>
        public void WriteData(short int16)
        {            
            WriteData(LittleEndian.GetBytes(int16));
        }

        /// <summary>
        /// Writes the specified 32-bit integer to the current header data.
        /// </summary>
        /// <param name="int32">The 32-bit integer to be written.</param>
        public void WriteData(int int32)
        {
            WriteData(LittleEndian.GetBytes(int32));
        }
    }
}