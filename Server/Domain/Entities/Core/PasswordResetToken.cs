namespace DMS.Server.Domain.Entities.Core
{
    public class PasswordResetToken
    {
        public int Id { get; set; }
        public Guid RecordId { get; set; }
        public string ClientCode { get; set; }
        public Guid UserGuid { get; set; }
        public string TokenHash { get; set; }
        public TimeOnly ExpireTime { get; set; }
        public Boolean IsUsed { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UsedDate { get; set; }
    }
}
