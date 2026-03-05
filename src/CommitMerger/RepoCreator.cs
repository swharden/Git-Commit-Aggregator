using LibGit2Sharp;

namespace CommitMerger;

internal static class RepoCreator
{
    public static void Create(string newRepoPath, DateTimeOffset[] timestamps, string authorName, string authorEmail)
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
