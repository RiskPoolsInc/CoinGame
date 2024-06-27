namespace App.Core.ViewModels.Users;

public class SimilarEmails {
    public int Count { get; set; }
    public UserInfo[] Users { get; set; }
    public string Email { get; set; }
}