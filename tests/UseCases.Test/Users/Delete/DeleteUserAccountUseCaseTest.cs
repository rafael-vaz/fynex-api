using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using Fynex.Application.UseCases.Users.Delete;
using Fynex.Domain.Entities;
using Shouldly;

namespace UseCases.Test.Users.Delete;

public class DeleteUserAccountUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var act = async () => await useCase.Execute();
        await act.ShouldNotThrowAsync();
    }

    private DeleteUserAccountUseCase CreateUseCase(User user) 
    {
        var repository = UserWriteOnlyRepositoryBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();

        return new DeleteUserAccountUseCase(unitOfWork, repository, loggedUser);
    }
}
