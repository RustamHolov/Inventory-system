using System.Text;

public class View{
    public string InfoInFrame(string info)
    {                             //auto-adjustable frame
        int contentLength = info.Length + 2;        // +2 to include whitespaces both at start and end
        string line = new string('═', contentLength);
        string top = "╔" + line + "╗";
        string middle = "║ " + info + " ║";
        string bottom = "╚" + line + "╝";
        string framed = $"{top}\n{middle}\n{bottom}\n";
        return framed;
    }
    public string InfoInBorders(string info, int length){
        string halfLine = "";
        if (info.Length < length){
            length = length - info.Length;
            halfLine = new string(' ', length/2);
        }
        string middle = "║ "+ halfLine + info + halfLine +" ║\n";
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
    public void DisplayMembers(Dictionary<int, Models.Member> members, string[] flags) { }
    public void DisplayMembers(Dictionary<int, Models.Member> members){
        string header = InfoInFrame("ID          Full Name");
        foreach (var member in members){
            string item = $"ID:{member.Key} Name: {member.Value.Name} {member.Value.SurName}";
            string line = InfoInBorders(item,"ID          Full Name".Length);
            header += line;
        }
        Console.WriteLine(header + InfoInFrame("ID          Full Name"));
        Console.WriteLine("ID          Full Name".Length);
    }
    public void DisplaySingleMember(Dictionary<string, Models.Member> members, string member){}
    public void DisplayEditingMember(Dictionary<string, Models.Member> members,string member){}
    public void DisplayException(Exception ex){}

}