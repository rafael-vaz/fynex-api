using Fynex.Domain.Entities;
using Fynex.Domain.Security.Tokens;
using Fynex.Domain.Services.LoggedUser;
using Fynex.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Fynex.Infrastructure.Services.LoggedUser;

internal class LoggedUser : ILoggedUser
{
    private readonly FynexDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(FynexDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> Get()
    {
        string token = _tokenProvider.TokenOnRequest();
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return await _dbContext.Users.AsNoTracking().FirstAsync(user => user.UserIdentifier == Guid.Parse(identifier));
    }
}
