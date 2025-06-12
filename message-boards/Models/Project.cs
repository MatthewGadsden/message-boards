namespace message_boards.Models;

public class Project
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    // posts on this message board
    private List<Post> Posts { get; set; } = [];

    public void PostMessage(Post newPost)
    {
        Posts.Add(newPost);
    }

    public List<Post> GetPosts() => Posts;
}