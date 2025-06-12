namespace message_boards.Models;

public class Memory
{
    private HashSet<User> Users { get; set; } = new HashSet<User>();
    private HashSet<Project> Projects { get; set; } = new HashSet<Project>();
    private HashSet<Post> Posts { get; set; } = new HashSet<Post>();

    public User? GetUser(string username)
    {
        return Users.SingleOrDefault(z => z.Name == username);
    }

    public User GetOrCreateUser(string username)
    {
        var user = GetUser(username);
        if (user != null) return user;

        var newUser = new User()
        {
            Id = Users.Count,
            Name = username
        };

        Users.Add(newUser);
        return newUser;
    }

    public Project? GetProject(string projectName)
    {
        return Projects.SingleOrDefault(z => z.Name == projectName);
    }
    
    public Project GetOrCreateProject(string projectName)
    {
        var project = GetProject(projectName);
        if (project != null) return project;

        var newProject = new Project()
        {
            Id = Projects.Count,
            Name = projectName
        };

        Projects.Add(newProject);
        return newProject;
    }

    public void PostMessage(Post newPost)
    {
        Posts.Add(newPost);
    }
    
    public Post[] GetPostsByProjectId(int projectId) => GetPostsByProjectIds([projectId]);
    public Post[] GetPostsByProjectIds(IEnumerable<int> projectIds)
    {
        return Posts.Where(z => projectIds.Contains(z.ProjectId)).ToArray();
    }
}
