using System;
using System.Collections.Generic;
using FunExplorerBot.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace BotTests
{
    [TestClass]
    public class TestEventModel
    {
        [TestMethod]
        public void TestJsonify()
        {
            var e = new Event()
            {
                ChanelId = "facebook",
                CreatorId = "colinnddd",
                CreatorName = "cole",
                Description = "something",
                Location = "seattle",
                Tags = new List<Tag>() { Tag.Basketball,Tag.Food},
                Time = DateTime.Now,
          
            };

            var es = JsonConvert.SerializeObject(e);
            Assert.IsTrue(es.Contains("Location"));
        }
    }
}
