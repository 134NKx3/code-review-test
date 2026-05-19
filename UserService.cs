@"
public class UserService
{
    private readonly string _connStr;

    public UserService(string connectionString)
    {
        _connStr = connectionString;
    }

    // Bug 1: SQL Injection
    public User GetUser(string username)
    {
        var query = "SELECT * FROM Users WHERE Name = '" + username + "'";
        using var conn = new SqlConnection(_connStr);
        return conn.QueryFirstOrDefault<User>(query);
    }

    // Bug 2: Thread.Sleep แทน Task.Delay
    public void ProcessUsers()
    {
        Console.WriteLine("Processing...");
        Thread.Sleep(5000);
        Console.WriteLine("Done");
    }

    // Bug 3: ไม่มี error handling
    public void DeleteUser(int id)
    {
        var query = "DELETE FROM Users WHERE Id = " + id;
        using var conn = new SqlConnection(_connStr);
        conn.Execute(query);
    }
}
"@ | Out-File -FilePath UserService.cs -Encoding utf8// webhook test
