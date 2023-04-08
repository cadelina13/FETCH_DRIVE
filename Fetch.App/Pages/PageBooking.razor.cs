using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Drawing;

namespace Fetch.App.Pages
{
    partial class PageBooking
    {
        [Inject]
        public IJSRuntime jsRuntime { get; set; }
        

    }
}
