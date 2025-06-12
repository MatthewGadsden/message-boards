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
            PostedTime = DateTime.UtcNow,
            ProjectId = project.Id
        };

        _memory.PostMessage(newPost);
    }
    
    private string GetProjectWall(string projectName)
    {
        var project = _memory.GetOrCreateProject(projectName);
        var posts = _memory.GetPostsByProjectId(project.Id).OrderBy(z => z.PostedTime);

        var stringBuilder = new StringBuilder();
        foreach (var post in posts)
        {
            var timeDifference = DateTime.UtcNow - post.PostedTime;
            stringBuilder.AppendLine($"{post.User.Name}");
            stringBuilder.AppendLine($"{post.Message} ({Math.Round(timeDifference.TotalMinutes)} minutes ago)");
        }
        
        return stringBuilder.ToString();
    }
    
    private string GetUserWall(string username)
    {
        var user = _memory.GetOrCreateUser(username);
        var projects = user!.GetFollowingProjects();
        var projectDictionary = projects.ToDictionary(z => z.Id, z => z);
        var posts = _memory.GetPostsByProjectIds(projects.Select(z => z.Id));
        
        var stringBuilder = new StringBuilder();
        foreach (var post in posts)
        {
            stringBuilder.AppendLine(post.GetUserWallDisplay(projectDictionary[post.ProjectId]));
        }
        
        return stringBuilder.ToString();
    }
    
    private void Follow(string username, string projectName)
    {
        var project = _memory.GetOrCreateProject(projectName);
        var user = _memory.GetOrCreateUser(username);
        user.FollowProject(project!);
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
                    Follow(arguments[0], arguments[2]);
                    break;
                // wall case
                case { Count: 2 }:
                    Console.WriteLine(GetUserWall(arguments[0]));
                    break;
                default:
                    Console.WriteLine("Bad commands entered...");
                    break;
            }
        }
        
        Console.WriteLine("quitting...");
    }
}