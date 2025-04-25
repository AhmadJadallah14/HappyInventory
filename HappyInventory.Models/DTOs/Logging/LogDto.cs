namespace HappyInventory.Models.DTOs.Logging
{
    public class LogDto
    {
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
