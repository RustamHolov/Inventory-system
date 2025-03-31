using System.Text.RegularExpressions;

public class Input
{
    public string GetProperInput()
    {
        string? userInput = Console.ReadLine();
        return userInput switch
        {
            _ when userInput == null || userInput.Trim() == "" => throw new Exception("Empty input"),
            _ => userInput.Trim()
        };
    }
    #region "TryParsing-Methods"
    public bool TryGetMenuItem(Dictionary<int, string> menu, out int item)
    {
        Console.WriteLine("Select a menu item:");
        if (int.TryParse(GetProperInput(), out int number) && menu.Keys.Any(n => n == number))
        {
            item = number;
            return true;
        }
        else
        {
            item = -1;
            throw new Exception("Invalid number");
        }
    }
    public bool TryGetMenuItem(int range, out int item)
    {
        Console.WriteLine("Select an item:");
        if (int.TryParse(GetProperInput(), out int number) && number > 0 && number <= range)
        {
            item = number;
            return true;
        }
        else
        {
            item = -1;
            throw new Exception("Invalid number");
        }
    }

    public bool TryGetStartDate(out DateTime startDate)
    {
        Console.WriteLine("Enter your birth date:");
        if (DateTime.TryParse(GetProperInput(), out DateTime result))
        {
            startDate = result;
            return true;
        }
        else
        {
            startDate = new DateTime();
            throw new Exception("Invalid date format");
        }
    }
    public bool TryGetName(out string name)
    {
        Console.WriteLine("Enter your name or surname:");
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
            throw new Exception("Invalid format, try again");
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
            throw new Exception("Invalid format, try again");
        }
    }
    public bool TryGetSex(out string male)
    {
        Console.WriteLine("Enter your sex in format (Male|Female|Other):");
        string maleRegex = @"^(?:male|female|other)$";
        string Male = GetProperInput();
        if (!Regex.IsMatch(Male, maleRegex))
        {
            male = Male;
            return true;
        }
        else
        {
            male = "";
            throw new Exception("Invalid format, try again");
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
            throw new Exception("Invalid format, try again");
        }
    }

    public bool TryGetCourse(out string course)
    {
        Console.WriteLine("Enter your course:");
        string courseRegex = @"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$";
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
    #endregion
    #region "Recursed Get-Methods"
    public int GetMenuItem(Dictionary<int, string> menu)
    {
        try
        {
            TryGetMenuItem(menu, out int item);
            return item;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return GetMenuItem(menu);
        }
    }
    public int GetMenuItem(int range)
    {
        try
        {
            TryGetMenuItem(range, out int item);
            return item;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return GetMenuItem(range);
        }
    }
    public DateTime GetStartDate()
    {
        try
        {
            TryGetStartDate(out DateTime startDate);
            return startDate;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return GetStartDate();
        }
    }
    public string GetName()
    {
        try
        {
            TryGetName(out string name);
            return name;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return GetName();
        }
    }
    public string GetEmail()
    {
        try
        {
            TryGetEmail(out string email);
            return email;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return GetEmail();
        }
    }
    public string GetPhone()
    {
        try
        {
            TryGetPhone(out string phone);
            return phone;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return GetPhone();
        }
    }
    public int GetAge()
    {
        try
        {
            TryGetAge(out int age);
            return age;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return GetAge();
        }
    }
    public string GetCourse()
    {
        try
        {
            TryGetCourse(out string course);
            return course;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return GetCourse();
        }
    }
    public string GetSex()
    {
        try
        {
            TryGetSex(out string male);
            return male;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return GetSex();
        }
    }
    #endregion
}