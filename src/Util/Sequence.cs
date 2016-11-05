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

using System;
using System.Linq;

namespace GCNet.Util
{
    /// <summary>
    /// Provides basic sequences handling functions.
    /// </summary>
    internal static class Sequence
    {
        /// <summary>
        /// Reads a range of elements from a sequence starting at a specified offset.
        /// </summary>
        /// <param name="source">The sequence from where the block will be read.</param>
        /// <param name="offset">the index where the reading begins.</param>
        /// <param name="length">The number of elements to be read.</param>
        /// <returns>The read block.</returns>
        public static T[] ReadBlock<T>(T[] source, int offset, int length)
        {
            var outputSeq = new T[length];
            Array.Copy(source, offset, outputSeq, 0, length);

            return outputSeq;
        }

        /// <summary>
        /// Concatenate N sequences.
        /// </summary>
        /// <param name="sequences">The sequences to be concatenated.</param>
        /// <returns>The N sequences concatenated.</returns>
        public static T[] Concat<T>(params T[][] sequences)
        {
            var outputSeq = new T[0];

            return sequences.Aggregate(outputSeq, Concat);
        }

        /// <summary>
        /// Concatenates two sequences.
        /// </summary>
        /// <param name="firstSeq">The first sequence to be concatenated.</param>
        /// <param name="secondSeq">The second sequence to be concatenated.</param>
        /// <returns>Both sequences concatenated.</returns>
        private static T[] Concat<T>(T[] firstSeq, T[] secondSeq)
        {
            var outputSeq = new T[firstSeq.Length + secondSeq.Length];

            Array.Copy(firstSeq, 0, outputSeq, 0, firstSeq.Length);
            Array.Copy(secondSeq, 0, outputSeq, firstSeq.Length, secondSeq.Length);

            return outputSeq;
        }
    }
}