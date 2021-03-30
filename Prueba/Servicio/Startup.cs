// Unused usings removed
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TTI.Practicas.Data;
using System;
using System.Threading.Tasks;

namespace TodoApi
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ToDoListContext>(opt =>
                opt.UseSqlServer(@"Server=.;Database=ToDoList;Trusted_Connection=True;"));
            services.AddControllers();

            /*
            services.AddTransient(); => Crea un nuevo objeto cada vez
            services.AddScoped(); => Crea un nuevo objeto para cada ambito (llamada al servicio)
            services.AddSingleton(); => Crea un objeto para toda la aplicacion
            */
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



            MigrateDB(app.ApplicationServices.CreateScope().ServiceProvider);
        }
        private void MigrateDB(IServiceProvider sp)
        {
            var con = sp.GetRequiredService<ToDoListContext>();
            con.Database.Migrate();
        }
    }
}