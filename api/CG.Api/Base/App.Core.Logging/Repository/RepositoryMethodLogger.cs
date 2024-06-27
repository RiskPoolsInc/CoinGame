using Microsoft.Extensions.Logging;

namespace App.Core.Logging.Repository;

public class RepositoryMethodLogger : BaseMethodLogger<RepositoryMethodLogger> {
    public RepositoryMethodLogger(ILogger<RepositoryMethodLogger> logger) : base(logger) {
        Filter = invocation => invocation.Method.Name != "GetAll";
    }
}