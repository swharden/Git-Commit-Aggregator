using CommitMerger;

string searchPath = Path.GetFullPath("../../../../../../");
GitDateExtractor.ScanReposAndCreateCommitFiles(searchPath);
GitBuilder.CreateFromCommitFolder();