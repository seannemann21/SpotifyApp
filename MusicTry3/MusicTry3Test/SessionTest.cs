using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MusicTry3.Models;
using System;
using System.Collections.Generic;

namespace MusicTry3Test
{
    [TestClass]
    public class SessionTest
    {

        [TestMethod]
        public void TestGenerate1()
        {
            var existingIds = new HashSet<int>();
            var rand = new Mock<Random>();
            rand.Setup(x => x.Next(Session.maxId)).Returns(1);
            var result = Session.GenerateId(existingIds, rand.Object);
            Assert.AreEqual("BAA", result);
        }

        [TestMethod]
        public void TestGenerate25()
        {
            var existingIds = new HashSet<int>();
            var rand = new Mock<Random>();
            rand.Setup(x => x.Next(Session.maxId)).Returns(25);
            var result = Session.GenerateId(existingIds, rand.Object);
            Assert.AreEqual("ZAA", result);
        }

        [TestMethod]
        public void TestGenerate26()
        {
            var existingIds = new HashSet<int>();
            var rand = new Mock<Random>();
            rand.Setup(x => x.Next(Session.maxId)).Returns(26);
            var result = Session.GenerateId(existingIds, rand.Object);
            Assert.AreEqual("ABA", result);
        }

        [TestMethod]
        public void TestGenerate0()
        {
            var existingIds = new HashSet<int>();
            var rand = new Mock<Random>();
            rand.Setup(x => x.Next(Session.maxId)).Returns(0);
            var result = Session.GenerateId(existingIds, rand.Object);
            Assert.AreEqual("AAA", result);
        }

        [TestMethod]
        public void TestGenerateMaxId()
        {
            var existingIds = new HashSet<int>();
            var rand = new Mock<Random>();
            rand.Setup(x => x.Next(Session.maxId)).Returns(Session.maxId);
            var result = Session.GenerateId(existingIds, rand.Object);
            Assert.AreEqual("ZZZ", result);
        }

        [TestMethod]
        public void TestGenerateSecondMax()
        {
            var existingIds = new HashSet<int>();
            var rand = new Mock<Random>();
            rand.Setup(x => x.Next(Session.maxId)).Returns(Session.maxId - 1);
            var result = Session.GenerateId(existingIds, rand.Object);
            Assert.AreEqual("YZZ", result);
        }

        [TestMethod]
        public void TestGenerateBCD()
        {
            var existingIds = new HashSet<int>();
            var rand = new Mock<Random>();
            rand.Setup(x => x.Next(Session.maxId)).Returns(3*26*26+2*26+1);
            var result = Session.GenerateId(existingIds, rand.Object);
            Assert.AreEqual("BCD", result);
        }

        [TestMethod]
        public void TestGenerateBAA()
        {
            var existingIds = new HashSet<int>();
            var rand = new Mock<Random>();
            rand.Setup(x => x.Next(Session.maxId)).Returns(26*26);
            var result = Session.GenerateId(existingIds, rand.Object);
            Assert.AreEqual("AAB", result);
        }

        [TestMethod]
        public void TestGenerateAll()
        {
            var existingIds = new HashSet<int>();
            var results = new HashSet<string>();
            var rand = new Mock<Random>();
            for(int i = 0; i <= Session.maxId + 5; i++)
            {
                rand.Setup(x => x.Next(Session.maxId)).Returns(i);
                results.Add(Session.GenerateId(existingIds, rand.Object));
            }
            
            //Session.maxId + 1 ids and 1 more error id
            Assert.AreEqual(Session.maxId + 2, results.Count);
        }
    }
}
