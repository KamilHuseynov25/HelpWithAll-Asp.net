namespace HelpWithAllWApi.Repositories;

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using HelpWithAllWApi.Models;
using HelpWithAllWApi.Repositories.Base;


public class HelperJsonRepository : IHelperRepository
{
    private string _filePath = "HelpWithAllWApi/helpers.json";

    public  async Task<bool> InsertHelperAsync(Helper helper)
    {
        var helpers = await SelectAllHelpersAsync();
        if (helpers.Any(h => h.Id == helper.Id) || helpers == null) return false;
        var helpersList = helpers.ToList();
        helpersList.Add(helper);
        var json = JsonSerializer.Serialize(helpersList, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, json);
        return true;
    }

    public  async Task<bool> DeleteHelperAsync(int id)
    {
        var helpers = await SelectAllHelpersAsync();
        var helper = helpers.FirstOrDefault(h => h.Id == id);
        
        if (helper == null)
        {
            return false;
        }

        var helpersList = helpers.ToList();
        helpersList.Remove(helper);

        var json = JsonSerializer.Serialize(helpersList, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, json);
        return true;
    }

    public  async Task<IEnumerable<Helper>> SelectAllHelpersAsync()
    {if (!File.Exists(_filePath)) return new List<Helper>();
        

        var json = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<Helper>>(json) ?? new List<Helper>();
        
    }

    public  async Task<Helper> SelectHelperByIdAsync(int id)
    {
        var helpers = await SelectAllHelpersAsync();
        var helpersList = helpers.ToList();
        return helpersList.FirstOrDefault(h => h.Id == id);
    }

    public  async Task<bool> UpdateHelperAsync(Helper updatedHelper)
    {
        var helpers = await SelectAllHelpersAsync();
        var helpersList = helpers.ToList();
        var index = helpersList.FindIndex(h => h.Id == updatedHelper.Id);
        
        if (index == -1)
        {
            return false;
        }

        helpersList[index] = updatedHelper;
        var json = JsonSerializer.Serialize(helpersList, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, json);
        return true;
    }
}
