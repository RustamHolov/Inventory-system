namespace Inventory_system;
// TODO
// look for hard-coded places
class Program
{
    static void Main(string[] args)
    {
        View view = new View();
        Input input = new Input();
        DataBase db = new DataBase();
        Controller controller = new Controller(view, input, db);
        // test group of members
        // db.AddMember(new Member("Jane", "Doe", "12.09.1996", "female", "l@l.com", "123456789"));
        // db.AddMember(new Member("John", "Smith", "05.03.1988", "male", "john.smith@email.net", "987654321"));
        // db.AddMember(new Member("Emily", "Wilson", "22.11.2001", "female", "emily.w@example.org", "555123456"));
        // db.AddMember(new Teacher("Sophia", "Garcia", "30.04.1992", "female", "sophia.g@domain.com", "998877665", "Math"));
        // db.AddMember(new Student("Michael", "Brown", "18.07.1975", "male", "m.brown75@mail.co.uk", "112233445", "C#", "17.02.2024"));
        controller.MainFlow();
    }
}
