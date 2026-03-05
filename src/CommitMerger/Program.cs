using CommitMerger;

DateTimeOffset[] dates = Process.GitFolders(Path.GetFullPath("../../../../../../"), "Scott");
Array.Sort(dates);
foreach (var date in dates)
    Console.WriteLine(date);