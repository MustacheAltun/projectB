using System;
using Xunit;
using projectB;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace projectB.Tests
{
    public class LoginTests
    {
        
        [Theory]
        [InlineData ("test123", "test123", true)]
        [InlineData ("test", "test", false)]
        [InlineData("test", "test1", false)]
        [InlineData("Test123", "test123", false)]
        public void validateCredentials(string username, string password, bool expected)
        {
            List<Account> movieList = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText("..\\..\\..\\accountTest.json"));
            
            bool actual = Login.loginMethod(movieList, username, password);

            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("test123", "test123", 0)]
        [InlineData("Kawish", "test123", 101)]
        [InlineData("kawish", "test123", -1)]
        [InlineData("Marc-Shaquille", "test123", -1)]
        [InlineData("marc-Shaquille", "test123456789", -1)]
        [InlineData("kawish", "test123456789", -1)]
        [InlineData("Marc-Shaquille", "test123456789", 102)]
        public void fetchAccountIDTest(string username, string password, int expected)
        {
            List<Account> movieList = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText("..\\..\\..\\accountTest.json"));
            int actual = Login.getId(movieList, username, password);
            Assert.Equal(expected, actual);
        }
    }
    
}
