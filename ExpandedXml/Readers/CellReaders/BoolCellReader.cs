using ClosedXML.Excel;

namespace ExpandedXml.Readers.CellReaders
{
    public class BoolCellReader : CellReader<bool>
    {
        public ICellTextConverter<bool>? CustomBoolReader => (ICellTextConverter<bool>?) CellTextConverter;

        public override bool TryRead(IXLCell cell, Type valueType, out object? value)
        {
            if (valueType != typeof(bool))
            {
                value = null;
                return false;
            }

            bool canRead;
            bool boolValue;
            
            if (cell.DataType == XLDataType.Boolean)
            {
                canRead = cell.TryGetValue<bool>(out boolValue);
            }
            else if(CustomBoolReader != null)
            {
                cell.DataType = XLDataType.Text;
                var text = cell.GetValue<string>();

                canRead = CustomBoolReader.TryConvert(text, out boolValue);
            }
            else
            {
                canRead = cell.TryGetValue(out boolValue);
            }

            value = boolValue;
            return canRead;
        }
    }
}
