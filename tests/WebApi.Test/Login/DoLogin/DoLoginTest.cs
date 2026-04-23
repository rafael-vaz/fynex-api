using CommonTestUtilities.Requests;
using Fynex.Communication.Requests;
using Fynex.Exception;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Login.DoLogin;

public class DoLoginTest : FynexClassFixture
{
    private const string METHOD = "api/login";
    private readonly HttpClient _httpClient;
    private readonly string _email;
    private readonly string _name;
    private readonly string _password;


    public DoLoginTest(CustomWebApplicationFactory factory) : base(factory)
    {       
        _email = factory.User_Team_Member.GetEmail();
        _name = factory.User_Team_Member.GetName();
        _password = factory.User_Team_Member.GetPassword();
    }

    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginJson
        {
            Email = _email,
            Password = _password
        };

        var response = await DoPost(requestUri: METHOD, request: request);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().ShouldBe(_name);
        responseData.RootElement.GetProperty("token").GetString().ShouldNotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Login_Invalid(string culture)
    {
        var request = new RequestLoginJsonBuilder();
        var response = await DoPost(requestUri: METHOD, request: request, culture: culture);
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

        var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);
        var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray().Select(e => e.GetString()).ToList(); ;
        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", new System.Globalization.CultureInfo(culture));
        
        errors.Count.ShouldBe(1);
        errors[0].ShouldBe(expectedMessage);
    }
}
