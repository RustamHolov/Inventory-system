namespace Inventory_system;

class Program
{
    static void Main(string[] args)
    {
        DataBase db = new DataBase();
        Controller controller = new Controller(new View(), new Input(), db);
        db.AddMember(new Member("Jane", "Doe","12.09.1996","female","l@l.com","123456789"));
        db.AddMember(new Member("Jan", "Do", "12.09.1996", "female", "l@l.com", "123456789"));
        db.AddMember(new Member("J", "D", "12.09.1996", "female", "l@l.com", "123456789"));
        db.AddMember(new Member("Ja", "D", "12.09.1996", "female", "l@l.com", "123456789"));
        controller.MainFlow();
    }
}
