using LibGit2Sharp;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;

namespace CommitMerger;

internal static class GitBuilder
{
    public static void CreateFromCommitFolder(
        string commitFileFolder = "CommitFiles",
        string gitOutputPath = "Anonymized-Git-Repo",
        string authorName = "Scott W Harden",
        string authorEmail = "swharden@gmail.com")
    {
        Stopwatch sw = Stopwatch.StartNew();

        Console.WriteLine($"Reading commit files from commit folder...");

        static DateTimeOffset filenameParser(string date) =>
            DateTimeOffset.ParseExact(date, "yyyy-MM-ddTHH-mm-ssZ", 
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

        DateTimeOffset[] dates = Directory
            .EnumerateFiles(commitFileFolder, "*.txt")
            .Select(path => Path.GetFileNameWithoutExtension(path))
            .Select(filenameParser)
            .ToArray();

        Console.WriteLine($"Creating new git repo with {dates.Length:N0} individual commits (slow)...");
        Create(gitOutputPath, dates, authorName, authorEmail);
        Console.WriteLine($"Completed in {sw.Elapsed.TotalSeconds:N2} sec");
    }

    private static void Create(string newRepoPath, DateTimeOffset[] timestamps, string authorName, string authorEmail)
    {
        Repository.Init(newRepoPath);
        using Repository repo = new(newRepoPath);

        var identity = new Identity(authorName, authorEmail);

        foreach (var timestamp in timestamps.OrderBy(t => t))
        {
            var signature = new Signature(identity, timestamp);
            repo.Commit(
                message: ".",
                author: signature,
                committer: signature,
                options: new CommitOptions { AllowEmptyCommit = true }
            );
        }
    }
}
