using Core1._0.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Core1._0.Dbcontext
{
    public class Appdbcontext:IdentityDbContext<IdentityUser>
    {
        public Appdbcontext(DbContextOptions<Appdbcontext> options) : base(options)
        {

        }

        public DbSet<TouristRoute> TouristRoutes { get; set; }

        public DbSet<TouristRoutePicture> TouristRoutePictures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var touristRoutePicturesDataJsonData = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/Dbcontext/touristRoutePicturesData.json");
            IList<TouristRoutePicture> routesPic = JsonConvert.DeserializeObject<List<TouristRoutePicture>>(touristRoutePicturesDataJsonData);
            var touristRoutesDataJsonData = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/Dbcontext/touristRoutesData.json");
            IList<TouristRoute> routes = JsonConvert.DeserializeObject<List<TouristRoute>>(touristRoutesDataJsonData);
            modelBuilder.Entity<TouristRoute>().HasData(routes);
            modelBuilder.Entity<TouristRoutePicture>().HasData(routesPic);
            base.OnModelCreating(modelBuilder);
        }

    }
}
