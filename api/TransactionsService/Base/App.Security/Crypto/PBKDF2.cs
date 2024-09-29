﻿using System.Security.Cryptography;

namespace App.Security.Crypto;

public sealed class PBKDF2 : IDisposable {
    private const int SALT_SIZE = 64;
    private const int KEY_SIZE = 256;
    private const int ITERATIONS_COUNT = 2048;
    private readonly Rfc2898DeriveBytes _crypto;
    private readonly byte[] _key;
    private readonly byte[] _preservedKey;

    private readonly byte[] _salt;

    /// <summary>
    ///     Constructs wrapper for password-based key derivation functionality. Should be used to generate key and salt.
    /// </summary>
    /// <param name="password">Password to generate key and salt</param>
    public PBKDF2(string password) {
        _crypto = new Rfc2898DeriveBytes(password, SALT_SIZE, ITERATIONS_COUNT);
        _salt = _crypto.Salt;
        _key = _crypto.GetBytes(KEY_SIZE);
    }

    public PBKDF2(string password, string preservedSalt) {
        _salt = Convert.FromBase64String(preservedSalt);
        _crypto = new Rfc2898DeriveBytes(password, _salt, ITERATIONS_COUNT);
        _key = _crypto.GetBytes(KEY_SIZE);
    }

    /// <summary>
    ///     Constructs wrapper for password-based key derivation functionality. Should be used to validate password against
    ///     preserved key and salt.
    /// </summary>
    /// <param name="password">Password to generate key</param>
    /// <param name="preservedKey">Preserved Key for password validation</param>
    /// <param name="preservedSalt">Preserved Salt for password validation</param>
    public PBKDF2(string password, string preservedKey, string preservedSalt) {
        _salt = Convert.FromBase64String(preservedSalt);
        _preservedKey = Convert.FromBase64String(preservedKey);
        _crypto = new Rfc2898DeriveBytes(password, _salt, ITERATIONS_COUNT);
        _key = _crypto.GetBytes(KEY_SIZE);
    }

    /// <summary>
    ///     Retrieves salt generated by key derivation function
    /// </summary>
    public string Salt => Convert.ToBase64String(_salt);

    /// <summary>
    ///     Retrieves key generated by key derivation function
    /// </summary>
    public string Key => Convert.ToBase64String(_key);

    /// <summary>
    ///     Disposes key derivation function object
    /// </summary>
    public void Dispose() {
        if (_crypto != null)
            _crypto.Dispose();
    }

    /// <summary>
    ///     Validates key, generated from password, against preserved key
    /// </summary>
    /// <returns>Evaluates to true if keys are the same</returns>
    public bool Validate() {
        if (_preservedKey == null)
            throw new NotSupportedException("This method could not be used if preserved key was not passed to constructor.");

        return _key.SequenceEqual(_preservedKey);
    }
}