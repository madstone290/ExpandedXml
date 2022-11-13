using ExpandedXml.Tests.Data;
using ExpandedXml.Writers;

namespace ExpandedXml.Tests
{
    public class WriterTests
    {
        public static IEnumerable<object[]> FilePathData => new List<object[]>
        {
            new object[] { Path.Combine(Directory.GetCurrentDirectory(), "Files", "test.xlsx") },
        };

        [Theory]
        [MemberData(nameof(FilePathData))]
        public void Write(string filePath)
        {
            IExWriter exWriter = new ExWriter();
            exWriter.Write<Student>(filePath, 
                new List<Student>()
                {
                    new Student(true, "John", 20, new DateTime(2002,1,1), TimeSpan.FromHours(6), Gender.Male),
                    new Student(false, "Alice", 22, new DateTime(2000,1,1), TimeSpan.FromHours(7), Gender.Female),
                },
                new List<ExColumnHeader>()
                {
                    new ExColumnHeader(nameof(Student.Name), "이름", typeof(string)),
                    new ExColumnHeader(nameof(Student.IsFreshman), "1학년?", typeof(bool)),
                    new ExColumnHeader(nameof(Student.Age), "나이", typeof(double)),
                    new ExColumnHeader(nameof(Student.Gender), "성별", typeof(Gender)),
                    new ExColumnHeader(nameof(Student.DateOfBirth), "생년월일", typeof(DateTime)),
                    new ExColumnHeader(nameof(Student.WakeUpTime), "기상시간", typeof(TimeSpan)),
                }
            );
        }
    }
}

