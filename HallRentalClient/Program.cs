using HallRentalClient;
using HallRentalClient.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
//@ Ben
// this client instance will be used to  make calls or send request to the (HallRental Api)
// the URL is the address that points to the API Component.
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7254/") });
//@Ben
// Registering the service
builder.Services.AddScoped<CustomerService>();

await builder.Build().RunAsync();
