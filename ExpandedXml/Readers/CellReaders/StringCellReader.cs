using ClosedXML.Excel;

namespace ExpandedXml.Readers.CellReaders
{
    public class StringCellReader : CellReader<string>
    {
        public override bool TryRead(IXLCell cell, Type valueType, out object? value)
        {
            if (valueType != typeof(string))
            {
                value = null;
                return false;
            }

            bool canRead;
            string stringValue;

            if (cell.DataType == XLDataType.Text)
            {
                canRead = cell.TryGetValue<string>(out stringValue);
            }
            else
            {
                cell.DataType = XLDataType.Text;

                canRead = cell.TryGetValue(out stringValue);
            }

            value = stringValue;
            return canRead;
        
        }
    }
}
