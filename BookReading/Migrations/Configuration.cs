namespace BookReading.Migrations
{
    using BookReading.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookReading.DAL.BookContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BookReading.DAL.BookContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.SliderItems.AddOrUpdate(s => new { s.Title, s.Button, s.Image },
                new SliderItem
                {
                    Image = "slider/slide1.jpg",
                    Button = "Shop Books",
                    Title = "<h3>welcome to bookstore</h3><h5>Discover the best books online with us</h5>"
                },
                new SliderItem
                {
                    Image = "slider/slide2.jpg",
                    Button = "Shop Books",
                    Title = "<h3>welcome to bookstore</h3><h5>Discover the best books online with us</h5>"
                },
                new SliderItem
                {
                    Image = "slider/slide3.jpg",
                    Button = "Shop Books",
                    Title = "<h3>welcome to bookstore</h3><h5>Discover the best books online with us</h5>"
                },
                new SliderItem
                {
                    Image = "slider/slide4.jpg",
                    Button = "Shop Books",
                    Title = "<h3>welcome to bookstore</h3><h5>Discover the best books online with us</h5>"
                });
        }
    }
}
