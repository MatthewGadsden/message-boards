namespace message_boards.Models;

public class Post
{
    public User User { get; set; }
    public string Message { get; set; }
    public DateTime PostedTime { get; set; }
}
