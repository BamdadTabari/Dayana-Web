using DayanaWeb.Shared.BaseControl;
using DayanaWeb.Shared.EntityFramework.DTO.Blog;


namespace DayanaWeb.Client.Pages.General.Blog;

public partial class BlogMainPage
{
    List<PostDto> model = new();
    private int _selected = 1;
    private int _totalPagesCount = 3;
    protected override async Task OnInitializedAsync()
    {
        await GetPostDtosAsync();
    }

    private async Task GetPostDtosAsync()
    {
        DefaultPaginationFilter paginationFilter = new(_selected, 10);
        var paginatedData = await _httpService.GetPagedValue<PostDto>(Routes.Post + "get-post-list-by-filter", paginationFilter);
        model = paginatedData.Data;
        _totalPagesCount = paginatedData.TotalPages;
        this.StateHasChanged();
    }

    private async Task OnPageChange(int pageNumber)
    {
        _selected = pageNumber;
        await GetPostDtosAsync();
    }

    private void OnReadMoreButtonClicked(long id)
    {
        _navigationManager.NavigateTo($"/blog-post-page/{id}");
    }
}
