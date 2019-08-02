using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Gram.Web.Areas.Identity.IdentityHostingStartup))]
namespace Gram.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}