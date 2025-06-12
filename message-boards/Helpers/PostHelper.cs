using message_boards.Models;

namespace message_boards.Helpers;

public static class PostHelper
{
    public static string GetUserWallDisplay(this Post @this, Project project)
    {
        var timeDifference = DateTime.UtcNow - @this.PostedTime;
        return $"{project.Name} - {@this.User.Name}: {@this.Message} ({Math.Round(timeDifference.TotalMinutes)} minutes ago)";
    }

    public static string GetProjectWallDisplay(this Post @this)
    {
        var timeDifference = DateTime.UtcNow - @this.PostedTime;
        return $"{@this.User.Name}\n" +
               $"{@this.Message} ({Math.Round(timeDifference.TotalMinutes)} minutes ago)";
    }
}