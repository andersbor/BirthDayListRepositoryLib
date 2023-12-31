﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using BirthDayListRepositoryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthDayListRepositoryLib.Tests
{
    [TestClass()]
    public class PersonsRepositoryTests
    {
        private readonly PersonsRepository repo = new();
        [TestMethod()]
        public void GetTest()
        {
            Assert.AreEqual(4, repo.Get().Count());
        }

        [TestMethod()]
        public void GetTest1()
        {
            List<Person> people = repo.Get(sortBy: "Next").ToList();
            Assert.AreEqual(4, people.Count);
            Assert.AreEqual("John Smith", people[0].Name); // as of today !?
        }

        [TestMethod()]
        public void AddTest()
        {
            Person p1 = new() { Name = "Thomas", BirthYear = 2006, BirthMonth = 7, BirthDayOfMonth = 7 };
            Person p2 = new() { Name = "Svend", BirthYear = 1942, BirthMonth = 12, BirthDayOfMonth = 30 };
            repo.Add(p1);
            Assert.AreEqual(5, repo.Get().Count());
            repo.Add(p2);
            Assert.AreEqual(6, repo.Get().Count());
        }

        [TestMethod()]
        public void RemoveTest()
        {

        }

        [TestMethod()]
        public void UpdateTest()
        {

        }
    }
}