using System;

public class PhotoConverter
{
    public static string ConvertToBase64String(byte[] photo)
    {
        return Convert.ToBase64String(photo);
    }

    public static byte[] ConvertFromBase64String(string base64String)
    {
        return Convert.FromBase64String(base64String);
    }
}
