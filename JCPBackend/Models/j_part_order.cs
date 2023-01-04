namespace JCPBackend.Models
{
    public class j_part_order
    {
        public string id { get; set; } = null!;
        public string quote_id { get; set; } = null!;
        public string supplier_id { get; set; } = null!;
        public DateTime sent_datetime { get; set; }
        
    }
}
