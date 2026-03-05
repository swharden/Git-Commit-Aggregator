namespace CommitMerger;

internal static class Process
{
    public static void GitFolder(string gitFolder, string? userFilter = null)
    {
        Console.WriteLine(gitFolder);
        DateTimeOffset[] dates = RepoInspection.GetCommitDates(gitFolder, userFilter);
        foreach (var date in dates)
        {
            Console.WriteLine($"  {date:O}");
        }
    }

    public static void GitFolders(string folderOfGitFolders, string? userFilter = null)
    {
        string[] gitFolders = RepoLocate.LocateGitRepoFolders(folderOfGitFolders);
        foreach (string gitFolder in gitFolders)
        {
            GitFolder(gitFolder, userFilter);
        }
    }
}
