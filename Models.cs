
using System.Reflection;
public class Member
{
    private string _name;
    private string _surName;
    private string _birth;
    private string _sex;
    private string _email;
    private string _phone;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public string Surname
    {
        get { return _surName; }
        set { _surName = value; }
    }
    public string Birth
    {
        get { return _birth; }
        set { _birth = value; }
    }
    public string Sex
    {
        get { return _sex; }
        set { _sex = value; }
    }
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }
    public string Phone
    {
        get { return _phone; }
        set { _phone = value; }
    }
    public Member(string name, string surName, string birth, string sex, string email, string phone)
    {
        _name = name;
        _surName = surName;
        _birth = birth;
        _sex = sex;
        _email = email;
        _phone = phone;
    }
    public Member() : this("", "", "", "", "", "") { } //default constructor
    public Dictionary<string, string> GetPropertiesAndValues()
    {
        Dictionary<string, string> fields = new Dictionary<string, string>();
        PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        foreach (var property in properties)
        {
            fields.Add(property.Name, property.GetValue(this)?.ToString() ?? string.Empty);
        }
        return fields;
    }
    public void AssignValues(Dictionary<string, string> fields)
    {
        foreach (var kvp in fields)
        {
            string propertyName = kvp.Key;
            string propertyValue = kvp.Value;
            PropertyInfo? property = GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
            if (property != null)
            {
                property.SetValue(this, propertyValue);
            }
            else
            {
                throw new Exception($"Property {propertyName} not found on type {GetType()}");
            }
        }
    }
    public static int FieldsToFill() => typeof(Member).GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Length;
    public override string ToString()
    {
        return $"Role: {GetType().Name} Name: {Name} Surname: {Surname} Birth: {Birth} Sex: {Sex} Email: {Email} Phone: {Phone}";
    }
}
public class Teacher : Member
{
    private string _subject;

    public string Subject
    {
        get { return _subject; }
        set { _subject = value; }
    }

    public Teacher(string name, string surName, string birth, string sex, string email, string phone, string subject) : base(name, surName, birth, sex, email, phone)
    {
        _subject = subject;
    }
    public Teacher() : this("", "", "", "", "", "", "") { } //default constructor
    public override string ToString()
    {
        return base.ToString() + $" Subject: {Subject} ";
    }
}

public class Student : Member
{
    private string _course;
    private string _startDate;
    public string Course
    {
        get { return _course; }
        set { _course = value; }
    }
    public string StartDate
    {
        get { return _startDate; }
        set { _startDate = value; }
    }
    public Student(string name, string surName, string birth, string sex, string email, string phone, string course, string startDate) : base(name, surName, birth, sex, email, phone)
    {
        _course = course;
        _startDate = startDate;
    }
    public Student() : this("", "", "", "", "", "", "", "") { } //default constructor
    public override string ToString()
    {
        return base.ToString() + $" Course: {Course} Start Date: {StartDate} ";
    }
}
