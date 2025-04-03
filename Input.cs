using System.Text;
using System.Text.RegularExpressions;

public class Input
{
    #region Regex's
    private const string _nameRegex = @"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$";
    private const string _courseRegex = @"^[\p{L}\p{N}\s\-_.,!@#$%^&*()+=`~\p{S}\p{P}]*$";
    private const string _emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    private const string _phoneRegex = @"^\+?[0-9]{1,3}[-. ]?\(?[0-9]{1,4}?\)?[-. ]?[0-9]{1,4}[-. ]?[0-9]{1,9}$";
    private const string _sexRegex = @"^(?:Male|Female|Other)$";



    #endregion
    public string GetProperInput(bool readKey = true)
    {
        if (readKey)
        {
            return GetKeyReading("*OR press ESC to go back*") ?? throw new EcsException();
        }
        else
        {
            string? userInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(userInput)){
                return userInput.Trim();
            }else{
                return string.Empty;
            }
        }
    }

    #region "TryParsing-Methods"

    public bool TryGetMenuItem(List<int> list, out int item)
    {
        Console.WriteLine("Select a menu item:");
        if (int.TryParse(GetProperInput(false), out int number) && list.Contains(number))
        {
            item = number;
            return true;
        }
        else
        {
            item = -1;
            return false;
        }
    }
    public bool TryGetConfirmation(out bool confirmation)
    {
        Console.WriteLine(" Yes(y)/No(n) ?: ");
        string input = GetProperInput().ToLower();
        switch (input)
        {
            case "y" or "yes": confirmation = true; return true;
            case "n" or "no": confirmation = false; return true;
            default: confirmation = false; return false;
        }
    }

    public bool TryGetDate(out DateTime startDate, string prompt)
    {
        Console.WriteLine($"Enter the {prompt}:");
        if (DateTime.TryParse(GetProperInput(), out DateTime result))
        {
            startDate = result;
            return true;
        }
        else
        {
            startDate = new DateTime();
            return false;
        }
    }

    public bool TryGetTitle(out string name, string prompt, string regex)
    {
        Console.WriteLine($"Enter the {prompt}:");
        string Name = GetProperInput();
        if (Regex.IsMatch(Name, regex))
        {
            name = Name;
            return true;
        }
        else
        {
            name = "";
            return false;
        }
    }
    #endregion
    #region "Recursive Get-Methods"
    public int GetMenuItem(List<int> list)
    {
        try
        {
            if (TryGetMenuItem(list, out int item))
            {
                return item;
            }
            else
            {
                Console.WriteLine("Invalid number");
                return GetMenuItem(list);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return GetMenuItem(list);
        }
    }
    public int GetMenuItem(Dictionary<int, string> menu) => GetMenuItem(menu.Keys.ToList());
    public int GetMenuItem(int range) => GetMenuItem(Enumerable.Range(1, range).ToList());
    public delegate bool TryParseDateDelegate(out DateTime date, string prompt);
    public string GetDate(string prompt, TryParseDateDelegate parser)
    {
        if (parser(out DateTime startDate, prompt))
        {
            return startDate.ToString();
        }
        else
        {
            Console.WriteLine($"Invalid {prompt} format");
            return GetStartDate();
        }
    }
    public string GetStartDate() => GetDate("start date", TryGetDate);
    public string GetBirth() => GetDate("birth date", TryGetDate);
    public delegate bool TryParseDelegate(out string value, string prompt, string regex);
    public string GetInput(TryParseDelegate parser, string prompt, string regex)
    { 
        if (parser(out string value,prompt, regex))
        {
            return value;
        }
        else
        {
            Console.WriteLine($"Invalid {prompt}, try again");
            return GetInput(parser, prompt, regex);
        }
    }
    public string GetEmail() => GetInput(TryGetTitle,"email",_emailRegex);
    public string GetPhone() => GetInput(TryGetTitle,"phone",_phoneRegex);
    public string GetCourse() => GetInput(TryGetTitle,"course",_courseRegex);
    public string GetSex() => GetInput(TryGetTitle,"sex",_sexRegex);
    public string GetName() => GetInput(TryGetTitle, "name", _nameRegex);
    public string GetSurname() => GetInput(TryGetTitle, "surname", _nameRegex);
    public string GetSubject() => GetInput(TryGetTitle,"subject",_courseRegex);
    public bool GetConfirmation()
    {
        try
        {
            if (TryGetConfirmation(out bool yesNo))
            {
                return yesNo;
            }
            else
            {
                Console.WriteLine("Invalid input");
                return GetConfirmation();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return GetConfirmation();
        }
    }
    #endregion
    public string? GetKeyReading(string prompt)
    {
        Console.WriteLine(prompt);
        StringBuilder input = new StringBuilder();
        ConsoleKeyInfo key;

        while (true)
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return input.ToString().Trim();
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine();
                return null;
            }
            else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input.Remove(input.Length - 1, 1);
                Console.Write("\b \b");
            }
            else if (!char.IsControl(key.KeyChar))
            {
                input.Append(key.KeyChar);
                Console.Write(key.KeyChar);
            }
        }
    }
    public class EcsException : Exception
    {
        public EcsException() : base() { }
        public EcsException(string message) : base(message) { }
    }
}