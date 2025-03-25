
using HelpWithAllWApi.Models;

namespace HelpWithAllWApi.Repositories.Base;
public interface IHelperRepository
{
    public Task<bool> InsertHelperAsync(Helper helper);

    public Task<IEnumerable<Helper>> SelectAllHelpersAsync();

    public Task<Helper> SelectHelperByIdAsync(int id);
    public Task<bool> DeleteHelperAsync(int id);
    public Task<bool> UpdateHelperAsync(Helper updatedHelper);
}
