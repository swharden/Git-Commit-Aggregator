using CommitMerger;
using System.Diagnostics;

Stopwatch sw = Stopwatch.StartNew();
string searchPath = Path.GetFullPath("../../../../../../");
Console.WriteLine($"Scanning {searchPath}");
DateTimeOffset[] dates = RepoProcess.GitFolders(searchPath, "Scott");
TimeSpan timeSpan = dates.Max() - dates.Min();
Console.WriteLine($"Identified {dates.Length:N0} commits over {timeSpan.TotalDays:N0} days");

string outputPath = Path.GetFullPath("Aggregated-Anonymized-Commits");
Console.WriteLine($"Saving: {outputPath}");
RepoCreator.Create(outputPath, dates, "Scott W Harden", "swharden@gmail.com");
Console.WriteLine($"Completed in {sw.Elapsed.TotalSeconds:N2} sec");