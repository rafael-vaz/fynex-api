using Shouldly;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Users.Profile;

public class GetUserProfileTest : FynexClassFixture
{
    private const string METHOD = "api/users";
    private readonly string _token;
    private readonly string _userName;
    private readonly string _userEmail;

    public GetUserProfileTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.User_Team_Member.GetToken();
        _userName = factory.User_Team_Member.GetName();
        _userEmail = factory.User_Team_Member.GetEmail();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(METHOD, _token);
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("name").GetString().ShouldBe(_userName);
        response.RootElement.GetProperty("email").GetString().ShouldBe(_userEmail);
    }
}
