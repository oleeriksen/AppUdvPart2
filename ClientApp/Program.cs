using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClientApp;
using ClientApp.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// brug af local storage
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton<IBikeService, BikeServiceMock>();
builder.Services.AddSingleton<IProductService, ProductServiceHttp>();

await builder.Build().RunAsync();