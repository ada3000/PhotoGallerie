using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoGallerie.Data;

namespace PhotoGallerie.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void Crud()
        {
            UserRepository repo = new UserRepository();

            var expected = new User
            {
                Name = "UnitTest"
            };

            try
            {
                repo.Create(expected);

                var actual = repo.Get(expected.Id);

                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.Name, actual.Name);

                var actualNull = repo.Get(-1111);
                Assert.IsNull(actualNull);
            }
            finally
            {
                repo.Del(expected.Id);
            }
        }
    }
}
