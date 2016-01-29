using FluentAssertions;
using Moq;
using NUnit.Framework;
using SoundConnect.Survey.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoundConnect.Survey.DBContext.Repository;
using Ploeh.AutoFixture;
using SoundConnect.Survey.Application.Common;
using Ploeh.AutoFixture.AutoMoq;
using System.Data.Common;
using SoundConnect.Survey.EntityFramework;
using System.Data.Entity;
using SoundConnect.Survey.DBContext.Tests.FakeObject;

namespace SoundConnect.Survey.DBContext.Tests
{
    [TestFixture]
    public class EntityRepositoryTest
    {
        private EntityRepository<FakeAnswer> _sut;

        private IFixture _fixture;

        private Mock<IDataContext> _dataContext;
        private Mock<DbSet<FakeAnswer>> _dbSet;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            DtoMappings.Map();
        }

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _dataContext = new Mock<IDataContext>();
            _dbSet = new Mock<DbSet<FakeAnswer>>();
            _dataContext.Setup(m => m.Set<FakeAnswer>()).Returns(_dbSet.Object);
            _sut = new EntityRepository<FakeAnswer>(_dataContext.Object);
        }

        [Test]
        public void GetById_WithValidId_CallFindMethod()
        {
            var id = _fixture.Create<int>();          
            _dbSet.Setup(m => m.Find(id)).Verifiable();

            var actual = _sut.GetById(id);

            _dbSet.VerifyAll();
        }

        [Test]
        public void GetById_WithValidId_ReturnValidObject()
        {
            var id = _fixture.Create<int>();
            var expected = _fixture.Create<FakeAnswer>();
            _dbSet.Setup(m => m.Find(id)).Returns(expected);

            var actual = _sut.GetById(id);

            actual.Should().NotBeNull();
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_EntityNull_ThrowArgumentNullException()
        {
            _sut.Create(null);
        }

        [Test]
        public void Create_EntityNotNull_CallFindMethod()
        {
            var expected = _fixture.Create<FakeAnswer>();
            _dbSet.Setup(m => m.Add(expected)).Returns(expected).Verifiable();

            var actual = _sut.Create(expected);

            actual.Should().NotBeNull();
            _dbSet.VerifyAll();
        }

        [Test]
        public void Create_EntityNotNull_CallSaveChangesMethod()
        {
            var expected = _fixture.Create<FakeAnswer>();
            _dbSet.Setup(m => m.Add(expected)).Returns(expected);
            _dataContext.Setup(m => m.SaveChanges()).Verifiable();

            var actual = _sut.Create(expected);

            actual.Should().NotBeNull();
            _dataContext.VerifyAll();
        }

        [Test]
        public void Create_EntityNotNull_ReturnTheObjectSaved()
        {
            var expected = _fixture.Create<FakeAnswer>();
            _dbSet.Setup(m => m.Add(expected)).Returns(expected);
            _dataContext.Setup(m => m.SaveChanges());

            var actual = _sut.Create(expected);

            actual.Should().NotBeNull();
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_EntityNull_ThrowArgumentNullException()
        {
            _sut.Update(null);
        }
        
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_EntityNull_ThrowArgumentNullException()
        {
            _sut.Delete(null);
        }

        [Test]
        public void Delete_EntityNotNull_CallRemoveMethod()
        {
            var expected = _fixture.Create<FakeAnswer>();
            _dbSet.Setup(m => m.Remove(expected)).Verifiable();

            _sut.Delete(expected);

            _dbSet.VerifyAll();
        }

        [Test]
        public void Delete_EntityNotNull_CallSaveChangesMethod()
        {
            var expected = _fixture.Create<FakeAnswer>();
            _dbSet.Setup(m => m.Remove(expected));
            _dataContext.Setup(m => m.SaveChanges()).Verifiable();

            _sut.Delete(expected);

            _dataContext.VerifyAll();
        }
    }
}