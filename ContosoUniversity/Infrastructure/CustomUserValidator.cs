using Microsoft.AspNetCore.Identity;

namespace ContosoUniversity.Infrastructure
{
    public class CustomUserValidator : IUserValidator<IdentityUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager,
            IdentityUser user)
        {
            if (!string.IsNullOrEmpty(user.Email) && user.Email.ToLower().EndsWith("@contoso.com"))
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "EmailDomainError",
                    Description = "Only contoso.com email addresses are allowed"
                }));
            }
        }
    }
}
