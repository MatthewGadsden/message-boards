namespace message_boards.Models;

public class Memory
{
    private HashSet<User> Users { get; set; } = new HashSet<User>();
    private HashSet<Project> Projects { get; set; } = new HashSet<Project>();

    public User GetOrCreateUser(string username)
    {
        var user = Users.SingleOrDefault(z => z.Name == username);
        if (user != null) return user;

        var newUser = new User()
        {
            Id = Users.Count,
            Name = username
        };

        Users.Add(newUser);
        return newUser;
    }
    
    public Project GetOrCreateProject(string projectName)
    {
        var project = Projects.SingleOrDefault(z => z.Name == projectName);
        if (project != null) return project;

        var newProject = new Project()
        {
            Id = Projects.Count,
            Name = projectName
        };

        Projects.Add(newProject);
        return newProject;
    }
}
