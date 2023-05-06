using DayanaWeb.Client.Shared;
using DayanaWeb.Shared.Basic.Classes;
using DayanaWeb.Shared.EntityFramework.DTO.Blog;
using MudBlazor;

namespace DayanaWeb.Client.Pages.Admin.Blog.Posts;

public partial class PostControlPage
{
    private IEnumerable<PostDto> pagedData;
    private MudTable<PostDto> table;
    private string searchString = "";
    private PostDto selectedItem = null;

    /// <summary>
    /// getting the paged, filtered and ordered data from the server
    /// </summary>
    private async Task<TableData<PostDto>> ServerReload(TableState state)
    {
        DefaultPaginationFilter paginationFilter = new(state.Page, state.PageSize);
        var paginatedData = await _httpService.GetPagedValue<PostDto>(BlogRoutes.PostCategory + CRUDRouts.ReadListByFilter, paginationFilter);
        pagedData = paginatedData.Data;
        return new TableData<PostDto>() { TotalItems = paginatedData.TotalCount, Items = pagedData };
    }

    #region Delete
    private async Task OnDelete(long id)
    {
        var parameters = new DialogParameters()
        {
            { "ContentText", "Do you really want to delete these record ? all sub-records will be deleted!! This process cannot be undo." },
            { "ButtonText", "Delete" },
            { "Color", Color.Error }
        };
        var dialog = await _dialogService.ShowAsync<CommonDialog>("Delete", parameters);
        var dialogResult = await dialog.Result;
        if (dialogResult.Canceled == false)
        {
            var response = await _httpService.DeleteValue(BlogRoutes.PostCategory + CRUDRouts.Create + $"/{id}");
            if (response.IsSuccessStatusCode)
            {
                _snackbar.Add("Post Deleted Succesfully", Severity.Success);
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

    #endregion

    #region Search
    private void OnSearch(string text)
    {
        searchString = text;
        table.ReloadServerData();
    }
    #endregion

    #region Edit
    private void Edit(long id)
    {
        _navigationManager.NavigateTo($"/p-p-edit/{id}");
    }
    #endregion
}
