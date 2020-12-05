using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Final_Project.DAL;
using Final_Project.Models;
namespace Final_Project.Seeding
{
    public class SeedShowings
    {
        public static void SeedAllShowings(AppDbContext db)
        {
            List<MovieShowing> AllShowings = new List<MovieShowing>();

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 4, 09, 05, 00),
				EndTime = new DateTime(2020, 12, 4, 10, 52, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Footloose"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 5, 09, 05, 00),
				EndTime = new DateTime(2020, 12, 5, 10, 52, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Footloose"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 6, 09, 05, 00),
				EndTime = new DateTime(2020, 12, 6, 10, 52, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Footloose"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 7, 09, 05, 00),
				EndTime = new DateTime(2020, 12, 7, 10, 52, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Footloose"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 8, 09, 05, 00),
				EndTime = new DateTime(2020, 12, 8, 10, 52, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Footloose"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 9, 09, 05, 00),
				EndTime = new DateTime(2020, 12, 9, 10, 52, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Footloose"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 10, 09, 05, 00),
				EndTime = new DateTime(2020, 12, 10, 10, 52, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Footloose"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 4, 11, 30, 00),
				EndTime = new DateTime(2020, 12, 4, 13, 24, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "WarGames"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 5, 11, 30, 00),
				EndTime = new DateTime(2020, 12, 5, 13, 24, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "WarGames"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 6, 11, 30, 00),
				EndTime = new DateTime(2020, 12, 6, 13, 24, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "WarGames"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 7, 11, 30, 00),
				EndTime = new DateTime(2020, 12, 7, 13, 24, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "WarGames"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 8, 11, 30, 00),
				EndTime = new DateTime(2020, 12, 8, 13, 24, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "WarGames"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 9, 11, 30, 00),
				EndTime = new DateTime(2020, 12, 9, 13, 24, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "WarGames"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 10, 11, 30, 00),
				EndTime = new DateTime(2020, 12, 10, 13, 24, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "WarGames"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 4, 14, 00, 00),
				EndTime = new DateTime(2020, 12, 4, 15, 29, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Office Space"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 5, 14, 00, 00),
				EndTime = new DateTime(2020, 12, 5, 15, 29, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Office Space"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 6, 14, 00, 00),
				EndTime = new DateTime(2020, 12, 6, 15, 29, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Office Space"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 7, 14, 00, 00),
				EndTime = new DateTime(2020, 12, 7, 15, 29, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Office Space"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 8, 14, 00, 00),
				EndTime = new DateTime(2020, 12, 8, 15, 29, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Office Space"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 9, 14, 00, 00),
				EndTime = new DateTime(2020, 12, 9, 15, 29, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Office Space"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 10, 14, 00, 00),
				EndTime = new DateTime(2020, 12, 10, 15, 29, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Office Space"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 4, 15, 55, 00),
				EndTime = new DateTime(2020, 12, 4, 17, 55, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Diamonds are Forever"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 5, 15, 55, 00),
				EndTime = new DateTime(2020, 12, 5, 17, 55, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Diamonds are Forever"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 6, 15, 55, 00),
				EndTime = new DateTime(2020, 12, 6, 17, 55, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Diamonds are Forever"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 7, 15, 55, 00),
				EndTime = new DateTime(2020, 12, 7, 17, 55, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Diamonds are Forever"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 8, 15, 55, 00),
				EndTime = new DateTime(2020, 12, 8, 17, 55, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Diamonds are Forever"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 9, 15, 55, 00),
				EndTime = new DateTime(2020, 12, 9, 17, 55, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Diamonds are Forever"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 10, 15, 55, 00),
				EndTime = new DateTime(2020, 12, 10, 17, 55, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Diamonds are Forever"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 4, 18, 40, 00),
				EndTime = new DateTime(2020, 12, 4, 21, 12, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "West Side Story"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 5, 18, 40, 00),
				EndTime = new DateTime(2020, 12, 5, 21, 12, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "West Side Story"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 6, 18, 40, 00),
				EndTime = new DateTime(2020, 12, 6, 21, 12, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "West Side Story"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 7, 18, 40, 00),
				EndTime = new DateTime(2020, 12, 7, 21, 12, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "West Side Story"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 8, 18, 40, 00),
				EndTime = new DateTime(2020, 12, 8, 21, 12, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "West Side Story"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 9, 18, 40, 00),
				EndTime = new DateTime(2020, 12, 9, 21, 12, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "West Side Story"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 10, 18, 40, 00),
				EndTime = new DateTime(2020, 12, 10, 21, 12, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "West Side Story"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 4, 21, 37, 00),
				EndTime = new DateTime(2020, 12, 4, 23, 59, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Forrest Gump"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 5, 21, 37, 00),
				EndTime = new DateTime(2020, 12, 5, 23, 59, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Forrest Gump"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 6, 21, 37, 00),
				EndTime = new DateTime(2020, 12, 6, 23, 59, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Forrest Gump"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 7, 21, 37, 00),
				EndTime = new DateTime(2020, 12, 7, 23, 59, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Forrest Gump"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 8, 21, 37, 00),
				EndTime = new DateTime(2020, 12, 8, 23, 59, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Forrest Gump"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 9, 21, 37, 00),
				EndTime = new DateTime(2020, 12, 9, 23, 59, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Forrest Gump"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.One,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 10, 21, 37, 00),
				EndTime = new DateTime(2020, 12, 10, 23, 59, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Forrest Gump"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 4, 09, 00, 00),
				EndTime = new DateTime(2020, 12, 4, 10, 21, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Toy Story"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 5, 09, 00, 00),
				EndTime = new DateTime(2020, 12, 5, 10, 21, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Toy Story"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 6, 09, 00, 00),
				EndTime = new DateTime(2020, 12, 6, 10, 21, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Toy Story"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 7, 09, 00, 00),
				EndTime = new DateTime(2020, 12, 7, 10, 21, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Toy Story"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 8, 09, 00, 00),
				EndTime = new DateTime(2020, 12, 8, 10, 21, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Toy Story"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 9, 09, 00, 00),
				EndTime = new DateTime(2020, 12, 9, 10, 21, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Toy Story"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 10, 09, 00, 00),
				EndTime = new DateTime(2020, 12, 10, 10, 21, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Toy Story"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 4, 10, 50, 00),
				EndTime = new DateTime(2020, 12, 4, 12, 32, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Dazed and Confused"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 5, 10, 50, 00),
				EndTime = new DateTime(2020, 12, 5, 12, 32, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Dazed and Confused"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 6, 10, 50, 00),
				EndTime = new DateTime(2020, 12, 6, 12, 32, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Dazed and Confused"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 7, 10, 50, 00),
				EndTime = new DateTime(2020, 12, 7, 12, 32, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Dazed and Confused"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 8, 10, 50, 00),
				EndTime = new DateTime(2020, 12, 8, 12, 32, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Dazed and Confused"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 9, 10, 50, 00),
				EndTime = new DateTime(2020, 12, 9, 12, 32, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Dazed and Confused"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 10, 10, 50, 00),
				EndTime = new DateTime(2020, 12, 10, 12, 32, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Dazed and Confused"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 4, 13, 00, 00),
				EndTime = new DateTime(2020, 12, 4, 14, 40, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Lego Movie"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 5, 13, 00, 00),
				EndTime = new DateTime(2020, 12, 5, 14, 40, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Lego Movie"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 6, 13, 00, 00),
				EndTime = new DateTime(2020, 12, 6, 14, 40, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Lego Movie"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 7, 13, 00, 00),
				EndTime = new DateTime(2020, 12, 7, 14, 40, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Lego Movie"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 8, 13, 00, 00),
				EndTime = new DateTime(2020, 12, 8, 14, 40, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Lego Movie"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 9, 13, 00, 00),
				EndTime = new DateTime(2020, 12, 9, 14, 40, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Lego Movie"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 10, 13, 00, 00),
				EndTime = new DateTime(2020, 12, 10, 14, 40, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Lego Movie"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 4, 15, 20, 00),
				EndTime = new DateTime(2020, 12, 4, 16, 58, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Princess Bride"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 5, 15, 20, 00),
				EndTime = new DateTime(2020, 12, 5, 16, 58, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Princess Bride"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 6, 15, 20, 00),
				EndTime = new DateTime(2020, 12, 6, 16, 58, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Princess Bride"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 7, 15, 20, 00),
				EndTime = new DateTime(2020, 12, 7, 16, 58, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Princess Bride"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 8, 15, 20, 00),
				EndTime = new DateTime(2020, 12, 8, 16, 58, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Princess Bride"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 9, 15, 20, 00),
				EndTime = new DateTime(2020, 12, 9, 16, 58, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Princess Bride"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 10, 15, 20, 00),
				EndTime = new DateTime(2020, 12, 10, 16, 58, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Princess Bride"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 4, 17, 25, 00),
				EndTime = new DateTime(2020, 12, 4, 19, 05, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Finding Nemo"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 5, 17, 25, 00),
				EndTime = new DateTime(2020, 12, 5, 19, 05, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Finding Nemo"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 6, 17, 25, 00),
				EndTime = new DateTime(2020, 12, 6, 19, 05, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Finding Nemo"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 7, 17, 25, 00),
				EndTime = new DateTime(2020, 12, 7, 19, 05, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Finding Nemo"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 8, 17, 25, 00),
				EndTime = new DateTime(2020, 12, 8, 19, 05, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Finding Nemo"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 9, 17, 25, 00),
				EndTime = new DateTime(2020, 12, 9, 19, 05, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Finding Nemo"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 10, 17, 25, 00),
				EndTime = new DateTime(2020, 12, 10, 19, 05, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Finding Nemo"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 4, 19, 30, 00),
				EndTime = new DateTime(2020, 12, 4, 22, 11, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Harry Potter and the Chamber of Secrets"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 5, 19, 30, 00),
				EndTime = new DateTime(2020, 12, 5, 22, 11, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Harry Potter and the Chamber of Secrets"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 6, 19, 30, 00),
				EndTime = new DateTime(2020, 12, 6, 22, 11, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Harry Potter and the Chamber of Secrets"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 7, 19, 30, 00),
				EndTime = new DateTime(2020, 12, 7, 22, 11, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Harry Potter and the Chamber of Secrets"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 8, 19, 30, 00),
				EndTime = new DateTime(2020, 12, 8, 22, 11, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Harry Potter and the Chamber of Secrets"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 9, 19, 30, 00),
				EndTime = new DateTime(2020, 12, 9, 22, 11, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Harry Potter and the Chamber of Secrets"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 10, 19, 30, 00),
				EndTime = new DateTime(2020, 12, 10, 22, 11, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "Harry Potter and the Chamber of Secrets"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 4, 22, 40, 00),
				EndTime = new DateTime(2020, 12, 4, 23, 49, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Land Before Time"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 5, 22, 40, 00),
				EndTime = new DateTime(2020, 12, 5, 23, 49, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Land Before Time"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 6, 22, 40, 00),
				EndTime = new DateTime(2020, 12, 6, 23, 49, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Land Before Time"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 7, 22, 40, 00),
				EndTime = new DateTime(2020, 12, 7, 23, 49, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Land Before Time"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 8, 22, 40, 00),
				EndTime = new DateTime(2020, 12, 8, 23, 49, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Land Before Time"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 9, 22, 40, 00),
				EndTime = new DateTime(2020, 12, 9, 23, 49, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Land Before Time"),
			});

			AllShowings.Add(new MovieShowing
			{
				TheaterSelection = TheaterType.Two,
				ShowingStatus = PublishStatus.Published,
				IsPublished = true,
				StartTime = new DateTime(2020, 12, 10, 22, 40, 00),
				EndTime = new DateTime(2020, 12, 10, 23, 49, 00),
				Movie = db.Movies.FirstOrDefault(c => c.Title == "The Land Before Time"),
			});

			//create a counter and flag to help with debugging
			int intMovieShowingID = 0;
			String strMovieName = "Start";

			//we are now going to add the data to the database
			//this could cause errors, so we need to put this code
			//into a Try/Catch block
			try
			{
				//loop through each of the categories
				foreach (MovieShowing seedShowing in AllShowings)
				{
					//updates the counters to get info on where the problem is
					intMovieShowingID = seedShowing.MovieShowingID;
					strMovieName = seedShowing.Movie.Title;

					//try to find the category in the database
					MovieShowing dbMovieShowing = db.MovieShowings.FirstOrDefault(c => c.MovieShowingID == seedShowing.MovieShowingID);

					//if the category isn't in the database, dbCategory will be null
					if (dbMovieShowing == null)
					{
						//add the Showing to the database
						db.MovieShowings.Add(seedShowing);
						db.SaveChanges();
					}
					else //the record is in the database
					{
						//update all the fields
						//this isn't really needed for category because it only has one field
						//but you will need it to re-set seeded data with more fields
						dbMovieShowing.TheaterSelection = seedShowing.TheaterSelection;
						dbMovieShowing.IsPublished = seedShowing.IsPublished;
						dbMovieShowing.Movie = seedShowing.Movie;
						dbMovieShowing.StartTime = seedShowing.StartTime;
						dbMovieShowing.EndTime = seedShowing.EndTime;
						//you would add other fields here
						db.SaveChanges();
					}

				}
			}
			catch (Exception ex)  //something about adding to the database caused a problem
			{
				//create a new instance of the string builder to make building out 
				//our message neater - we don't want a REALLY long line of code for this
				//so we break it up into several lines
				StringBuilder msg = new StringBuilder();

				msg.Append("There was an error adding the ");
				msg.Append(strMovieName);
				msg.Append(" genre (MovieShowingID = ");
				msg.Append(intMovieShowingID);
				msg.Append(")");

				//have this method throw the exception to the calling method
				//this code wraps the exception from the database with the 
				//custom message we built above.  The original error from the
				//database becomes the InnerException
				throw new Exception(msg.ToString(), ex);
			}

		}
    }
}
