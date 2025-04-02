namespace HelpWithAll.Presentation.ViewModel;

public class UserViewModel{
    public string Login{get; set;}
    public string Password{get; set;}
    public string Email{get; set;}
    public List<string> Roles{get; set;} = [];
}