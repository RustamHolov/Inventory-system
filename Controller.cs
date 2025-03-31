public class Controller
{
    private readonly View _view;
    private readonly Input _input;
    private readonly DataBase _dataBase;

    public Controller(View view, Input input, DataBase dataBase)
    {
        _view = view;
        _input = input;
        _dataBase = dataBase;
    }
    public Dictionary<int, string> Menu = new Dictionary<int, string>()
    {
        { 1, "Add a member" },
        { 2, "Edit a member" },
        { 3, "Delete a member" },
        { 4, "Display all members" },
        { 5, "Display a single member" },
        { 6, "Exit" }
    };
    public void DisplayMembers()
    {
        _view.DisplayMembers(_dataBase.Members);
    }
    public void AddMember(){
        
    }
    public void MainMenu(){
        _view.DisplayMenu(Menu);
        Console.WriteLine("Select an option:");
        switch(_input.GetMenuItem(Menu)){
            case 1: break;
            case 4: DisplayMembers(); break;
            case 6: break;
        }
    }
    public void MainFlow(){
        while(true){
            MainMenu();
            break;
        }
    }
}