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

namespace GCNet.PacketLib.Writer
{
    /// <summary>
    /// Represents the base class for the packet writers.
    /// </summary>
    public abstract class BaseWriter
    {
        /// <summary>
        /// Gets or sets the current data being written.
        /// </summary>
        protected byte[] Data { get; private set; } = new byte[0];


        /// <summary>
        /// Writes the specified bytes to the current data.
        /// </summary>
        /// <param name="bytes">The bytes to be written.</param>
        public void WriteData(byte[] bytes)
        {
            Data = Sequence.Concat(Data, bytes);
        }
    }
}