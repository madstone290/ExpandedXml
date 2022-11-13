using ClosedXML.Excel;

namespace ExpandedXml.Readers.CellReaders
{
    public class TimeSpanCellReader : CellReader<TimeSpan>
    {
        public ICellTextConverter<TimeSpan>? CustomReader { get; set; }

        public override bool TryRead(IXLCell cell, Type valueType, out object? value)
        {
            if (valueType != typeof(TimeSpan))
            {
                value = null;
                return false;
            }

            bool canRead;
            TimeSpan timespan;

            if (cell.DataType == XLDataType.TimeSpan)
            {
                canRead = cell.TryGetValue(out timespan);
            }
            else if (cell.DataType == XLDataType.DateTime)
            {
                timespan = cell.GetValue<DateTime>().TimeOfDay;
                canRead = true;
            }
            else if (CustomReader != null)
            {
                cell.DataType = XLDataType.Text;
                var text = cell.GetValue<string>();

                canRead = CustomReader.TryConvert(text, out timespan);
            }
            else
            {
                cell.DataType = XLDataType.TimeSpan;
                canRead = cell.TryGetValue(out timespan);
            }

            value = timespan;
            return canRead;
        }
    }
}
