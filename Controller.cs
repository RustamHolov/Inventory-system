
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
    private void GetAndSetFields(Dictionary<string, string> fields, string fieldName)
    {
        Console.Clear();
        _view.DisplayForm(fields, fieldName);
        if (fields.ContainsKey(fieldName))
        {
            string? newValue = null;
            try
            {
                newValue = fieldName switch
                {
                    "Name" => _input.GetName(),
                    "Surname" => _input.GetSurname(),
                    "Sex" => _input.GetSex(),
                    "Email" => _input.GetEmail(),
                    "Phone" => _input.GetPhone(),
                    "Subject" => _input.GetSubject(),
                    "Course" => _input.GetCourse(),
                    "Birth" => _input.GetBirth(),
                    "StartDate" => _input.GetStartDate(),
                    _ => throw new Exception($"Warning: Unknown field '{fieldName}'. No input taken.")
                };
            }
            catch { } // Make ESC button work

            if (newValue != null) fields[fieldName] = newValue; // Default to null for safety
        }
        else
        {
            Console.WriteLine($"Error: Field '{fieldName}' not found in fields dictionary.");
        }
        _dataBase.edited = true;
    }
    private void FillForm(Dictionary<string, string> fields)
    {
        _view.DisplayForm(fields);
        foreach (var fieldName in fields.Keys)
        {
            if (string.IsNullOrEmpty(fields[fieldName]))
            {
                GetAndSetFields(fields, fieldName);
            }
        }
        _view.DisplayMessageUnderscore("All fields filled out successfully!");
        _view.DisplayForm(fields);
    }
    private void EditField(Dictionary<string, string> fields)
    {
        _view.DisplayForm(fields, count: true);
        int choise = _input.GetMenuItem(fields.Count, withESC: true);
        string fieldName = fields.Keys.ElementAt(choise - 1);
        GetAndSetFields(fields, fieldName);
        _view.DisplayForm(fields);
    }
    private void SaveMember(Dictionary<string, string> fields, Member member, int id = 0)
    {
        if (fields.Values.All(f => !string.IsNullOrEmpty(f.ToString())))
        {
            member.AssignValues(fields);
            try
            {
                if (id == 0)
                {
                    _dataBase.AddMember(member);
                }
                else
                {
                    _dataBase.EditMember(id, member);
                }
                _view.DisplayForm(fields);
                _view.DisplayMessageUnderscore("Changes saved successfully!");
            }
            catch (Exception e)
            {
                _view.DisplayMessageUnderscore(e.Message);
            }
        }
        else
        {
            _view.DisplayForm(fields);
            _view.DisplayMessageUnderscore("Please fill out all fields before saving.");
        }
    }
    private void AddMember(Member member)
    {
        Dictionary<string, string> fields = member.GetPropertiesAndValues();
        _view.DisplayForm(fields);
        void addMenu()
        {
            _view.DisplayMenu(_view.AddMenu);
            switch (_input.GetMenuItem(_view.AddMenu))
            {
                case 1: FillForm(fields); addMenu(); break;
                case 2: try { EditField(fields); addMenu(); break; } catch (EscException) { Console.Clear(); addMenu(); break;/*back*/}
                case 3: SaveMember(fields, member); addMenu(); break;
                case 9: Console.Clear(); MainMenu(); break;
                case 0: Exit(); break;
            }
        }
        addMenu();
    }
    private void AddRoledMember()
    {
        Console.Clear();
        _view.DisplayMenu(_view.RoleMenu);
        switch (_input.GetMenuItem(_view.RoleMenu))
        {
            case 1: AddMember(new Teacher()); break;
            case 2: AddMember(new Student()); break;
            case 3: AddMember(new Member()); break;
            case 9: Console.Clear(); MainMenu(); break;
            case 0: Exit(); break;
        }

    }

    private void EditMember()
    {
        _view.DisplayMembers(_dataBase.Members);
        try
        {
            Console.WriteLine("[ID of the member you want to edit]");
            int id = _input.GetMenuItem(_dataBase.Members.Keys.ToList(), withESC: true);
            Member member = _dataBase.GetMember(id);
            Dictionary<string, string> fields = member.GetPropertiesAndValues();
            _view.DisplayForm(fields);
            void editMenu()
            {
                _view.DisplayMenu(_view.EditMenu);
                switch (_input.GetMenuItem(_view.EditMenu))
                {
                    case 1: EditField(fields); editMenu(); break;
                    case 2: SaveMember(fields, member, id); editMenu(); break;
                    case 9: Console.Clear(); MainMenu(); break;
                    case 0: Exit(); break;
                }
            }
            editMenu();
        }
        catch (EscException)
        { //back
            Console.Clear();
            MainMenu();
        }
    }

    private void DeleteMember()
    {
        _view.DisplayMembers(_dataBase.Members);

        try
        {
            int id = _input.GetMenuItem(_dataBase.Members.Keys.ToList(), "Enter the ID of the member you want to delete:", withESC: true);
            Console.Clear();
            Console.WriteLine($"Are you sure you want to delete:\n[ID:{id} {_dataBase.GetMember(id)}]");
            bool confirmation = _input.GetConfirmation();
            if (confirmation)
            {
                try
                {
                    _dataBase.DeleteMember(id);
                    _dataBase.edited = true;
                    Console.Clear();
                    Console.WriteLine("Member deleted successfully!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.Clear();
                _view.DisplayMessageUnderscore("Member deletion cancelled.");
            }
        }
        catch (EscException)
        { //back
            Console.Clear();
            MainMenu();
        }
    }
    private void DisplayMembers()
    {
        Console.Clear();
        _view.DisplayMembers(_dataBase.Members);
    }
    private void SaveDataBase()
    {
        Console.Clear();
        if (_dataBase.edited)
        {
            _dataBase.SaveToCSV();
            _dataBase.edited = false;
            Console.WriteLine("Data saved successfully!");
        }
    }
    private void Exit()
    {
        if (_dataBase.edited)
        {
            Console.Write("Do you want to save changes before exiting");
            try
            {
                bool confirmation = _input.GetConfirmation();
                if (confirmation)
                {
                    SaveDataBase();
                }
            }
            catch (EscException) //back
            {
                Console.Clear();
                MainMenu();
            }
        }
        Console.WriteLine("Program finished successfully!");
        Environment.Exit(0);
    }
    private void MainMenu(bool esc = false)
    {
        try{
            _view.DisplayUndercsore();
            _view.DisplayMenu(_view.Menu);
            switch (_input.GetMenuItem(_view.Menu, withESC: esc))
            {
                case 1: AddRoledMember(); break;
                case 2: EditMember(); break;
                case 3: DeleteMember(); MainMenu(); break;
                case 4: DisplayMembers(); MainMenu(esc: true); break;
                case 5: SaveDataBase(); MainMenu(); break;
                case 0: Exit(); break;
            }
        }catch (EscException){
            Console.Clear();
            MainMenu();
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