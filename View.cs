public class View
{
    public Dictionary<int, string> Menu = new Dictionary<int, string>()
    {
        { 1, "Add a member" },
        { 2, "Edit a member" },
        { 3, "Delete a member" },
        { 4, "Display all members" },
        { 5, "Save Data"},
        { 0, "Exit" }
    };
    public Dictionary<int, string> AddMenu = new Dictionary<int, string>(){
        {1, "Start filling out the form"},
        {2, "Edit Field"},
        {3, "Save"},
        {9, "Previous Menu"},
        {0, "Exit"}
    };
    public Dictionary<int, string> RoleMenu = new Dictionary<int, string>(){
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
    public string InfoInFrame(string info)
    {                             //auto-adjustable one line frame
        int contentLength = info.Length + 2;        // +2 to include whitespaces both at start and end
        string line = new string('═', contentLength);
        string top = "╔" + line + "╗";
        string middle = "║ " + info + " ║";
        string bottom = "╚" + line + "╝";
        string framed = $"{top}\n{middle}\n{bottom}\n";
        return framed;
    }
    public string InfoInBorders(string info)
    {
        string middle = "║ " + info + "\n";
        return middle;
    }

    public void DisplayMenu(Dictionary<int, string> menu)
    {
        foreach (var items in menu)
        {
            Console.WriteLine($"{items.Key}. {items.Value}");
        }
        Console.WriteLine("─────────────────────────────");
    }
    public void DisplayMembers(Dictionary<int, Member> members, string[] flags) { } //TODO implement sorted display
    public void DisplayMembers(Dictionary<int, Member> members)
    {
        Console.Clear();
        Console.WriteLine(InfoInFrame("Members"));
        foreach (var member in members)
        {
            Console.Write($"ID: {member.Key} {InfoInBorders(member.Value.ToString())}");
        }
        Console.WriteLine("─────────────────────────────");
    }
    public void DisplaySingleMember(Member member)
    {
        Console.WriteLine(InfoInFrame("Member"));
        Console.WriteLine(InfoInBorders(member.ToString()));
        Console.WriteLine("─────────────────────────────");

    }
    public void DisplayForm(Dictionary<string, string> fields, string fieldName = "",bool count = false)
    { //Displays object with empty or filled fields, show if field is being edited or give fields numbers. Takes dictionary of properties and their values of the object
        bool editing = fieldName != string.Empty;
        Console.Clear();
        int i = 1;
        foreach (var field in fields)
        {
            bool chosenField = field.Key == fieldName;
            Console.WriteLine($"{(count ? i + "." : "")} {(editing && chosenField ? "    > " + field.Key : field.Key)}: {(field.Value == string.Empty ? "" : field.Value)}");
            i++;
        }
        Console.WriteLine("─────────────────────────────");
    }
    public void DisplayException(Exception ex) { }

    
    public void DisplayUndercsore()
    {
        Console.WriteLine("─────────────────────────────");
    }
    public void DisplayMessageUnderscore(string message)
    {
        Console.WriteLine(message);
        DisplayUndercsore();
    }
    
}