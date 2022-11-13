namespace ExpandedXml.Tests.Data
{
    public record Student
    {
        public Student() { }

        public Student(bool? isFreshman, string? name, double? age, DateTime? dateOfBirth, TimeSpan? wakeUpTime, Gender? gender)
        {
            IsFreshman = isFreshman;
            Name = name;
            Age = age;
            DateOfBirth = dateOfBirth;
            WakeUpTime = wakeUpTime;
            Gender = gender;
        }

        public bool? IsFreshman { get; set; }
        public string? Name { get; set; }
        public double? Age { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public TimeSpan? WakeUpTime { get; set; }
        public Gender? Gender { get; set; }

        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
    }

  
}
