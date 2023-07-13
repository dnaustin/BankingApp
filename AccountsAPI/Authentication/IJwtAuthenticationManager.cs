namespace AccountsAPI.Authentication
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(UserCredential userCredential);
    }
}
