using Fynex.Exception;
using Shouldly;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Expenses.Delete;

public class DeleteExpenseTest : FynexClassFixture
{

    private const string METHOD = "api/expenses";
    private readonly string _token;
    private readonly long _expenseId;

    public DeleteExpenseTest(CustomWebApplicationFactory factory): base(factory)
    {
        _token = factory.User_Team_Member.GetToken();
        _expenseId = factory.Expense_TeamMember.GetId();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoDelete(requestUri: $"{METHOD}/{_expenseId}", token: _token);
        result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        result = await DoGet(requestUri: $"{METHOD}/{_expenseId}", token: _token);
        result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Expense_Not_Found(string culture)
    {
        var result = await DoDelete(requestUri: $"{METHOD}/100", token: _token, culture: culture);
        result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);
        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray().Select(e => e.GetString()).ToList();
        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EXPENSE_NOT_FOUND", new System.Globalization.CultureInfo(culture));
        errors.Count.ShouldBe(1);
        errors[0].ShouldBe(expectedMessage);
    }
}
