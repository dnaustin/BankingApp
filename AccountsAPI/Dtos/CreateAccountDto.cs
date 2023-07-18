namespace AccountsAPI.Dtos
{
    public class CreateAccountDto
    {
        public decimal Balance { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
