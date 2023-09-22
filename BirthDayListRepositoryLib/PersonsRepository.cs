using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BirthDayListRepositoryLib
{
    public class PersonsRepository
    {
        private readonly List<Person> _persons = new();
        private int _nextID = 1;

        public PersonsRepository()
        {
            Add(new Person { Name = "Jane Doe", UserId = "anbo@zealand.dk", BirthYear = 1981, BirthMonth = 2, BirthDayOfMonth = 2 });
            Add(new Person { Name = "John Smith", UserId = "anbo@zealand.dk", BirthYear = 1982, BirthMonth = 3, BirthDayOfMonth = 3 });
            Add(new Person { Name = "Hans", UserId = "anbo@zealand.dk", BirthYear = 2000, BirthMonth = 1, BirthDayOfMonth = 1, Remarks = "Hans is a nice guy" });
            Add(new Person { Name = "Jane Smith", UserId = "anbo@zealand.dk", BirthYear = 1983, BirthMonth = 4, BirthDayOfMonth = 4 });
        }

        public IEnumerable<Person> Get(string? userId = null, string? sortBy = null, string? nameFragment = null, int? ageBelow = null, int? ageAbove = null)
        {
            List<Person> result = new List<Person>(_persons);
            if (userId != null)
                result = result.Where(p => p.UserId == null || p.UserId == userId).ToList();
            if (nameFragment != null)
                result = result.Where(p => p.Name != null && p.Name.Contains(nameFragment)).ToList();
            if (ageBelow != null)
                result = result.Where(p => p.Age <= ageBelow).ToList();
            if (ageAbove != null)
                result = result.Where(p => p.Age >= ageAbove).ToList();
            if (sortBy != null)
            {
                sortBy = sortBy.ToLower();
                switch (sortBy)
                {
                    case "name":
                        result = result.OrderBy(p => p.Name).ToList();
                        break;
                    case "age":
                        result = result.OrderBy(p => p.Age).ToList();
                        break;
                    case "birthday":
                        result.Sort((p1, p2) => CompareDates(p1, p2));
                        break;
                    case "next":
                        result.Sort((p1, p2) =>
                         {
                             int montDifference = p1.BirthMonth - p2.BirthMonth;
                             if (montDifference != 0) return montDifference;
                             return p1.BirthDayOfMonth - p2.BirthDayOfMonth;
                         });
                        //result = result.OrderBy(p => p.BirthMonth).ThenBy(p => p.BirthDayOfMonth);
                        //Person? e = result.FirstOrDefault(p => p.BirthMonth >= DateTime.UtcNow.Month && p.BirthDayOfMonth >= DateTime.UtcNow.Day);

                        break;
                    default:
                        break; // do nothing

                }
            }
            return result;
        }

        private bool Between(int month1, int day1, int month2, int day2, int month3, int day3)
        {
            if (month1 < month2) return true;
            if (month1 > month2) return false;
            if (day1 <= day2) return true;
            return false;
        }


        private static int CompareDates(Person p1, Person p2)
        {
            return CompareDates(p1.BirthMonth, p1.BirthDayOfMonth, p2.BirthMonth, p2.BirthDayOfMonth);

        }

        private static int CompareDates(int month1, int dayOfMonth1, int month2, int dayOfMonth2)
        {
            if (month1 < month2) return -1;
            if (month1 > month2) return 1;
            if (dayOfMonth1 < dayOfMonth2) return -1;
            if (dayOfMonth1 > dayOfMonth2) return 1;
            return 0;
        }

        public Person? Get(int id)
        {
            return _persons.FirstOrDefault(p => p.Id == id);
        }

        public Person Add(Person person)
        {
            person.Validate();
            person.Id = _nextID++;
            _persons.Add(person);
            return person;
        }

        public Person? Remove(int id)
        {
            Person? p = _persons.FirstOrDefault(p => p.Id == id);
            if (p == null) return null;
            _persons.Remove(p);
            return p;
        }

        public Person? Update(int id, Person data)
        {
            Person? person = _persons.Find(p => p.Id == id);
            if (person == null) return null;
            person.Name = data.Name;
            person.BirthYear = data.BirthYear;
            person.BirthMonth = data.BirthMonth;
            person.BirthDayOfMonth = data.BirthDayOfMonth;
            person.Remarks = data.Remarks;
            return person;
        }
    }
}
