using DayanaWeb.Client.Shared;
using DayanaWeb.Shared.Basic.Classes;
using DayanaWeb.Shared.EntityFramework.DTO.Blog;
using Microsoft.AspNetCore.Components.Routing;
using MudBlazor;

namespace DayanaWeb.Client.Pages.Admin.Blog.Categories;

public partial class PostCategoryPage
{
    private IEnumerable<PostCategoryDto> pagedData;
    private MudTable<PostCategoryDto> table;
    private string searchString = "";
    private PostCategoryDto selectedItem = null;
    private bool isBusy = false;

    /// <summary>
    /// getting the paged, filtered and ordered data from the server
    /// </summary>
    private async Task<TableData<PostCategoryDto>> ServerReload(TableState state)
    {
        DefaultPaginationFilter paginationFilter = new(state.Page, state.PageSize);
        var paginatedData = await _httpService.GetPagedValue<PostCategoryDto>(BlogRoutes.PostCategory + CRUDRouts.ReadListByFilter, paginationFilter);
        pagedData = paginatedData.Data;
        return new TableData<PostCategoryDto>() { TotalItems = paginatedData.TotalCount, Items = pagedData };
    }

    private async Task OnDelete(long id)
    {
        var parameters = new DialogParameters();
        parameters.Add("ContentText", "Do you really want to delete these records? This process cannot be undone.");
        parameters.Add("ButtonText", "Delete");
        parameters.Add("Color", Color.Error);
        var dialog = await _dialogService.ShowAsync<CommonDialog>("Delete", parameters);
        var dialogResult = await dialog.Result;
        if (dialogResult.Canceled == false)
        {
            var response = await _httpService.DeleteValue(BlogRoutes.PostCategory + CRUDRouts.Delete + $"{id}");
            if (response.IsSuccessStatusCode)
            {
                _snackbar.Add("Post Category Deleted Succesfully", Severity.Success);
                await table.ReloadServerData();
            }
            else
            {
                _snackbar.Add("Operation Failed", Severity.Error);
            }
        }
        else
        {
            _snackbar.Add("Operation Canceled", Severity.Warning);

        }
    }
    private void OnSearch(string text)
    {
        searchString = text;
        table.ReloadServerData();
    }

    private void Edit(long id)
    {
        _navigationManager.NavigateTo($"/p-p-edit/{id}");
    }
}
