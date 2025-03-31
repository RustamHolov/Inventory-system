
public class DataBase{
    public Dictionary<int, Models.Member> Members { get; set; } = new Dictionary<int, Models.Member>();
    
    public Dictionary<int, Models.Teacher> Teachers { get; set; } = new Dictionary<int, Models.Teacher>();
    public Dictionary<int, Models.Student> Students { get; set; } = new Dictionary<int, Models.Student>();
}