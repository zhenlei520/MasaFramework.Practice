namespace Assignment.MinimalApiDemo.Response;

public class UserItemResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public UserItemResponse(int id, string name, int age)
    {
        Id = id;
        Name = name;
        Age = age;
    }
}
