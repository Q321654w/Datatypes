namespace Datatypes
{
    public class BitInByte
    {
        private const byte BIT_IN_BYTE = 8;

        public byte Value()
        {
            return BIT_IN_BYTE;
        }

        public int BitsCount(byte value)
        {
            for (var i = BIT_IN_BYTE - 1; i >= 0; i--)
            {
                if((value >> i & 1) == 1)
                    return i + 1;
                    
            }

            return 0;
        }
    }
}