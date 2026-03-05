# Git Commit Aggregator

A .NET tool for aggregating anonymized commit activity from multiple git repositories into a single repository to facilitate high-level inspection of contributor activity and large-scale code changes across multiple projects.

## WIP: blog entry

**As a long time GitHub user, I've come to enjoy the green square activity graph as a way to visualize the ebbs and flows of my development efforts over the last decade.** Over the last few years I've been working more in repositories on other platforms (GitLab, Azure, and locally) which are not tracked by GitHub and do not contribute to the activity graph. 

**This page documents how I created an anonymized git activity aggregator** to combine commits across various untracked git repositories into a single private repo that can be used to accurately reflect my total development effort on GitHub's green square activity graph.

## Results

Before:

After:

## Theory of Operation
The code itself isn't too complex, leaned heavily on the [libgit2sharp](https://github.com/libgit2/libgit2sharp) .NET package, and its [source code is available](https://github.com/swharden/Git-Commit-Aggregator) so I'll describe what it does at a high level:

* Scan a folder of git repositories to identify all repos to analyze
* Scan the commit history of each git repo to generate an array of `DateTime`
* Optionally restrict commits to those matching a given name or email address
* Merge all commit `DateTime` values across all repos
* Create a new git repo locally and add one empty commit per `DateTime` value
* Create a new git repo in GitHub, set it as the origin for the local one, and push to it
* The GitHub activity graph will now include squares for the aggregated commits

## Future Directions
**This strategy could be extended to provide extensive analytics for multiple users working across large multi-project code bases.** Although the present implementation aimed to preserve anonymity of the analyzed source code, extending commit analysis records to include repository name, commit messages, and author details could facilitate generation of fascinating reports where users can be seen working across different projects at different times. Consider what an activity graph would look like for a single user if each square were colors according to the repo they were working in, or a graph that shows a single repo with different authors represented by different squares. The simplicity of the git analysis methods described here make graphs like these feasible to realize without much additional effort.

## Additional Resources
* Although I wrote a C# app to analyze git repos and create them from scratch, the heavy lifting was performed by the [libgit2sharp](https://github.com/libgit2/libgit2sharp) .NET package
* [Git-Commit-Aggregator](https://github.com/swharden/Git-Commit-Aggregator) (source code on GitHub)