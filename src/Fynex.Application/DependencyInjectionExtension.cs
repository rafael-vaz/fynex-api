using Fynex.Application.AutoMapper;
using Fynex.Application.UseCases.Expenses.Delete;
using Fynex.Application.UseCases.Expenses.GetAll;
using Fynex.Application.UseCases.Expenses.GetById;
using Fynex.Application.UseCases.Expenses.Register;
using Fynex.Application.UseCases.Expenses.Reports.Excel;
using Fynex.Application.UseCases.Expenses.Reports.Pdf;
using Fynex.Application.UseCases.Expenses.Update;
using Fynex.Application.UseCases.Login;
using Fynex.Application.UseCases.Users.ChangePassword;
using Fynex.Application.UseCases.Users.Delete;
using Fynex.Application.UseCases.Users.Profile;
using Fynex.Application.UseCases.Users.Register;
using Fynex.Application.UseCases.Users.Update;
using Microsoft.Extensions.DependencyInjection;

namespace Fynex.Application;

public static class DependencyInjectionExtension
{
    public static void AddAplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(_ => { }, typeof(AutoMapping).Assembly);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        // login
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();

        // change password
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();

        // users
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IDeleteUserAccountUseCase, DeleteUserAccountUseCase>();
        

        // expenses
        services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
        services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
        services.AddScoped<IGetAllExpenseUseCase, GetAllExpenseUseCase>();
        services.AddScoped<IGetExpenseByIdUseCase, GetExpenseByIdUseCase>();
        services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
        services.AddScoped<IGenerateExpensesReportExcelUseCase, GenerateExpensesReportExcelUseCase>();
        services.AddScoped<IGenerateExpensesReportPdfUseCase, GenerateExpensesReportPdfUseCase>();      
    }
}