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
    
    private Dictionary<string, string> GetProrepties(Type type)
    {
        Dictionary<string, string> fields = new Dictionary<string, string>();
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
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
        _view.DisplayMessageUnderscore("All fields filled out successfully!");
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
                _view.DisplayMessageUnderscore("Member saved successfully!");
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
    private void AddMember(Type member)
    {
        Dictionary<string, string> fields = GetProrepties(member);
        _view.DisplayForm(fields);
        void addMenu()
        {
            _view.DisplayMenu(_view.AddMenu);
            switch (_input.GetMenuItem(_view.AddMenu))
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
        _view.DisplayMenu(_view.RoleMenu);
        switch (_input.GetMenuItem(_view.RoleMenu))
        {
            case 1: AddMember(typeof(Teacher)); break;
            case 2: AddMember(typeof(Student)); break;
            case 3: AddMember(typeof(Member)); break;
            case 9: Console.Clear(); MainMenu(); break;
            case 0: Environment.Exit(0); break;
        }

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
            _view.DisplayMenu(_view.EditMenu);
            switch (_input.GetMenuItem(_view.EditMenu))
            {
                case 1: EditField(fields); editMenu(); break;
                case 2: SaveMember(fields, memberType, id); editMenu(); break;
                case 9: Console.Clear(); MainMenu(); break;
                case 0: Environment.Exit(0); break;
            }
        }
        editMenu();
    }

    private void DeleteMember()
    {
        _view.DisplayMembers(_dataBase.Members);
        Console.WriteLine("[ID of the member you want to delete]");
        int id = _input.GetMenuItem(_dataBase.Members.Keys.ToList());
        Console.Clear();
        Console.WriteLine($"Are you sure you want to delete:\n[ID:{id} {_dataBase.GetMember(id)}]");
        bool confirmation = _input.GetYesNo();
        if (confirmation)
        {
            try{
                _dataBase.DeleteMember(id);
                Console.Clear();
                Console.WriteLine("Member deleted successfully!");
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }else {
             Console.Clear();
            _view.DisplayMessageUnderscore("Member deletion cancelled.");
        }
    }
    private void DisplayMembers()
    {
        Console.Clear();
        _view.DisplayMembers(_dataBase.Members);
    }

    private void MainMenu()
    {
        _view.DisplayUndercsore();
        _view.DisplayMenu(_view.Menu);
        switch (_input.GetMenuItem(_view.Menu))
        {
            case 1: AddRoledMember(); break;
            case 2: EditMember(); break;
            case 3: DeleteMember(); MainMenu(); break;
            case 4: DisplayMembers(); MainMenu(); break;
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