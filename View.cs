using System.Text;
using System;
using System.Reflection;

public class View
{
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
    {
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

}