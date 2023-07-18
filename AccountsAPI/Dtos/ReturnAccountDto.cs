namespace AccountsAPI.Dtos
{
    public class ReturnAccountDto
    {
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
