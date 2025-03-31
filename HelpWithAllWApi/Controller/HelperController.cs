

namespace HelpWithAllWApi.Controllers;
using HelpWithAllWApi.Models;
using Microsoft.AspNetCore.Mvc;
using HelpWithAllWApi.Repositories;
using HelpWithAllWApi.Repositories.Base;

[Route("[controller]/[action]")]
public class HelperController : ControllerBase
{
    private readonly IHelperRepository helperRepository;
    public HelperController(IHelperRepository helperRepository){
        this.helperRepository = helperRepository;
    }
    [HttpPost]
    public async Task<int> AddHelperAsync([FromForm]Helper helper) {


        await helperRepository.InsertHelperAsync(helper);
        return helper.Id;
    }
    [HttpGet]
    public async Task ShowAllHelpersAsync(){
        await helperRepository.SelectAllHelpersAsync();

    }
    [HttpGet]
    public async Task ShowHelperByIdAsync(int id){
        await helperRepository.SelectHelperByIdAsync(id);
    }
    [HttpPut]
    public async Task ChangeHelperAsync([FromBody]Helper helper){
        await helperRepository.UpdateHelperAsync(helper);
    }
    [HttpDelete]
    public async Task<bool> RemoveHelperAsync(int id){
        var result = await helperRepository.DeleteHelperAsync(id);
        return result;
    }

}
