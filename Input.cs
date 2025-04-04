
using System.Text;
using System.Text.RegularExpressions;

public class Input
{
    #region Regex's
    private const string _nameRegex = @"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$";
    private const string _courseRegex = @"^[\p{L}\p{N}\s\-_.,!@#$%^&*()+=`~\p{S}\p{P}]*$";
    private const string _emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    private const string _phoneRegex = @"^\+?[0-9]{1,3}[-. ]?\(?[0-9]{1,4}?\)?[-. ]?[0-9]{1,4}[-. ]?[0-9]{1,9}$";
    private const string _sexRegex = @"^(?:[Mm]ale|[Ff]emale|[Oo]ther)$";

    #endregion
    public string GetProperInput(bool readKey = true, string prompt = "")
    {
        if (readKey)
        {
            Console.WriteLine("*OR press ESC to go back*");
            Console.WriteLine(prompt);
            return GetKeyReading() ?? throw new EscException();
        }
        else
        {
            Console.WriteLine(prompt);
            string? userInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(userInput))
            {
                return userInput.Trim();
            }
            else
            {
                return string.Empty;
            }
        }
    }

    #region "TryParsing-Methods"

    public bool TryGetMenuItem(List<int> list, out int item, string prompt = "", bool withESC = false)
    {
        
        if (int.TryParse(GetProperInput(withESC, prompt), out int number) && list.Contains(number))
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
        if (DateTime.TryParse(GetProperInput(prompt: $"Enter the {prompt}:"), out DateTime result))
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
        string Name = GetProperInput(prompt: $"Enter the {prompt}:");
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

    public int GetMenuItem(List<int> list, string prompt = "", bool withESC = false)
    {
        if (TryGetMenuItem(list, out int item, string.IsNullOrEmpty(prompt) ? "Select a menu item:" : prompt, withESC))
        {
            Console.Clear();
            return item;
        }
        else
        {
            Console.WriteLine("Invalid number");
            return GetMenuItem(list, prompt, withESC);
        }
    }
    public int GetMenuItem(Dictionary<int, string> menu, string prompt = "", bool withESC = false) => GetMenuItem(menu.Keys.ToList(), prompt, withESC);
    public int GetMenuItem(int range, string prompt = "", bool withESC = false) => GetMenuItem(Enumerable.Range(1, range).ToList(), prompt, withESC);
    public bool GetConfirmation()
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
    #region Properties
    public delegate bool TryParseDateDelegate(out DateTime date, string prompt);
    public delegate bool TryParseStringDelegate(out string value, string prompt, string regex);
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

    public string GetInput(TryParseStringDelegate parser, string prompt, string regex)
    {
        if (parser(out string value, prompt, regex))
        {
            return value;
        }
        else
        {
            Console.WriteLine($"Invalid {prompt}, try again");
            return GetInput(parser, prompt, regex);
        }
    }
    public string GetInput(string prompt, string regex) => GetInput(TryGetTitle, prompt, regex);
    public string GetEmail() => GetInput("email", _emailRegex);
    public string GetPhone() => GetInput("phone", _phoneRegex);
    public string GetCourse() => GetInput("course", _courseRegex);
    public string GetSex() => GetInput("sex", _sexRegex);
    public string GetName() => GetInput("name", _nameRegex);
    public string GetSurname() => GetInput("surname", _nameRegex);
    public string GetSubject() => GetInput("subject", _courseRegex);
    #endregion
    #endregion
    public string? GetKeyReading()
    {
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
                return null; //return null to make ?? syntax work
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
}
