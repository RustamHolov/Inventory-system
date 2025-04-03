public class DataBase{
    private Dictionary<int, Member> _members { get; set; } = new Dictionary<int, Member>();
    private int _nexID = 1;
    public bool edited = false;
    private string _csvPath = "membersDB.csv";
    private string _titles = "Id,Type,Name,Surname,Birth,Sex,Email,Phone,Subject,Course,StartDate";
    public bool AddMember(Member member){
        if (!_members.ContainsValue(member)){
            _members.Add(_nexID, member);
            _nexID++;
            return true;
        }
        else{
            throw new Exception("Member already exists");
        }
    }
    public void EditMember(int id, Member member){
        if (_members.ContainsKey(id)){
            _members[id] = member;
        }
        else{
            throw new Exception("Member not found");
        }
    }
    public void DeleteMember(int id)
    {
        if (_members.ContainsKey(id)){
            _members.Remove(id);
        }
        else{
            throw new Exception("Member not found");
        }
    }
    public Member GetMember(int id){
        if(_members.ContainsKey(id)){
            return _members[id];
        }
        else{
            throw new Exception("Member not found");
        }
    }
    public Dictionary<int, Member> Members{
        get { return _members; }
    }

    public DataBase(){
        InitializeDataBase();
    }
    public DataBase(string path){
        _csvPath = path;
        InitializeDataBase();
    }
    private void InitializeDataBase(){
        if(File.Exists(_csvPath)){
            LoadFromCSV();
        }else{
            _members = new Dictionary<int, Member>();
            _nexID = 1;
            SaveToCSV();
        }
    }
    public void LoadFromCSV(){
        _members.Clear();
        _nexID = 1;
        try{// implement dictionaries and loops
            using (StreamReader reader = new StreamReader(_csvPath)){
                string[] titles = reader.ReadLine()?.Split(',') ?? throw new Exception("No header found");
                string? line;
                while ((line = reader.ReadLine()) != null){
                    string[] values = line.Split(',');
                    if(titles.Length != values.Length) throw new Exception("Something went wrong with tables");
                    Dictionary<string, string> table = titles.Zip(values, (k, v) => new { Key = k, Value = v })
                   .ToDictionary(x => x.Key, x => x.Value);           //make a Dict by LINQ and lambdas from titles and values
                    if (int.TryParse(values[0], out int id)){
                        Member member = values[1] switch
                        {
                            "Member" => new Member(),
                            "Teacher" => new Teacher(),
                            "Student" => new Student(),
                            _ => throw new Exception($"Unknown type {values[1]}")
                        };
                        member.AssignValues(table);
                        _members.Add(id, member);
                        _nexID = Math.Max(_nexID, id + 1); 
                    }
                }
            }
        }catch (Exception e){
            Console.WriteLine($"Error loading from CSV: {e.Message}");
        }
    }

    public void SaveToCSV(){
        try{
            using (StreamWriter sw = new StreamWriter(_csvPath)){
                sw.WriteLine(_titles);
                foreach (var memberPair in _members){
                    var member = memberPair.Value;
                    string type = member.GetType().Name;
                    string line = $"{memberPair.Key},{type},{member.Name},{member.Surname},{member.Birth},{member.Sex},{member.Email},{member.Phone},";
                    if (member is Teacher teacher){
                        line += $"{teacher.Subject},,";
                    }else if (member is Student student){
                        line += $",{student.Course},{student.StartDate}";
                    }else{
                        line += ",,";
                    }
                    sw.WriteLine(line);
                }
            }
        }catch(Exception e){
            Console.WriteLine("Error saving to CSV: " + e.Message);
        }
    }


}