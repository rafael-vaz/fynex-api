using Shouldly;
using System.Net;

namespace WebApi.Test.Users.Delete;

public class DeleteUserTest : FynexClassFixture
{
    private const string METHOD = "api/users";
    private readonly string _token;

    public DeleteUserTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.User_Team_Member.GetToken();
    }

    public async Task Success()
    {
        var result = await DoDelete(METHOD, _token);
        result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
}
