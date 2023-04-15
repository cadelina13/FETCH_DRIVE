using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Drawing;

namespace Fetch.Web.Pages
{
    partial class PageBooking
    {
        [Inject]
        public IJSRuntime jsRuntime { get; set; }
    public ElementReference searchBoxContainer;
        public ElementReference mapContainer;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // await jsRuntime.InvokeVoidAsync("initMap", mapContainer);
                await jsRuntime.InvokeVoidAsync("getLocation", mapContainer, searchBoxContainer);
                await InvokeAsync(StateHasChanged);
            }
        }

        [JSInvokable("DestinationResultMethod")]
        public static void MapInitializedAsync()
        {
            Console.WriteLine("Map initialized!");
        }

        async void NextClick()
        {
            nav.NavigateTo("/booking/dropoff");
        }
    }
}
