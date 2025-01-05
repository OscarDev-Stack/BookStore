namespace BookStore.Entities
{
    public class AppSettings
    {
        public Jwt Jwt { get; set; } = default!;
    }
    public class Jwt
    {
        public string JWTKey { get; set; } = default!;
        public int LifetimeInSeconds { get; set; }
    }
}
