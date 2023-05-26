using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Configuration.API.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public string Permission { get; set; } = null;
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            HashSet<string> permissions = new();
            permissions = context.HttpContext.User.Claims
                .Where(x => x.Type == "permissions")
                .Select(x => x.Value)
                .ToHashSet();

            if (string.IsNullOrEmpty(Permission))
            {
                context.Result = new ForbidResult();
                return;
            }

            if (!permissions.Contains(Permission))
            {
                context.Result = new ForbidResult();
            }
            return;

        }
    }
}
