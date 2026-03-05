using LibGit2Sharp;
using System.Diagnostics;

namespace CommitMerger;

internal static class GitDateExtractor
{
    public static void ScanReposAndCreateCommitFiles(
        string searchPath,
        string commitFileFolder = "CommitFiles",
        string? userFilter = "Scott",
        bool overwrite = true)
    {
        Stopwatch sw = Stopwatch.StartNew();

        Console.WriteLine($"Scanning {searchPath}");
        DateTimeOffset[] dates = GitFolders(searchPath, userFilter);
        TimeSpan timeSpan = dates.Max() - dates.Min();
        Console.WriteLine($"Identified {dates.Length:N0} commits over {timeSpan.TotalDays:N0} days");

        Console.WriteLine($"Writing commit files...");
        string commitsPath = Path.GetFullPath(commitFileFolder);
        if (overwrite && Directory.Exists(commitsPath))
            Directory.Delete(commitsPath, true);
        if (!Directory.Exists(commitsPath))
            Directory.CreateDirectory(commitsPath);
        foreach (DateTimeOffset date in dates)
        {
            string isoSafe = date.ToUniversalTime().ToString("yyyy-MM-ddTHH-mm-ssZ");
            string commitFilename = Path.Combine(commitsPath, $"{isoSafe}.txt");
            File.WriteAllText(commitFilename, date.ToUniversalTime().ToString("O"));
        }

        Console.WriteLine($"Completed in {sw.Elapsed.TotalSeconds:N2} sec");
        Console.WriteLine(commitsPath);
    }

    private static DateTimeOffset[] GitFolder(string gitFolder, string? userFilter = null)
    {
        Console.WriteLine($"scanning {gitFolder}");
        DateTimeOffset[] dates = GetCommitDates(gitFolder, userFilter);
        return dates;
    }

    private static DateTimeOffset[] GitFolders(string folderOfGitFolders, string? userFilter = null)
    {
        string[] gitFolders = LocateGitRepoFolders(folderOfGitFolders);
        List<DateTimeOffset> dates = [];
        foreach (string gitFolder in gitFolders)
        {
            dates.AddRange(GitFolder(gitFolder, userFilter));
        }
        return dates.ToArray();
    }

    public static string[] LocateGitRepoFolders(string folder)
    {
        return Directory.GetDirectories(folder)
            .Where(dir => Repository.IsValid(dir))
            .ToArray();
    }

    public static DateTimeOffset[] GetCommitDates(string repoPath, string? userFilter = null)
    {
        using Repository repo = new(repoPath);
        return repo.Commits
            .QueryBy(new CommitFilter { SortBy = CommitSortStrategies.Time })
            .Where(c => userFilter == null ||
                        c.Author.Name.Contains(userFilter, StringComparison.OrdinalIgnoreCase) ||
                        c.Author.Email.Contains(userFilter, StringComparison.OrdinalIgnoreCase))
            .Select(c => c.Author.When)
            .ToArray();
    }
}
