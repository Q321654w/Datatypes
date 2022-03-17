using System;

namespace Datatypes
{
    public class Value : IEquals<Value>
    {
        private readonly byte[] _bytes;

        private readonly BitInByte _bitInByte;

        public int BitsCount { get; }

        public Value(byte[] bytes, BitInByte bitInByte) : this
        (
            bytes, bitInByte.BitsCount(bytes[bytes.Length - 1]) + Math.Max(bitInByte.Value() * (bytes.Length - 1), 0),
            bitInByte
        )
        {
        }

        public Value(int bitsCount, BitInByte bitInByte) : this(
            new byte[Math.DivRem(bitsCount, bitInByte.Value(), out var mod) + mod > 0 ? 1 : 0], bitsCount, bitInByte)
        {
        }

        public Value(byte[] bytes, int bitsCount, BitInByte bitInByte)
        {
            _bytes = bytes;
            BitsCount = bitsCount;
            _bitInByte = bitInByte;
        }

        public bool Bit(int index)
        {
            var byteNumber = Math.DivRem(index, _bitInByte.Value(), out var bitNumber);
            var selectedByte = _bytes[byteNumber];

            return (selectedByte >> bitNumber & 1) == 1;
        }

        public byte BitInDegree(int index)
        {
            var byteNumber = Math.DivRem(index, _bitInByte.Value(), out var bitNumber);
            var selectedByte = _bytes[byteNumber];

            var bitInDegree = selectedByte >> bitNumber;
            return (byte) bitInDegree;
        }

        public bool Equals(Value content)
        {
            if (_bytes.Length != content._bytes.Length)
                return false;

            if (BitsCount != content.BitsCount)
                return false;

            for (var i = 0; i < _bytes.Length; i++)
            {
                var currentByte = _bytes[i];
                var contentCurrentByte = content._bytes[i];

                if (currentByte != contentCurrentByte)
                    return false;
            }

            return true;
        }
    }
}