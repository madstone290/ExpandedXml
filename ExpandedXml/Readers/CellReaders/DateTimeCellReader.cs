using ClosedXML.Excel;

namespace ExpandedXml.Readers.CellReaders
{
    public class DateTimeCellReader : CellReader<DateTime>
    {
        public ICellTextConverter<DateTime>? CustomReader { get; set; }

        public override bool TryRead(IXLCell cell, Type valueType, out object? value)
        {
            if (valueType != typeof(DateTime))
            {
                value = null;
                return false;
            }

            bool canRead;
            DateTime datetime;

            if (cell.DataType == XLDataType.DateTime)
            {
                canRead = cell.TryGetValue<DateTime>(out datetime);
            }
            else if (CustomReader != null)
            {
                cell.DataType = XLDataType.Text;
                var text = cell.GetValue<string>();
                
                canRead = CustomReader.TryConvert(text, out datetime);
            }
            else
            {
                cell.DataType = XLDataType.DateTime;
                canRead = cell.TryGetValue(out datetime);
            }
            value = datetime;
            return canRead;
        }
    }
}
