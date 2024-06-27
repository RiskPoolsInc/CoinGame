using MediatR;

namespace App.Data.Sql.MigrateAfterBuild;

public class RunMigrateAfterBuild : IRequest<string[]> {
}