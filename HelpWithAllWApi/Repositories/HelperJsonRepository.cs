namespace HelpWithAllWApi.Repositories;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HelpWithAllWApi.Models;
using HelpWithAllWApi.Repositories.Base;

public class HelperJsonRepository : IHelperRepository
{
    private string _filePath = "helpers.json";

    public HelperJsonRepository()
    {
        EnsureFileExists();
    }

    private void EnsureFileExists()
    {
        if (!File.Exists(_filePath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath));
            File.WriteAllText(_filePath, "[]");
        }
    }

    public async Task<bool> InsertHelperAsync(Helper helper)
    {
        EnsureFileExists();
        var helpers = await SelectAllHelpersAsync();
        if (helpers.Any(h => h.Id == helper.Id)) return false;
        
        helpers.Add(helper);
        var json = JsonSerializer.Serialize(helpers, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, json);
        return true;
    }

    public async Task<bool> DeleteHelperAsync(int id)
    {
        EnsureFileExists();
        var helpers = await SelectAllHelpersAsync();
        var helper = helpers.FirstOrDefault(h => h.Id == id);
        
        if (helper == null) return false;

        helpers.Remove(helper);
        var json = JsonSerializer.Serialize(helpers, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, json);
        return true;
    }

    public async Task<List<Helper>> SelectAllHelpersAsync()
    {
        EnsureFileExists();
        var json = await File.ReadAllTextAsync(_filePath);
        var helpers = JsonSerializer.Deserialize<List<Helper>>(json) ?? new List<Helper>();
        return helpers;

    }

    public async Task<Helper> SelectHelperByIdAsync(int id)
    {
        EnsureFileExists();
        var helpers = await SelectAllHelpersAsync();
        return helpers.FirstOrDefault(h => h.Id == id);
    }

    public async Task<bool> UpdateHelperAsync(Helper updatedHelper)
    {
        EnsureFileExists();
        var helpers = await SelectAllHelpersAsync();
      
        var index = helpers.FindIndex(h => h.Id == updatedHelper.Id);
        
        if (index == -1) return false;

        helpers[index] = updatedHelper;
        var json = JsonSerializer.Serialize(helpers, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, json);
        return true;
    }
}