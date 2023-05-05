using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DayanaWeb.Client.Shared;

public partial class CommonDialog
{
    [Parameter] public string ContentText { get; set; }
    [Parameter] public string ButtonText { get; set; }
    [Parameter] public Color Color { get; set; }
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    void Submit() => MudDialog.Close(DialogResult.Ok(true));
    void Cancel() => MudDialog.Cancel();
}
