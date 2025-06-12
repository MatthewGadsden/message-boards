namespace message_boards.Models;

public class Post
{
    public required User User { get; set; }
    public required string Message { get; set; }
    public required DateTime PostedTime { get; set; }
    public required int ProjectId { get; set; }
    
    public string GetUserWallDisplay(Project project)
    {
        var timeDifference = DateTime.UtcNow - PostedTime;
        return $"{project.Name} - {User.Name}: {Message} ({Math.Round(timeDifference.TotalMinutes)} minutes ago)";
    }
}
