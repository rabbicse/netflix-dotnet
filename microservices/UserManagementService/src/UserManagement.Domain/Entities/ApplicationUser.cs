namespace UserManagement.Domain.Entities
{
    public class ApplicationUser
    {
        public string FullName { get; set; }
        public bool IsDeletable { get; set; } = true;
        public bool EnableMfa { get; set; } = false; 
        public int IsActive { get; set; } = 1;
        public bool? IsUnLocked { get; set; } = true;
        public bool IsDeleted { get; protected set; } = false;
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public Guid? UserLevelId { get; set; }
        public DateTime? PasswordResetDate { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }                
        public DateTime? LastLoginDate { get; set; }
        public string? PasswordHistory { get; set; }        

        // TODO: Need to think about all other user data

    }
}
