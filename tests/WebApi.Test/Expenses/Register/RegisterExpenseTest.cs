using CommonTestUtilities.Requests;
using Fynex.Exception;
using Shouldly;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Expenses.Register;

public class RegisterExpenseTest : FynexClassFixture
{
    private const string METHOD = "api/expenses";    
    private readonly string _token;

    public RegisterExpenseTest(CustomWebApplicationFactory factory) : base(factory)
    {        
        _token = factory.User_Team_Member.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestExpenseJsonBuilder.Build();         
        var result = await DoPost(requestUri:METHOD, request:request, token: _token);          
        result.StatusCode.ShouldBe(HttpStatusCode.Created);
       
        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("title").GetString().ShouldBe(request.Title);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Title_Empty(string culture)
    {
        var request = RequestExpenseJsonBuilder.Build();
        request.Title = string.Empty;
      
        var result = await DoPost(requestUri:METHOD, request:request, token: _token, culture:culture);
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);
        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray().Select(e => e.GetString()).ToList();
        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("TITLE_REQUIRED", new CultureInfo(culture));

        errors.Count.ShouldBe(1);
        errors[0].ShouldBe(expectedMessage);
    }
}