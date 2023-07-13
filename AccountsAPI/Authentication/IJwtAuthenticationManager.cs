namespace AccountsAPI.Authentication
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string username, int pinCode);
    }
}
