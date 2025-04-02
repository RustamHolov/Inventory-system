
public class DataBase{
    private Dictionary<int, Member> _members { get; set; } = new Dictionary<int, Member>();
    private int _nexID = 1;
    public bool edited = false;
    private string _csvPath = "membersDB.csv";
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
        try{
            using (StreamReader reader = new StreamReader(_csvPath)){
                reader.ReadLine(); // Skip header line
                string? line;
                while ((line = reader.ReadLine()) != null){
                    string[] fields = line.Split(',');
                    if(fields.Length == 11){
                        if(int.TryParse(fields[0], out int id)){
                            string type = fields[1];
                            string name = fields[2];
                            string surname = fields[3];
                            string birth = fields[4];
                            string sex = fields[5];
                            string email = fields[6];
                            string phone = fields[7];
                            Member? member = null;
                            switch(type){
                                case "Member": member = new Member(name, surname, birth, sex, email, phone); break;
                                case "Teacher": member = new Teacher(name, surname, birth, sex, email, phone, fields[8]); break;
                                case "Student": member = new Student(name, surname, birth, sex, email, phone, fields[9], fields[10]); break;
                                default: Console.WriteLine($"Unknown member type: {type}"); continue;
                            }
                            _members.Add(id, member);
                            _nexID = Math.Max(_nexID, id + 1); // Update next ID based on the highest ID in the file
                        }
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
                sw.WriteLine("Id,Type,Name,Surname,Birth,Sex,Email,Phone,Subject,Course,StartDate");
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