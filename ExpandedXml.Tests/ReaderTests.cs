using ClosedXML.Excel;
using ExpandedXml.Readers;
using ExpandedXml.Readers.CellReaders;
using ExpandedXml.Readers.CellReaders.CellTextConverters;
using ExpandedXml.Tests.Data;
using FluentAssertions;

namespace ExpandedXml.Tests
{
    public class ReaderTests
    {
        public static IEnumerable<object[]> StudentTableData => new List<object[]>
        {
            new object[] { Path.Combine(Directory.GetCurrentDirectory(), "Files", "StudentTable.xlsx") },
        };

        [Theory]
        [MemberData(nameof(StudentTableData))]
        public void Read_Table(string filePath)
        {
            var columnHeaders = new List<ExColumnHeader>()
            {
                new ExColumnHeader("DateOfBirth", "DateOfBirth", typeof(DateTime)),
                new ExColumnHeader("WakeUpTime", "WakeUpTime", typeof(TimeSpan)),
                new ExColumnHeader("Name", "Name", typeof(string)),
                new ExColumnHeader("IsFreshman", "IsFreshman", typeof(bool)),
                new ExColumnHeader("Age", "Age", typeof(int)),
                new ExColumnHeader("Gender", "Gender", typeof(Gender)),
                new ExColumnHeader("StartTime", "시작시간", typeof(TimeSpan)),
                new ExColumnHeader("EndTime", "종료시간", typeof(TimeSpan)),
            };

            var data = File.ReadAllBytes(filePath);
            var stream = new MemoryStream(data);
            var workbook = new XLWorkbook(stream);
            var sheet = workbook.Worksheet(1);

            var reader = new ExReader();
            var boolTextConvertet = new BoolTextConverter();
            boolTextConvertet.AddTrueString("예");
            boolTextConvertet.AddTrueString("응");

            reader.BlockReader.RowReader.AddCellTextConverter(typeof(bool), boolTextConvertet);

            var studentList = reader.Read<Student>(sheet.RowsUsed(), sheet.ColumnsUsed(), columnHeaders);
        }

    }
}