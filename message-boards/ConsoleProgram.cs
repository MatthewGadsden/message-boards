using System.Text;
using message_boards.Models;

namespace message_boards;

public class ConsoleProgram
{
    private Memory _memory = new();
    
    private void Post(string username, string projectName, string[] messages)
    {
        var message = string.Join(" ", messages);
        
        var user = _memory.GetOrCreateUser(username);
        var project = _memory.GetOrCreateProject(projectName);

        var newPost = new Post()
        {
            User = user,
            Message = message,
        };

        project.PostMessage(newPost);
    }
    
    private string GetProjectWall(string projectName)
    {
        var project = _memory.GetProject(projectName);
        if (project == null) throw new Exception("project not found");
        var posts = project.GetPosts();

        var stringBuilder = new StringBuilder();
        foreach (var post in posts)
        {
            stringBuilder.AppendLine($"{post.User.Name}");
            stringBuilder.AppendLine($"{post.Message}");
        }
        
        return stringBuilder.ToString();
    }

    public void Run()
    {
        while (true)
        {
            var command = Console.ReadLine();
            var arguments = command!.Split(" ").ToList();
            if (arguments[0] == "exit") break;
    
            switch (arguments!)
            {
                // posting case
                case { Count: >= 4 }:
                    Post(arguments[0], arguments[2].Trim('@'), arguments.Skip(3).ToArray());
                    break;
                // reading case
                case { Count: 1 }:
                    var wallOutput = GetProjectWall(arguments[0]);
                    Console.WriteLine(wallOutput);
                    break;
                // following case
                case { Count: 3 }:
                    break;
                // wall case
                case { Count: 2 }:
                    break;
                default:
                    Console.WriteLine("Bad commands entered...");
                    break;
            }
        }
        
        Console.WriteLine("quitting...");
    }
}