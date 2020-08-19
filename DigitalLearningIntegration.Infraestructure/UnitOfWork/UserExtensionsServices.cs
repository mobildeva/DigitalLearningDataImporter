using System;
using System.Security.Claims;

namespace DigitalLearningIntegration.Infraestructure.UnitOfWork
{
    public static class UserExtensionsServices
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Name)?.Value;
        }      
    }
}
