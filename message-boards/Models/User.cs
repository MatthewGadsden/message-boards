namespace message_boards.Models;

public class User
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    
    // users that this user follows ids
    private HashSet<int> FollowingProjectIds { get; set; } = [];
}
