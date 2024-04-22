namespace SpaceLoops.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailId { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid CityId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsActive { get; set; }
        public string? Password { get; set; }
        public string? ResetCode { get; set; }

        public Guid ImageId { get; set; }

        public string? ImageName { get; set; }
        public string? ImageData { get; set; }
        
    }
}
