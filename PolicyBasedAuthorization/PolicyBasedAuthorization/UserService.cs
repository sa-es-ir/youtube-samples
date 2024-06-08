namespace PolicyBasedAuthorization;

public class UserService
{
    public List<string> GetUserClaims(string username)
    {
        return [AuthConstants.WebClaim, AuthConstants.MobileClaim];
    }
}
