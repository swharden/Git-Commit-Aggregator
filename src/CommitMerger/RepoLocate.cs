using LibGit2Sharp;

namespace CommitMerger;

internal static class RepoLocate
{
    public static string[] LocateGitRepoFolders(string folder)
    {
        return Directory.GetDirectories(folder)
            .Where(dir => Repository.IsValid(dir))
            .ToArray();
    }
}
