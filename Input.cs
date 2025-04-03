using System.Text;
using System.Text.RegularExpressions;

public class Input
{
    public string GetProperInput(bool readKey = true)
    {
        if(readKey){
            return GetKeyReading("*Or press ESC to go back*") ?? throw new ArgumentException();
        }else{
            string? userInput = Console.ReadLine();
            return userInput switch
            {
                _ when userInput == null || userInput.Trim() == "" => throw new Exception(),
                _ => userInput.Trim()
            };
        }
    }

    #region "TryParsing-Methods"
    public bool TryGetMenuItem(Dictionary<int, string> menu, out int item)
    {
        Console.WriteLine("Select a menu item:");
        if (int.TryParse(GetProperInput(false), out int number) && menu.Keys.Any(n => n == number))
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
    public bool TryGetMenuItem(int range, out int item)
    {
        Console.WriteLine("Select an item:");
        if (int.TryParse(GetProperInput(false), out int number) && number > 0 && number <= range)
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
    public bool TryGetMenuItem(List<int> list, out int item)
    {
        Console.WriteLine("Select an item:");
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

    public bool TryGetStartDate(out DateTime startDate)
    {
        Console.WriteLine("Enter the date:");
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
    public bool TryGetTitle(out string name)
    {
        Console.WriteLine("Enter the title:");
        string nameRegex = @"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$";
        string Name = GetProperInput();
        if (Regex.IsMatch(Name, nameRegex))
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
    public bool TryGetCourse(out string course)
    {
        Console.WriteLine("Enter your subject:");
        string courseRegex = @"^[\p{L}\p{N}\s\-_.,!@#$%^&*()+=`~\p{S}\p{P}]*$";
        string Course = GetProperInput();
        if (Regex.IsMatch(Course, courseRegex))
        {
            course = Course;
            return true;
        }
        else
        {
            course = "";
            throw new Exception("Invalid format, try again");
        }
    }
    public bool TryGetEmail(out string email)
    {
        Console.WriteLine("Enter your email:");
        string emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        string Email = GetProperInput();
        if (Regex.IsMatch(Email, emailRegex))
        {
            email = Email;
            return true;
        }
        else
        {
            email = "";
            return false;
        }
    }
    public bool TryGetPhone(out string phone)
    {
        Console.WriteLine("Enter your phone:");
        string phoneRegex = @"^\+?[0-9]{1,3}[-. ]?\(?[0-9]{1,4}?\)?[-. ]?[0-9]{1,4}[-. ]?[0-9]{1,9}$"; // international phone number format
        string Phone = GetProperInput();
        if (Regex.IsMatch(Phone, phoneRegex))
        {
            phone = Phone;
            return true;
        }
        else
        {
            phone = "";
            return false;
        }
    }
    public bool TryGetSex(out string sex)
    {
        Console.WriteLine("Enter your sex in format (Male|Female|Other):");
        string maleRegex = @"^(?:Male|Female|Other)$";
        string Male = GetProperInput();
        if (!Regex.IsMatch(Male.ToLower(), maleRegex))
        {
            sex = Male;
            return true;
        }
        else
        {
            sex = "";
            return false;
        }
    }
    public bool TryGetAge(out int age)
    {
        Console.WriteLine("Enter your age:");
        if (int.TryParse(GetProperInput(), out int result) && result > 0 && result < 120)
        {
            age = result;
            return true;
        }
        else
        {
            age = -1;
            return false;
        }
    }
    

    #endregion
    #region "Recursive Get-Methods"
    public int GetMenuItem(Dictionary<int, string> menu)
    {
        try{
            if (TryGetMenuItem(menu, out int item))
            {
                return item;
            }
            else
            {
                Console.WriteLine("Invalid number");
                return GetMenuItem(menu);
            }
        }catch (Exception e){
            Console.WriteLine(e.Message);
            return GetMenuItem(menu);
        }
    }
    public int GetMenuItem(int range)
    {
        try
        {
            if (TryGetMenuItem(range, out int item))
            {
                return item;
            }
            else
            {
                Console.WriteLine("Invalid number");
                return GetMenuItem(range);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return GetMenuItem(range);
        }
    }
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
    public DateTime GetStartDate()
    {
        if(TryGetStartDate(out DateTime startDate))
        {
            return startDate;
        }
        else
        {
            Console.WriteLine("Invalid date format");
            return GetStartDate();
        }
    }
    public string GetTitle()
    {
        if(TryGetTitle(out string name))
        {
            return name;
        }
        else
        {
            Console.WriteLine("Invalid format, try again");
            return GetTitle();
        }
    }
    public string GetEmail()
    {
        if(TryGetEmail(out string email))
        {
            return email;
        }
        else
        {
            Console.WriteLine("Invalid Email");
            return GetEmail();
        }
    }
    public string GetPhone()
    {
        if(TryGetPhone(out string phone))
        {
            return phone;
        }
        else
        {
            Console.WriteLine("Invalid Phone");
            return GetPhone();
        }
    }
    public int GetBirth()
    {
        if(TryGetAge(out int age))
        {
            return age;
        }
        else
        {
            Console.WriteLine("Invalid Date");
            return GetBirth();
        }
    }
    public string GetCourse()
    {
        if(TryGetCourse(out string course))
        {
            return course;
        }
        else
        {
            Console.WriteLine("Invalid Course");
            return GetCourse();
        }
    }
    public string GetSex()
    {
        if(TryGetSex(out string sex))
        {
            return sex;
        }
        else
        {
            Console.WriteLine("Invalid sex");
            return GetSex();
        }
    }
    public bool GetConfirmation()
    {
        try{
            if (TryGetConfirmation(out bool yesNo))
            {
                return yesNo;
            }
            else
            {
                Console.WriteLine("Invalid input");
                return GetConfirmation();
            }
        }catch (Exception e){
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