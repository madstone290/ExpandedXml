using ClosedXML.Excel;
using ExpandedXml.Readers.CellReaders;

namespace ExpandedXml.Readers
{
    public abstract class CellReader<TValueType> : ICellReader<TValueType>
    {
        public ICellTextConverter? CellTextConverter { get; set; }

        public bool TryRead(IXLCell cell, out TValueType value)
        {
            var canRead = ((ICellReader)this).TryRead(cell, typeof(TValueType), out object? obj);
            value = obj == null ? default! : (TValueType)obj;

            return canRead;
        }

        public abstract bool TryRead(IXLCell cell, Type valueType, out object? value);
    }
}
