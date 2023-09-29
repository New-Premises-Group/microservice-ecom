
namespace IW.Exceptions.ReadRoleError
{
    public class ReadRoleErrorFactory : 
        IPayloadErrorFactory<RoleNotFoundException, RoleNotFoundError>
    {
        public RoleNotFoundError CreateErrorFrom(RoleNotFoundException ex)
        {
            return new RoleNotFoundError(ex.Message);
        }
    }
}
