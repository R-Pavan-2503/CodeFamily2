using CodeFamily.Core.Models;

namespace CodeFamily.Core.Services;

public interface ICommitService
{
    IEnumerable<CommitLogItem> GetCommitLog(string repoPath);
}