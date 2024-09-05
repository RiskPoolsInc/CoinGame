using System;
using System.IO;

namespace App.Services.Telegram.Helpers; 

public class ImageHelper {
    public static string ConvertLocalFileToBase64(string fullPathToImage) {
        return ConvertBytesToBase64String(File.ReadAllBytes(fullPathToImage));
    }

    public static string ConvertBytesToBase64String(byte[] bytes) {
        return Convert.ToBase64String(bytes);
    }
}