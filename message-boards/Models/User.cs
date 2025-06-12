namespace message_boards.Models;

public class User
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    
    // users that this user follows ids
    private HashSet<Project> FollowingProjects { get; set; } = [];

    public void FollowProject(Project project)
    {
        FollowingProjects.Add(project);
    }

    public Project[] GetFollowingProjects() => FollowingProjects.ToArray();
}