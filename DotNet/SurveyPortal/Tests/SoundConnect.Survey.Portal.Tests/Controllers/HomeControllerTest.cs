using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SoundConnect.Survey.Portal;
using SoundConnect.Survey.Portal.Controllers;
using NUnit.Framework;
using FluentAssertions;

namespace SoundConnect.Survey.Portal.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private HomeController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new HomeController();
        }

        [Test]
        public void Index_ViewNotNull_ReturnViewResult()
        {
            var actual = _controller.Index();

            actual.Should().NotBeNull();
            actual.Should().BeOfType<ViewResult>();
        }

        [Test]
        public void About_ViewNotNull_ReturnViewResult()
        {
            var actual = _controller.About();

            actual.Should().NotBeNull();
            actual.Should().BeOfType<ViewResult>();
        }

        [Test]
        public void Contact_ViewNotNull_ReturnViewResult()
        {
            var actual = _controller.Contact();

            actual.Should().NotBeNull();
            actual.Should().BeOfType<ViewResult>();
        }
    }
}
