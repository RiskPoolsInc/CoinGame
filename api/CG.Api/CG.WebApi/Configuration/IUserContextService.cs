﻿namespace CG.WebApi.Configuration; 

/// <summary>
///     Service for received user info from context.
/// </summary>
public interface IUserContextService {
    /// <summary>
    ///     Get user unique identificator from context.
    /// </summary>
    /// <returns>Unique identificator.</returns>
    Guid GetUserId();

    /// <summary>
    ///     Get user email from context.
    /// </summary>
    /// <returns>User email.</returns>
    string GetUserEmail();

    /// <summary>
    ///     Get user access token from context.
    /// </summary>
    /// <returns>Access token.</returns>
    string GetAccessToken();
}