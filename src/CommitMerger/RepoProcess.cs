namespace CommitMerger;

internal static class RepoProcess
{
    public static DateTimeOffset[] GitFolder(string gitFolder, string? userFilter = null)
    {
        Console.WriteLine($"scanning {gitFolder}");
        DateTimeOffset[] dates = RepoInspection.GetCommitDates(gitFolder, userFilter);
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
