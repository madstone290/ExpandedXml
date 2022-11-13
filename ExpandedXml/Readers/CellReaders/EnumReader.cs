using ClosedXML.Excel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ExpandedXml.Readers.CellReaders
{
    public class EnumReader : ICellReader
    {
        public ICellTextConverter? CellTextConverter { get; set; }

        public bool TryRead(IXLCell cell, Type valueType, out object? value)
        {
            if (!valueType.IsEnum)
            {
                value = null;
                return false;
            }

            cell.DataType = XLDataType.Text;
            var text = cell.GetString();

            // int 및 문자열 변환
            if (Enum.IsDefined(valueType, text) && Enum.TryParse(valueType, Convert.ToString(text), true, out object? result))
            {
                value = result!;
                return true;
            }

            // DisplayAttrite의 Name속성 이용
            var fields = valueType.GetFields();

            foreach (var field in fields)
            {
                var displayAttr = field.GetCustomAttribute<DisplayAttribute>();
                if (displayAttr == null)
                    continue;

                if (string.Equals(displayAttr.Name, Convert.ToString(text), StringComparison.OrdinalIgnoreCase))
                {
                    value = field.GetValue(null)!;
                    return true;
                }
            }

            // 변환 실패
            value = null;
            return false;

        }
    }
}
