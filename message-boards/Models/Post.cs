namespace message_boards.Models;

public class Post
{
    public required User User { get; set; }
    public required string Message { get; set; }
    public required DateTime PostedTime { get; set; }
    public required int ProjectId { get; set; }
}
