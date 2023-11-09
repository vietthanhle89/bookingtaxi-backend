using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace bookingtaxi_backend.Model
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleClaimRequiresAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private string[] _roleTypes = { };

        public RoleClaimRequiresAttribute(string roleType)
        {
            _roleTypes.Append(roleType);
        }

        public RoleClaimRequiresAttribute(string[] roleTypes)
        {
            _roleTypes = roleTypes;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            bool result = false;

            foreach (var roleType in _roleTypes)
            {
                if (context.HttpContext.User.HasClaim(IdentityData.AccountRoleClaimName, roleType))
                {
                    result = true;
                    break;                    
                }
            }

            if (result == false) {
                context.Result = new ForbidResult();
            }

            return Task.CompletedTask;
        }
    }
}
