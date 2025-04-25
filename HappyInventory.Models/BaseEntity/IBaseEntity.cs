namespace HappyInventory.Models.BaseEntity
{
    public interface IBaseEntity
    {
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string UpdatedBy { get; set; }
        DateTime? UpdateOn { get; set; }
    }
}
