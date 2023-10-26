namespace EmailServiceModel;

public class ModelBase
{
    public string Id { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
}