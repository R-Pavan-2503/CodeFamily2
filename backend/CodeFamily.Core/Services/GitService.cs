using LibGit2Sharp;

namespace CodeFamily.Core.Services;

public class GitService : IGitService
{
    // Ideally, this comes from config, but for MVP we hardcode the storage root
    private readonly string _baseStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "repos_temp");

    public string CloneRepository(string repositoryUrl)
    {
        // 1. Generate a unique folder name (using a GUID or the repo name)
        var folderName = Guid.NewGuid().ToString();
        var localPath = Path.Combine(_baseStoragePath, folderName);

        // 2. Ensure directory exists
        if (!Directory.Exists(_baseStoragePath))
        {
            Directory.CreateDirectory(_baseStoragePath);
        }

        // 3. Clone using LibGit2Sharp
        // This performs the heavy network I/O
        Repository.Clone(repositoryUrl, localPath);

        return localPath;
    }
}