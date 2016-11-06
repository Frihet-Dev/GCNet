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

using System.Security.Cryptography;

namespace GCNet.CoreLib
{
    /// <summary>
    /// Handles DES encryption operations.
    /// </summary>
    internal static class DesEncryption
    {
        /// <summary>
        /// Gets the cryptography provider used in the Grand Chase's encryption operations.
        /// </summary>
        private static DESCryptoServiceProvider DesProvider { get; } = new DESCryptoServiceProvider()
        {
            Mode = CipherMode.CBC,
            Padding = PaddingMode.None
        };

        /// <summary>
        /// Encrypts the specified byte array.
        /// </summary>
        /// <param name="data">The array of bytes to be encrypted.</param>
        /// <param name="iv">The initialization vector (IV).</param>
        /// <param name="key">The encryption key.</param>
        /// <returns>The encrypted data.</returns>
        public static byte[] EncryptData(byte[] data, byte[] iv, byte[] key)
        {
            ICryptoTransform encryptor = DesProvider.CreateEncryptor(key, iv);
            return encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        /// <summary>
        /// Decrypts the specified byte array.
        /// </summary>
        /// <param name="data">The array of bytes to be decrypted.</param>
        /// <param name="iv">The initialization vector (IV).</param>
        /// <param name="key">The decryption key.</param>
        /// <returns>The decrypted data.</returns>
        public static byte[] DecryptData(byte[] data, byte[] iv, byte[] key)
        {
            ICryptoTransform decryptor = DesProvider.CreateDecryptor(key, iv);
            return decryptor.TransformFinalBlock(data, 0, data.Length);
        }
    }
}