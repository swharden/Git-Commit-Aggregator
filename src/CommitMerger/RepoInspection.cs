using LibGit2Sharp;

namespace CommitMerger;

internal static class RepoInspection
{
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
