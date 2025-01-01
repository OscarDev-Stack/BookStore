namespace BookStore.Entities
{
    public class Customer : EntityBase
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string DNI { get; set; } = default!;
        public int Edad {  get; set; }
    }
}
