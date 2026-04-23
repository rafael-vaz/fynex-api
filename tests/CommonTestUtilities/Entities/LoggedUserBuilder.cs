using Fynex.Domain.Entities;
using Fynex.Domain.Services.LoggedUser;
using Moq;

namespace CommonTestUtilities.Entities;

internal class LoggedUserBuilder
{
    public static ILoggedUser Build(User user)
    {
      var mock = new Mock<ILoggedUser>();
      mock.Setup(loggedUser => loggedUser.Get()).ReturnsAsync(user);
      return mock.Object;
    }
}
