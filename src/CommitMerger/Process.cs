namespace CommitMerger;

internal static class Process
{
    public static DateTimeOffset[] GitFolder(string gitFolder, string? userFilter = null)
    {
        Console.WriteLine(gitFolder);
        DateTimeOffset[] dates = RepoInspection.GetCommitDates(gitFolder, userFilter);
        foreach (var date in dates)
        {
            Console.WriteLine($"  {date:O}");
        }
        return dates;
    }

    public static DateTimeOffset[] GitFolders(string folderOfGitFolders, string? userFilter = null)
    {
        string[] gitFolders = RepoLocate.LocateGitRepoFolders(folderOfGitFolders);
        List<DateTimeOffset> dates = [];
        foreach (string gitFolder in gitFolders)
        {
            dates.AddRange(GitFolder(gitFolder, userFilter));
        }
        return dates.ToArray();
    }
}
