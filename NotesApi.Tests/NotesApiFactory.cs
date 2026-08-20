using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NotesApi.Infrastructure.Data;

namespace NotesApi.Tests;

public class NotesApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptors = services
                .Where(d => d.ServiceType == typeof(DbContextOptions<NotesDbContext>)
                         || d.ServiceType == typeof(IDbContextOptionsConfiguration<NotesDbContext>))
                .ToList();

            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<NotesDbContext>(options =>
                options.UseInMemoryDatabase("NotesTestDb"));
        });
    }
}