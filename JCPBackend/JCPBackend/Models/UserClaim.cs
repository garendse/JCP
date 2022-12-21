namespace JCPBackend.Models
{
    public class UserClaim
    {
        public string id { get; set; } = null!;

        public string username { get; set; } = null!;

        public string name { get; set; } = null!;

        public string surname { get; set; } = null!;

        public string? tel_no { get; set; }

        public bool active { get; set; }

        public DateTime password_date { get; set; }

        public DateTime end_date { get; set; }

        public string role { get; set; } = null!;

        public string site_id { get; set;} = null!;
    }
}
