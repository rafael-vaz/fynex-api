using Shouldly;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Expenses.GetAll;

public class GetAllExpenseTest : FynexClassFixture
{

    private const string METHOD = "api/expenses";
    private readonly string _token;

    public GetAllExpenseTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.User_Team_Member.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: METHOD, token: _token);
        result.StatusCode.ShouldBe(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);
        response.RootElement.GetProperty("expenses").EnumerateArray().ShouldNotBeEmpty();
    }

}
