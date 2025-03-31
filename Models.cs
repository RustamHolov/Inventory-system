public class Member{
    private string _name;
    private string _surName;
    private string _birth;
    private string _sex;
    private string _email;
    private string _phone;
    public string Name{
        get { return _name ; }
        set { _name = value; }
    }
    public string SurName{
        get { return _surName ; }
        set { _surName = value; }
    }
    public string Birth{
        get { return _birth ; }
        set { _birth = value; }
    }
    public string Male{
        get { return _sex ; }
        set { _sex = value; }
    }
    public string Email{
        get { return _email ; }
        set { _email = value; }
    }   
    public string Phone{
        get { return _phone; }
        set { _phone = value; }
    }
    public Member(string name,string surName, string birth, string sex, string email, string phone){
            _name = name;
            _surName = surName;
            _birth = birth;
            _sex = sex;
            _email = email;
            _phone = phone;
    }
    public override string ToString(){
        return $"Name: {Name}\nSurname: {SurName}\nBirth: {Birth}\nSex: {Male}\nEmail: {Email}\nPhone: {Phone}";
    }
}
public class Teacher: Member
{
    private string _subject;
    private List<string> _students;
    public string Subject{
        get { return _subject ; }
        set { _subject = value; }
    }
    public List<string> Students{
        get {return _students; }
    }
    public Teacher(string name, string surName, string birth, string sex, string email, string phone, string subject, List<string> students):base(name,surName,birth,sex,email,phone )
    {
        _subject = subject;
        _students = students;
    }
    public override string ToString()
    {
        return base.ToString() + $"\nSubject: {Subject}\nStudents: {string.Join(", ", Students)}";
    }
}

public class Student : Member
{

    private string _course;
    private DateTime _startDate;
    private Teacher _mentor;
    public string Course
    {
        get { return _course ; }
        set { _course = value; }
    }
    public DateTime StartDate{
        get { return _startDate ; }
    }
    public Teacher Mentor{
        get { return _mentor ; }
        set { _mentor = value; }
    }
    public Student(string name, string surName, string birth, string sex, string email, string phone, string course, DateTime startDate, Teacher mentor): base(name, surName, birth, sex, email, phone)
    {
        _course = course;
        _startDate = startDate;
        _mentor = mentor;
    }

    public override string ToString()
    {
        return base.ToString() + $"\nCourse: {Course}\nStart Date: {StartDate.ToString("yyyy-MM-dd")}\nMentor: {Mentor.Name}";
    }
}
