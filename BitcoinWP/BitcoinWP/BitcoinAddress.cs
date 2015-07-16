using System;
using System.Collections;

namespace BitcoinWP
{
    public class BitcoinAddress
    {
        public BitcoinAddress(string Address)
        {
            WIF = Address;
        }
        public BitArray _bits;
        public byte[] bytes
        {
            get
            {
                byte[] bytes = new byte[32];
                for (int i = 0; i < 32; i++)
                {
                    bytes[i] = Convert.ToByte((_bits[i * 4] ? 128 : 0) +
                                              (_bits[i * 4 + 1] ? 64 : 0) +
                                              (_bits[i * 4 + 2] ? 32 : 0) +
                                              (_bits[i * 4 + 3] ? 16 : 0) +
                                              (_bits[i * 4 + 4] ? 8 : 0) +
                                              (_bits[i * 4 + 5] ? 4 : 0) +
                                              (_bits[i * 4 + 6] ? 2 : 0) +
                                              (_bits[i * 4 + 7] ? 1 : 0));
                }
                return bytes;
            }
            set
            {
                _bits = new BitArray(value);
            }
        }
        public string Hex
        {
            get
            {
                return BitConverter.ToString(bytes);
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
    }
}