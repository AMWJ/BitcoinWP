using System;
using System.Collections;

namespace BitcoinWP
{
    public class PublicKey
    {
        private static string Base58Alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
        public BitArray Bits;
        public PublicKey(BitArray Key)
        {
            this.Bits = Key;
        }
        public PublicKey(string Key)
        {
            this.WIF = WIF; 
        }
        public byte[] Bytes
        {
            get
            {
                byte[] bytes = new byte[32];
                for (int i = 0; i < 32; i++)
                {
                    bytes[i] = Convert.ToByte((Bits[i * 4] ? 128 : 0) +
                                              (Bits[i * 4 + 1] ? 64 : 0) +
                                              (Bits[i * 4 + 2] ? 32 : 0) +
                                              (Bits[i * 4 + 3] ? 16 : 0) +
                                              (Bits[i * 4 + 4] ? 8 : 0) +
                                              (Bits[i * 4 + 5] ? 4 : 0) +
                                              (Bits[i * 4 + 6] ? 2 : 0) +
                                              (Bits[i * 4 + 7] ? 1 : 0));
                }
                return bytes;
            }
            set
            {
                Bits = new BitArray(value);
            }
        }
        public string Hex
        {
            get
            {
                return BitConverter.ToString(Bytes);
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        private string WIF
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        private string compressedWIF
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        private string compressedHex
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public PublicKey GetAddress()
        {
            throw new NotImplementedException();
        }
    }
}