
public class DataBase{
    private Dictionary<int, Member> _members { get; set; } = new Dictionary<int, Member>();
    private int _nexID = 1;
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
}