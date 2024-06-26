using MediatR;
using System.Collections.Generic;

namespace App.Core.Requests.Database {
    [AdminAccess]
    public class GetMigrationsRequest : IRequest<string[]> {
        public GetMigrationsRequest() {
        }
    }
}