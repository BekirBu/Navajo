using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.Default;
using TimeKeeper.DAL;
using System.Linq;

namespace TimeKeeper.OAuth
{
    public class TimeUserService : UserServiceBase
    {
        private readonly DAL.Repositories.UnitOfWork unitOfWork;
        public TimeUserService(DAL.Repositories.UnitOfWork unit)
        {
            unitOfWork = unit;
        }

        public override async System.Threading.Tasks.Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            var user = unitOfWork.Employees.Get(x => x.Email == context.UserName && x.Password == context.Password).FirstOrDefault();
            if (user == null)
                context.AuthenticateResult = new AuthenticateResult("Bad username or password");
            else
                context.AuthenticateResult = new AuthenticateResult(context.UserName, context.Password);
        }
    }
}