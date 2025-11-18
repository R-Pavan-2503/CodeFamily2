namespace CodeFamily.Core.Services;

public interface IGitService
{
    /// <summary>
    /// Clones a repository to a local temporary path.
    /// </summary>
    /// <param name="repositoryUrl">The HTTP URL of the GitHub repo.</param>
    /// <returns>The full local path where the repo was cloned.</returns>
    string CloneRepository(string repositoryUrl);
}