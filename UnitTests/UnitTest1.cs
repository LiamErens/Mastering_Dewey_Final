using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task CheckPositions_ShouldNavigateToCounter_WhenPositionsAreCorrect()
        {
            // Arrange
            var jsRuntimeMock = new Mock<IJSRuntime>();
            jsRuntimeMock.Setup(r => r.InvokeAsync<List<string>>("blazorCheckPositions"))
                        .ReturnsAsync(new List<string> { /* correct order here */ });

            var navigationManagerMock = new Mock<Microsoft.AspNetCore.Components.NavigationManager>();
            var appStateMock = new Mock<AppState>();

            var component = new TestYourComponent(jsRuntimeMock.Object, navigationManagerMock.Object, appStateMock.Object);

            // Act
            await component.CheckPositions();

            // Assert
            appStateMock.VerifySet(app => app.HasPassedFirstTest = true);
            navigationManagerMock.Verify(nav => nav.NavigateTo("Counter"), Times.Once);
        }

        [TestMethod]
        public async Task CheckPositions_ShouldSetResponseToIncorrect_WhenPositionsAreIncorrect()
        {
            // Arrange
            var jsRuntimeMock = new Mock<IJSRuntime>();
            jsRuntimeMock.Setup(r => r.InvokeAsync<List<string>>("blazorCheckPositions"))
                        .ReturnsAsync(new List<string> { /* incorrect order here */ });

            var navigationManagerMock = new Mock<Microsoft.AspNetCore.Components.NavigationManager>();
            var appStateMock = new Mock<AppState>();

            var component = new TestYourComponent(jsRuntimeMock.Object, navigationManagerMock.Object, appStateMock.Object);

            // Act
            await component.CheckPositions();

            // Assert
            appStateMock.VerifySet(app => app.HasPassedFirstTest = false);
            navigationManagerMock.Verify(nav => nav.NavigateTo(It.IsAny<string>()), Times.Never);
            Assert.AreEqual("incorrect", component.response);
        }
    }
}
