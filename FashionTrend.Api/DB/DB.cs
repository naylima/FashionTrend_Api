public class DB
{
	public static void CreateDatabase(WebApplication app)
	{
        var serviceScope = app.Services.CreateScope();

        var dataContext = serviceScope.ServiceProvider.GetService<AppDbContext>();

        dataContext?.Database.EnsureCreated();
    }
}

