using Microsoft.EntityFrameworkCore;

namespace PostgreSearch;

public static class RegisterLayerExtension
{
	private const string DatabaseName = "MasterDatabase";
	public static WebApplicationBuilder RegisterLayer(this WebApplicationBuilder builder)
	{
		builder.Services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseNpgsql(builder.Configuration.GetConnectionString(DatabaseName));
		});

		return builder;
	}
}
