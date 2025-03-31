namespace Inventory_system;

class Program
{
    static void Main(string[] args)
    {
        DataBase db = new DataBase();
        Controller controller = new Controller(new View(), new Input(), db);
        db.Members.Add(1,new Models.Member("Jane", "Doe",20,"female","l@l.com","123456789"));
        db.Members.Add(2, new Models.Member("Jane", "Doe", 20, "female", "l@l.com", "123456789"));
        controller.MainFlow();
    }
}
