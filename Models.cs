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

    public override string ToString()
    {
        return base.ToString() + $" Course: {Course} Start Date: {StartDate} ";
    }
}
