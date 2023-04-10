using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Drawing;

namespace Fetch.App.Pages
{
    partial class PageBooking
    {
        [Inject]
        public IJSRuntime jsRuntime { get; set; }
        public ElementReference mapContainer;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
               // await jsRuntime.InvokeVoidAsync("initMap", mapContainer);
                await jsRuntime.InvokeVoidAsync("getLocation", mapContainer);
                await InvokeAsync(StateHasChanged);
            }
        }

        [JSInvokable]
        public static void MapInitializedAsync()
        {
            Console.WriteLine("Map initialized!");
        }
    }
}
