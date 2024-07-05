using Microsoft.EntityFrameworkCore;

namespace StudentStore.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<StudentStoreContext>();
        await dbContext.Database.MigrateAsync();
    }
}
