using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ONS.WEBPMO.Application.Authentication
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public ClaimRequirementFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
            string token = null;

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                token = authorizationHeader.Substring("Bearer ".Length).Trim();
            }

            if (token == null)
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ClaimRequirementFilter>>();
                logger.LogWarning("Token JWT não encontrado no cabeçalho de autorização.");
                context.Result = new ForbidResult();
                return;
            }

            var claimsList = ExtractClaimsFromToken(token);

            var expClaim = claimsList.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);
            if (expClaim != null && !IsTokenValid(expClaim))
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ClaimRequirementFilter>>();
                logger.LogWarning("Token JWT expirado.");
                context.Result = new ForbidResult();
                return;
            }

            var hasClaim = claimsList.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);

            if (!hasClaim)
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ClaimRequirementFilter>>();

                logger.LogWarning("Token JWT: {Token}", token);
                logger.LogWarning("Claims from JWT Token: {Claims}", claimsList.Select(c => $"{c.Type}: {c.Value}"));
                logger.LogWarning("Unauthorized access attempt by {User} to {Path}", context.HttpContext.User.Identity?.Name, context.HttpContext.Request.Path);

                context.Result = new ForbidResult();
            }
        }

        private List<Claim> ExtractClaimsFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            return jwtToken.Claims.ToList();
        }

        private bool IsTokenValid(Claim expClaim)
        {
            var expTimestamp = long.Parse(expClaim.Value);
            var expirationDate = DateTimeOffset.FromUnixTimeSeconds(expTimestamp).UtcDateTime;

            return expirationDate > DateTime.UtcNow;
        }
    }
}
