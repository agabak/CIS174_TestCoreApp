using CIS174_TestCoreApp.Controllers;
using CIS174_TestCoreApp.Models;
using CIS174_TestCoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace CIS174_TestCoreApp.Test.Controllers
{
    public class AccomplishmentController_Test
    {
        private readonly Mock<IAccomplishmentService> _mockService;
        private readonly Mock<IAuthorizationService> _mockAuth;
        private readonly AccomplishmentController _controller;
        private ClaimsPrincipal fakeUser;
        private readonly Mock<ILogger<AccomplishmentController>> _mockLogger;

        public AccomplishmentController_Test()
        {
            _mockService = new Mock<IAccomplishmentService>();
            _mockAuth = new Mock<IAuthorizationService>();
            _mockLogger = new Mock<ILogger<AccomplishmentController>>();
            _controller = new AccomplishmentController(_mockService.Object, _mockAuth.Object, _mockLogger.Object);
            var context = new ControllerContext
                        {
                            HttpContext = new DefaultHttpContext
                            {
                                User = fakeUser
                            }
                        };

            _controller.ControllerContext = context;

            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "123"),
                    new Claim(ClaimTypes.Name, "Test user"),
                    new Claim(ClaimTypes.Email, "test@example.com"),
                    new Claim("Admin", "test@example.com")
                }));
        }

        [Fact]
        public void List_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.List();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void List_ActionExecutes_ReturnsExactAccomplishment()
        {
             _mockService.Setup(repo => repo.GetAccomplishments())
                        .Returns(new List<PersonAccomplishmentViewModel>()
                        { new PersonAccomplishmentViewModel(), new PersonAccomplishmentViewModel() });

            var result = _controller.List();

            var viewResult = Assert.IsType<ViewResult>(result);
            var personAccomplishments = Assert.IsType<List<PersonAccomplishmentViewModel>>(viewResult.Model);
            Assert.Equal(2, personAccomplishments.Count);
        }

        [Fact]
        public void Create_ActionExecutes_ReturnsViewForCreate()
        {
            var result = _controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_InvalidModelState_ReturnsView()
        {
            _controller.ModelState.AddModelError("FirstName", "Name is required");

            var personAccomplishment = new PersonAccomplishmentViewModel { LastName ="Test LastName", City = "CityTest", State = "Test state", DateOfBirth = new DateTime(1978,4,5)};

            var result = _controller.Create(personAccomplishment);

            var viewResult = Assert.IsType<ViewResult>(result);
            var testPerson = Assert.IsType<PersonAccomplishmentViewModel>(viewResult.Model);
            Assert.Equal(personAccomplishment.LastName, testPerson.LastName);
            Assert.Equal(personAccomplishment.State, testPerson.State);
        }

        [Fact]
        public void Create_InvalidModelState_CreatePersonAccomplishmentNeverExecutes()
        {
            _controller.ModelState.AddModelError("FirstName", "Name is required");

            var personAccomplishment = new PersonAccomplishmentViewModel { LastName = "Test LastName", City = "CityTest", State = "Test state", DateOfBirth = new DateTime(1978, 4, 5) };
            _controller.Create(personAccomplishment);
            _mockService.Verify(x => x.Create(It.IsAny<PersonAccomplishmentViewModel>()), Times.Never);
        }

        [Fact]
        public void Create_ModelStateValid_CreatePersonAccomplishmentCalledOnce()
        {
            PersonAccomplishmentViewModel personAccomplishment = null;
            _mockService.Setup(r => r.Create(It.IsAny<PersonAccomplishmentViewModel>()))
                .Callback<PersonAccomplishmentViewModel>(x => personAccomplishment = x);

            var createPerson = new PersonAccomplishmentViewModel
            {
                FirstName = "Test FirstName",
                LastName = "Test LastName",
                City = "CityTest",
                State = "Test state",
                DateOfBirth = new DateTime(1978, 4, 5)
            };

            _controller.Create(createPerson);
            _mockService.Verify(x => x.Create(It.IsAny<PersonAccomplishmentViewModel>()), Times.Once);

            Assert.Equal(personAccomplishment.FirstName, createPerson.FirstName);
            Assert.Equal(personAccomplishment.LastName, createPerson.LastName);
            Assert.Equal(personAccomplishment.State, createPerson.State);
        }

        [Fact]
        public void Create_ActionExecuted_RedirectsToIndexAction()
        {
         
            var createPerson = new PersonAccomplishmentViewModel
            {
                FirstName = "Test FirstName",
                LastName = "Test LastName",
                City = "CityTest",
                State = "Test state",
                DateOfBirth = new DateTime(1978, 4, 5)
            };

            _mockService.Setup(service => service.Create(createPerson)).Returns(true);

            var result = _controller.Create(createPerson);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("List", redirectToActionResult.ActionName);
        }
    }
}
