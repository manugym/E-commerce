namespace TuringClothes.Dtos
{
    public class CheckTransactionRequest
    {
        public string Hash { get; set; }
        public long TemporaryOrderId { get; set; }
    }
}
