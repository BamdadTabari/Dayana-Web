using DayanaWeb.Client.Shared;
using DayanaWeb.Shared.BaseControl;
using DayanaWeb.Shared.EntityFramework.DTO.Blog;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DayanaWeb.Client.Pages.General.Blog.BlogPostComment;

public partial class PostComments
{
    [Parameter]
    public string PostId { get; set; }
    List<PostFeedBackDto> model = new();
    private int _selected = 1;
    private int _totalPagesCount = 3;
    protected override async Task OnInitializedAsync()
    {
        await GetPostCommentsDtosAsync();
    }

    private async Task GetPostCommentsDtosAsync()
    {
        DefaultPaginationFilter paginationFilter = new(_selected, 10);
        var paginatedData = await _httpService.GetPagedValue<PostFeedBackDto>(Routes.PostFeedback + "get-post-feedback-list-by-filter", paginationFilter);
        model = paginatedData.Data;
        _totalPagesCount = paginatedData.TotalPages;
        this.StateHasChanged();
    }

    private async Task OnPageChange(int pageNumber)
    {
        _selected = pageNumber;
        await GetPostCommentsDtosAsync();
    }
    private async Task Delete(long id)
    {
        var parameters = new DialogParameters();
        parameters.Add("ContentText", "Do you really want to delete these records? This process cannot be undone.");
        parameters.Add("ButtonText", "Delete");
        parameters.Add("Color", Color.Error);
        var dialog = await _dialogService.ShowAsync<CommonDialog>("Delete", parameters);
        var dialogResult = await dialog.Result;
        if (dialogResult.Canceled == false)
        {
            var response = await _httpService.DeleteValue(Routes.PostFeedback + $"delete-post-feedback/{id}");
            if (response.IsSuccessStatusCode)
            {
                _snackbar.Add("Post Comment Deleted Succesfully", Severity.Success);
                await GetPostCommentsDtosAsync();
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
}
