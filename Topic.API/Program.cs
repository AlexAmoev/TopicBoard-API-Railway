using Microsoft.EntityFrameworkCore;
using Topic.Data;

namespace Topic.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // For Admin Test:

            //  Admin ID:     8716071C-1D9B-48FD-B3D0-F059C4FB8031

            //  Login:      admin@gmail.com

            //  Password:       Admin123!

            // For Nika Test:

            //  Nika ID:     D514EDC9-94BB-416F-AF9D-7C13669689C9

            //  Login:      nika@gmail.com

            //  Password:       Nika123!

            // For Gio Test:

            //  Gio ID:     87746F88-DC38-4756-924A-B95CFF3A1D8A

            //  Login:      gio@gmail.com

            //  Password:       Gio123!


            // Add services to the container. 


            builder.AddDatabaseContext();
            builder.ConfigureJwtOptions();
            builder.AddIdentity();
            builder.AddAuthentication();
            builder.AddHttpContextAccessor();
            builder.AddScopedServices();
            builder.AddControllers();
            builder.AddEndpointsApiExplorer();
            builder.AddSwaggerGen();
            builder.AddBackGroundServices();


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
