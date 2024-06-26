﻿using MediatR;

namespace App.Interfaces.Core.Requests; 

public interface IPagedRequest : ISortedRequest {
    int? Page { get; }
    int? Size { get; }
    public bool? GetAll { get; set; }
    public bool? WithDeleted { get; set; }
    int? Skip { get; }
}

public interface IPagedRequest<T> : IRequest<IPagedList<T>>, IPagedRequest {
}