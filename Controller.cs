using System;
using System.Reflection;
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
        // { 4, "Display a single member" },
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
    private Dictionary<int, string> RoleMenu = new Dictionary<int, string>(){
        {1, "Teacher"},
        {2, "Student"},
        {3, "Unspecified"},
        {9, "Previous Menu"},
        {0, "Exit"}
    };
    public Dictionary<int, string> EditMenu = new Dictionary<int, string>(){
        {1, "Edit Field"},
        {2, "Save changes"},
        {9, "Previous Menu"},
        {0, "Exit"}
    };
    private Dictionary<string, string> GetProrepties(Type type)
    {
        Dictionary<string, string> fields = new Dictionary<string, string>();
        PropertyInfo[] properties = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        foreach (var property in properties)
        {
            fields.Add(property.Name, string.Empty);
        }
        return fields;
    }

    private void GetAndSetFields(Dictionary<string, string> fields, string fieldName)
        {
            Console.Clear();
            _view.DisplayForm(fields, fieldName);
            switch (fieldName)
            {
                case "Name": fields["Name"] = _input.GetTitle(); break;
                case "Surname": fields["Surname"] = _input.GetTitle(); break;
                case "Birth": fields["Birth"] = _input.GetStartDate().ToString("d"); break;
                case "Sex": fields["Sex"] = _input.GetSex(); break;
                case "Email": fields["Email"] = _input.GetEmail(); break;
                case "Phone": fields["Phone"] = _input.GetPhone(); break;
                case "Subject": fields["Subject"] = _input.GetCourse(); break;
                case "Course": fields["Course"] = _input.GetCourse(); break;
                case "StartDate": fields["StartDate"] = _input.GetStartDate().ToString("d"); break;
            }
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
        Console.WriteLine("Filled out the form successfully!");
        _view.DisplayForm(fields);
    }
    private void EditField(Dictionary<string, string> fields)
    {
        _view.DisplayForm(fields, count: true);
        int choise = _input.GetMenuItem(fields.Count);
        string fieldName = fields.Keys.ElementAt(choise - 1);
        GetAndSetFields(fields, fieldName);
        _view.DisplayForm(fields);
    }
    private void SaveMember(Dictionary<string, string> fields, Type member,int id = 0)
    {
        if (fields.Values.All(f => !string.IsNullOrEmpty(f.ToString())))
        {

            Member _member = member switch
            {
                _ when member == typeof(Member) => new Member(fields["Name"],
                fields["Surname"],
                fields["Birth"],
                fields["Sex"],
                fields["Email"],
                fields["Phone"]),
                _ when member == typeof(Teacher) => new Teacher(fields["Name"],
                fields["Surname"],
                fields["Birth"],
                fields["Sex"],
                fields["Email"],
                fields["Phone"],
                fields["Subject"]),
                _ when member == typeof(Student) => new Student(fields["Name"],
                fields["Surname"],
                fields["Birth"],
                fields["Sex"],
                fields["Email"],
                fields["Phone"],
                fields["Course"],
                fields["StartDate"]),
                _ => throw new Exception("Invalid type")
            };
            try
            {
                if (id == 0){
                    _dataBase.AddMember(_member);
                }else{
                    _dataBase.EditMember(id, _member);
                }
                foreach (var fieldName in fields.Keys)
                {
                    fields[fieldName] = string.Empty;
                }
                _view.DisplayForm(fields);
                Console.WriteLine("Member added successfully!");
                Console.WriteLine("─────────────────────────────");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("─────────────────────────────");
            }
        }
        else
        {
            _view.DisplayForm(fields);
            Console.WriteLine("Please fill out all fields before saving.");
            Console.WriteLine("─────────────────────────────");
        }
    }
    private void AddMember(Type member)
    {
        Dictionary<string, string> fields = GetProrepties(member);
        _view.DisplayForm(fields);
        void addMenu()
        {
            _view.DisplayMenu(AddMenu);
            switch (_input.GetMenuItem(AddMenu))
            {
                case 1: FillForm(fields); addMenu(); break;
                case 2: EditField(fields); addMenu(); break;
                case 3: SaveMember(fields, member); addMenu(); break;
                case 9: Console.Clear(); MainMenu(); break;
                case 0: Environment.Exit(0); break;
            }
        }
        addMenu();
    }
    private void AddRoledMember(){
        Console.Clear();
        _view.DisplayMenu(RoleMenu);
        switch (_input.GetMenuItem(RoleMenu))
        {
            case 1: AddMember(typeof(Teacher)); break;
            case 2: AddMember(typeof(Student)); break;
            case 3: AddMember(typeof(Member)); break;
            case 9: Console.Clear(); MainMenu(); break;
            case 0: Environment.Exit(0); break;
        }

    }
    private void DisplaySingleMember()
    {
        throw new NotImplementedException();
    }

    private void EditMember()
    {
        _view.DisplayMembers(_dataBase.Members);
        Console.WriteLine("[ID of the member you want to edit]");
        int id = _input.GetMenuItem(_dataBase.Members.Keys.ToList());
        Member member = _dataBase.GetMember(id);
        Type memberType = member.GetType();
        Dictionary<string, string> fields = new Dictionary<string, string>();
        foreach (var property in memberType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            fields[property.Name] = property.GetValue(member)?.ToString() ?? string.Empty;
        }
        _view.DisplayForm(fields);
        
        void editMenu(){
            _view.DisplayMenu(EditMenu);
            switch (_input.GetMenuItem(EditMenu))
            {
                case 1: EditField(fields); editMenu(); break;
                case 2: SaveMember(fields, memberType, id); editMenu(); break;
                case 9: Console.Clear(); MainMenu(); break;
                case 0: Environment.Exit(0); break;
            }
        }
        editMenu();
    }

    private void DeleteMember() //TODO Create the menu, input method for confirmation and display the member to be deleted
    {
        _view.DisplayMembers(_dataBase.Members);
        Console.WriteLine("[ID of the member you want to delete]");
        int id = _input.GetMenuItem(_dataBase.Members.Keys.ToList());
        Console.WriteLine($"Are you sure you want to delete member with ID {id}? (y/n)");
        string confirmation = _input.GetProperInput().ToLower();
        if(confirmation.Contains("y")){
            try{
                _dataBase.DeleteMember(id);
                Console.Clear();
                Console.WriteLine("Member deleted successfully!");
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }else if(confirmation.Contains("n")){
            Console.WriteLine("Member deletion cancelled.");
        }else{
            Console.WriteLine("Invalid input. Member deletion cancelled.");
        }
        Console.WriteLine("─────────────────────────────");
    }
    private void DisplayMembers()
    {
        Console.Clear();
        _view.DisplayMembers(_dataBase.Members);
    }

    private void MainMenu()
    {
        Console.WriteLine("─────────────────────────────");
        _view.DisplayMenu(Menu);
        switch (_input.GetMenuItem(Menu))
        {
            case 1: AddRoledMember(); break;
            case 2: EditMember(); break;
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