using System.Collections.Generic;

namespace Datatypes
{
    public class IntConverter : IConverter<int>
    {
        private BitInByte _bitInByte;

        public IntConverter(BitInByte bitInByte)
        {
            _bitInByte = bitInByte;
        }

        public int ConvertTo(Value value)
        {
            var result = 0;

            for (int index = 0; index < value.BitsCount; index++)
            {
                var currentBit = value.Bit(index);
                if (currentBit)
                    result += 1 << index;
            }

            return result;
        }

        public Value ConvertFrom(int value)
        {
            var bitInByte = _bitInByte.Value();
            var bitStack = BitStack(value);

            var count = bitStack.Count;
            var byteCount = Ceil((float) count / bitInByte);
            var bitArray = BitArray(ref byteCount, bitStack, ref bitInByte);

            return new Value(bitArray, count, _bitInByte);
        }

        private byte[] BitArray(ref int byteCount, Stack<bool> bitStack, ref byte byteInBit)
        {
            var bitArray = new byte[byteCount];

            for (int i = 0; i < byteCount; i++)
            {
                byte currentByte = 0;

                for (int j = 0; j < byteInBit; j++)
                {
                    var bit = false;

                    if (bitStack.Count > 0)
                        bit = bitStack.Pop();

                    if (bit)
                    {
                        currentByte += (byte) (1 << j);
                    }
                }

                bitArray[i] = currentByte;
            }

            return bitArray;
        }

        private Stack<bool> BitStack(int value)
        {
            var bitStack = new Stack<bool>();

            for (int index = 0; index < value; index++)
            {
                var shiftedValue = value >> index;
                if (shiftedValue <= 0)
                    break;

                var bit = (shiftedValue & 1) == 1;
                bitStack.Push(bit);
            }

            return bitStack;
        }

        private int Ceil(float num)
        {
            int intNum = (int) num;

            if (num == intNum)
                return intNum;

            return intNum + 1;
        }
    }
}