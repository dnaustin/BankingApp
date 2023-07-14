using System;

namespace AccountsAPI.Exceptions
{
    public class FrozenAccountException : Exception
    {
        public FrozenAccountException() : base("Account is frozen")
        {
        }
    }
}
