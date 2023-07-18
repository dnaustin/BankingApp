namespace ConsoleFrontEnd.Dtos
{
    public class TransactionDto
    {
        public int? AccountNumberToDebit { get; set; }
        public int? AccountNumberToCredit { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
    }
}
