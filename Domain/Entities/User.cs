namespace Domain.Entities;
public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string TwoStepSecret { get; set; }
    public string DateCreated { get; set; }
}