using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FetchApp.Data
{
    public interface IDataHandler
    {
        public void Insert<T>(T data);
    }

    public class DataHandler : IDataHandler
    {
        public readonly string UserId = "09163617169";
        [Inject]
        public IJSRuntime js { get; set; }
        public void Insert<T>(T data)
        {
            js.InvokeAsync<string>("db.insert", UserId, JsonSerializer.Serialize(data));
        }

    }
}
