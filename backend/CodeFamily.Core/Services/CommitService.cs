using LibGit2Sharp;
using CodeFamily.Core.Models;

namespace CodeFamily.Core.Services;

public class CommitService : ICommitService
{
    public IEnumerable<CommitLogItem> GetCommitLog(string repoPath)
    {
        if (!Directory.Exists(repoPath))
        {
            throw new DirectoryNotFoundException($"Repository not found at {repoPath}");
        }

        using (var repo = new Repository(repoPath))
        {
            // We filter to the main branch or HEAD
            var commits = repo.Commits.QueryBy(new CommitFilter
            {
                SortBy = CommitSortStrategies.Topological | CommitSortStrategies.Time
            });

            foreach (var commit in commits)
            {
                yield return new CommitLogItem
                {
                    Sha = commit.Sha,
                    Message = commit.MessageShort,
                    AuthorName = commit.Author.Name,
                    AuthorEmail = commit.Author.Email,
                    Date = commit.Author.When
                };
            }
        }
    }
}