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
    private Dictionary<int, string> Menu = new Dictionary<int, string>()
    {
        { 1, "Add a member" },
        { 2, "Edit a member" },
        { 3, "Delete a member" },
        { 4, "Display a single member" },
        { 5, "Display all members" },
        { 0, "Exit" }
    };
    private Dictionary<int, string> AddMenu = new Dictionary<int, string>(){
        {1, "Start filling out the form"},
        {2, "Edit Field"},
        {3, "Save"},
        {9, "Previous Menu"},
        {0, "Exit"}
    };
    private void DisplayMembers()
    {
        Console.Clear();
        _view.DisplayMembers(_dataBase.Members);
    }
    private void AddMember()
    {
        Dictionary<string, string> fields = new Dictionary<string, string>(){
            {"Name", string.Empty},
            {"Surname", string.Empty},
            {"Birth", string.Empty},
            {"Sex", string.Empty},
            {"Email", string.Empty},
            {"Phone", string.Empty},
        };
        
        
        void DisplayForm(){
            Console.Clear();
            int i = 1;
            foreach(var field in fields){
                Console.WriteLine($"{i}.{field.Key}: {(field.Value == string.Empty ? "" : field.Value)}");
                i++;
            }
            Console.WriteLine("─────────────────────────────");
        }
        void GetAndSetFields(string fieldName){
            Console.Clear();
            DisplayForm();
            switch(fieldName){
                case "Name": fields["Name"] = _input.GetName(); break;
                case "Surname": fields["Surname"] = _input.GetName(); break;
                case "Birth" : fields["Birth"] = _input.GetStartDate().ToString(); break;
                case "Sex" : fields["Sex"]  = _input.GetSex(); break;
                case "Email" : fields["Email"] = _input.GetEmail(); break;
                case "Phone" : fields["Phone"] = _input.GetPhone(); break;
            }
        }
        void FillForm()
        {
            DisplayForm();
            foreach(var fieldName in fields.Keys){
                if (string.IsNullOrEmpty(fields[fieldName])){
                    GetAndSetFields(fieldName);
                }
            }
            Console.WriteLine("Filled out the form successfully!");
        }
        void EditField()
        {
            DisplayForm();
            int choise = _input.GetMenuItem(fields.Count);
            string fieldName = fields.Keys.ElementAt(choise - 1);
            GetAndSetFields(fieldName);
        }

        void SaveMember()
        {
            if (fields.Values.All(f => !string.IsNullOrEmpty(f.ToString()))){
                Member member = new Member(
                    fields["Name"],
                    fields["Surname"],
                    fields["Birth"],
                    fields["Sex"],
                    fields["Email"],
                    fields["Phone"]
                );
                try{
                    _dataBase.AddMember(member);
                    foreach (var fieldName in fields.Keys){
                        fields[fieldName] = string.Empty;
                    }
                    DisplayForm();
                    Console.WriteLine("Member added successfully!");
                    Console.WriteLine("─────────────────────────────");
                }catch (Exception e){
                    Console.WriteLine(e.Message);
                    Console.WriteLine("─────────────────────────────");
                }
            }else{
                DisplayForm();
                Console.WriteLine("Please fill out all fields before saving.");
                Console.WriteLine("─────────────────────────────");
            }
        }
        DisplayForm();
        void addMenu()
        {
            _view.DisplayMenu(AddMenu);
            switch (_input.GetMenuItem(AddMenu))
            {
                case 1: FillForm(); addMenu(); break;
                case 2: EditField(); addMenu(); break;
                case 3: SaveMember(); addMenu(); break;
                case 9: Console.Clear(); MainMenu(); break;
                case 0: Environment.Exit(0); break;
            }
        } 
        addMenu();
    }
    private void DisplaySingleMember()
    {
        throw new NotImplementedException();
    }

    private void EditMember()
    {
        throw new NotImplementedException();
    }

    private void DeleteMember()
    {
        throw new NotImplementedException();
    }

    private void MainMenu()
    {
        Console.WriteLine("─────────────────────────────");
        _view.DisplayMenu(Menu);
        switch (_input.GetMenuItem(Menu))
        {
            case 1: AddMember(); break;
            case 2: EditMember(); MainMenu(); break;
            case 3: DeleteMember(); MainMenu(); break;
            case 4: DisplaySingleMember(); MainMenu(); break;
            case 5: DisplayMembers(); MainMenu(); break;
            case 0: Environment.Exit(0); break;
        }
    }

    

    public void MainFlow()
    {
        while (true)
        {
            MainMenu();
            break;
        }
    }
}