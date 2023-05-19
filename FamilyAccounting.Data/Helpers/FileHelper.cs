using Microsoft.AspNetCore.Http;

namespace FamilyAccounting.Data.Helpers;

public class FileHelper
{
    private const string Wwwroot = "wwwroot";
    private const string FilesFolder = "Files";

    private static void CheckDirectory(string folder)
    {
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
    }

    public static async Task<string> SaveUserFile(IFormFile file)
    {
        return await SaveFile(file, "UserFiles");
    }

    public static async Task<string> SaveFile(IFormFile file, string folder)
    {
        CheckDirectory(Path.Combine(Wwwroot, FilesFolder, folder));

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

        var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        await File.WriteAllBytesAsync(Path.Combine(Wwwroot, FilesFolder, folder, fileName), ms.ToArray());

        return $"/{FilesFolder}/{folder}/{fileName}";
    }
}