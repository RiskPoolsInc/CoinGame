using MediatR;
using System.Collections.Generic;

namespace App.Core.Requests.Database {
    [AdminAccess]
    public class GetClientMigrationsRequest : IRequest<Dictionary<string, string[]>> {
        public GetClientMigrationsRequest(int[]? databaseTypes = null) {
            DatabaseTypes = databaseTypes;
        }
        public int[]? DatabaseTypes { get; set; }
    }
}