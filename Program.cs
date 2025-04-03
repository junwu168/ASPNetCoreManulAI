using Azure.Monitor.OpenTelemetry.AspNetCore;
using OpenTelemetry.Resources;
using Azure.Monitor.OpenTelemetry.Profiler;

var resourceAttributes = new Dictionary<string, object> {
    { "service.name", "masonTest" },
    { "service.namespace", "OpenTelemetryDistro" },
    { "service.instance.id", "jw-desktop" }
};

var builder = WebApplication.CreateBuilder(args);

// Add OpenTelemetry with Azure Monitor and custom resource attributes.
builder.Services.AddOpenTelemetry()
    .UseAzureMonitor()
    .ConfigureResource(resourceBuilder => resourceBuilder.AddAttributes(resourceAttributes))
    .AddAzureMonitorProfiler();

// Add Razor Pages.
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
