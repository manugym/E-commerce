namespace TuringClothes.Database
{
    public class Mail
    {
        

        public int id { get; set; }
        public TemporaryOrder TemporaryOrder { get; set; }
        public TemporaryOrderDetail TemporaryOrderDetail { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }


    }
}