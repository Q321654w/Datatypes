namespace Datatypes
{
    public class IntConverter : IConverter<int>
    {
        private readonly BitInByte _bitInByte;

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

            var bitCount = BitCount(value);
            var bitArray = BitArray(ref bitCount, ref value, ref bitInByte);

            return new Value(bitArray, bitCount, _bitInByte);
        }

        private byte[] BitArray(ref int bitCount, ref int value, ref byte bitInByte)
        {
            var byteCount = Ceil((float) bitCount / bitInByte);
            var bitArray = new byte[byteCount];

            for (var i = 0; i < byteCount; i++)
            {
                var currentByte = 0;

                for (var j = 0; j < bitInByte; j++)
                {
                    if ((value & 1) == 0)
                    {
                        value >>= 1;
                        continue;
                    }

                    currentByte += 1 << j;
                    value >>= 1;
                }

                bitArray[i] = (byte) currentByte;
            }

            return bitArray;
        }

        private int BitCount(int value)
        {
            var count = 0;
            
            while (value > 0)
            {
                value >>= 1;
                count++;
            }

            return count;
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