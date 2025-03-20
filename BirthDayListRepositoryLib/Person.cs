using System.Net.Security;

namespace BirthDayListRepositoryLib
{
    public class Person
    {
        public int Id { get; set; }

        public string? UserId { get; set; }
        public string? Name { get; set; }

        public int BirthYear { get; set; }
        public int BirthMonth { get; set; }
        public int BirthDayOfMonth { get; set; }

        public string? Remarks { get; set; }

        public string? PictureUrl { get; set; }

        public int Age
        {
            get
            {
                try
                {
                    DateTime then = new(BirthYear, BirthMonth, BirthDayOfMonth);
                    DateTime now = DateTime.UtcNow;
                    TimeSpan difference = now - then;
                    return difference.Days / 365; // better way, leap year!?
                } catch (ArgumentOutOfRangeException)
                {
                    return -1;
                }
            }
        }

        override
        public string ToString()
        { return $"{Id} {Name} {BirthYear} {BirthMonth} {BirthDayOfMonth} {Remarks}"; }

        public void Validate()
        {
            if (Name == null) throw new ArgumentNullException("Name is null");
            if (Name == "") throw new ArgumentException("Name is empty");
            if (UserId == null) throw new ArgumentNullException("UserId is null");
            new DateTime(BirthYear, BirthMonth, BirthDayOfMonth); // may throw ArgumentOutOfRangeException
            // https://learn.microsoft.com/en-us/dotnet/api/system.datetime.-ctor?view=net-7.0#system-datetime-ctor(system-int32-system-int32-system-int32)
        }
    }
}