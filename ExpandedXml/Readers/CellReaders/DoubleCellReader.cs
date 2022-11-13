using ClosedXML.Excel;

namespace ExpandedXml.Readers.CellReaders
{
    public class DoubleCellReader : CellReader<double>, 
        ICellReader<ushort>, ICellReader<short>,
        ICellReader<uint>, ICellReader<int>,
        ICellReader<ulong>, ICellReader<long>,
        ICellReader<float>, ICellReader<decimal>, ICellReader<byte>
    {
        private readonly HashSet<Type> _conversionTypes = new HashSet<Type>()
        {
            typeof(double),
            typeof(ushort),
            typeof(short),
            typeof(uint),
            typeof(int),
            typeof(ulong),
            typeof(long),
            typeof(float),
            typeof(decimal),
            typeof(byte),
        };

        public ICellTextConverter<double>? CustomReader { get; set; }

        public override bool TryRead(IXLCell cell, Type valueType, out object? value)
        {
            if (!_conversionTypes.Contains(valueType))
            {
                value = null;
                return false;
            }

            bool canRead;
            double doubleValue;
            if (cell.DataType == XLDataType.Number)
            {
                canRead = cell.TryGetValue<double>(out doubleValue);
            }
            else if(CustomReader != null)
            {
                cell.DataType = XLDataType.Text;
                var text = cell.GetValue<string>();

                canRead = CustomReader.TryConvert(text, out doubleValue);
            }
            else
            {
                canRead = cell.TryGetValue<double>(out doubleValue);
            }

            value = Convert.ChangeType(doubleValue, valueType);
            return canRead;
        }

        public bool TryRead<TValueType>(IXLCell cell, out TValueType value)
        {
            bool canRead = TryRead(cell, typeof(TValueType), out object? objValue);
            value = objValue == null ? default! : (TValueType)objValue;

            return canRead;
        }

        bool ICellReader<uint>.TryRead(IXLCell cell, out uint value)
        {
            return TryRead<uint>(cell, out value);
        }

        bool ICellReader<int>.TryRead(IXLCell cell, out int value)
        {
            return TryRead<int>(cell, out value);
        }

        bool ICellReader<ulong>.TryRead(IXLCell cell, out ulong value)
        {
            return TryRead<ulong>(cell, out value);
        }

        bool ICellReader<long>.TryRead(IXLCell cell, out long value)
        {
            return TryRead<long>(cell, out value);
        }

        bool ICellReader<short>.TryRead(IXLCell cell, out short value)
        {
            return TryRead<short>(cell, out value);
        }

        bool ICellReader<ushort>.TryRead(IXLCell cell, out ushort value)
        {
            return TryRead<ushort>(cell, out value);
        }

        bool ICellReader<float>.TryRead(IXLCell cell, out float value)
        {
            return TryRead<float>(cell, out value);
        }

        bool ICellReader<decimal>.TryRead(IXLCell cell, out decimal value)
        {
            return TryRead<decimal>(cell, out value);
        }

        bool ICellReader<byte>.TryRead(IXLCell cell, out byte value)
        {
            return TryRead<byte>(cell, out value);
        }
    }
}
