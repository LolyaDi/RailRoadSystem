using RailRoadSystem.DataAccess;
using RailRoadSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RailRoadSystem.Console
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string userChoice;

            System.Console.WriteLine("Хотите купить билет? y/n");
            userChoice = System.Console.ReadLine();

            switch (userChoice)
            {
                case "y":
                    BuyTicket();
                    break;
                case "n":
                    System.Console.WriteLine("Оке...");
                    break;
                default:
                    System.Console.WriteLine("Были введены некорректные символы!");
                    break;
            }

            System.Console.ReadLine();
        }

        public static void BuyTicket()
        {
            string fullName;

            System.Console.WriteLine("Введите свое ФИО:");
            fullName = System.Console.ReadLine();

            User user;

            using (var repository = new Repository())
            {
                var usersData = repository.Select<User>();
                user = new List<User>(usersData).Find(u => u.FullName == fullName);
            }

            if (user is null)
            {
                System.Console.WriteLine("Пользователя с таким ФИО не существует!");

                return;
            }

            string userChoice;
            bool isParsed;

            var ticket = new Ticket();

            System.Console.WriteLine("Введите дату отправления:");
            userChoice = System.Console.ReadLine();

            isParsed = isParsed = DateTime.TryParse(userChoice, out DateTime dateTime);

            while (!isParsed)
            {
                System.Console.WriteLine("Неверный ввод! Введите еще раз: Например: 2009-05-08 14:40:52");
                userChoice = System.Console.ReadLine();
                isParsed = DateTime.TryParse(userChoice, out dateTime);
            }

            ticket.DepartureDate = dateTime;

            System.Console.WriteLine("Введите номер вагона:");
            userChoice = System.Console.ReadLine();

            CheckData(userChoice, out int railwayCarriage);
            ticket.RailwayCarriage = railwayCarriage;

            System.Console.WriteLine("Введите номер места:");
            userChoice = System.Console.ReadLine();

            CheckData(userChoice, out int seat);
            ticket.Seat = seat;

            List<City> cities;

            using (var repository = new Repository())
            {
                var citiesData = repository.Select<City>();
                cities = new List<City>(citiesData);
            }

            GetAllCities(cities);

            System.Console.WriteLine("Введите номер города, куда направляется поезд:");
            userChoice = System.Console.ReadLine();

            CheckData(userChoice, out int cityNumber);

            if (cityNumber > cities.Count || cityNumber <= 0)
            {
                System.Console.WriteLine("Города под таким номером не существует!");
                return;
            }

            var cityTo = cities[cityNumber - 1];
            
            System.Console.WriteLine("Введите номер города, откуда отправляется поезд:");
            userChoice = System.Console.ReadLine();

            CheckData(userChoice, out cityNumber);

            if (cityNumber > cities.Count || cityNumber <= 0)
            {
                System.Console.WriteLine("Города под таким номером не существует!");
                return;
            }

            var cityFrom = cities[cityNumber - 1];
            
            using (var context = new DataContext())
            {
                context.Users.Attach(user);

                ticket.CityFrom = context.Cities.Attach(cityFrom);
                ticket.CityTo = context.Cities.Attach(cityTo);

                user.Tickets = new List<Ticket>
                {
                    ticket
                };

                context.SaveChanges();
            }

            System.Console.WriteLine("Поздравляем с покупкой!");
        }

        public static void CheckData(string dataString, out int number)
        {
            bool isParsed = int.TryParse(dataString, out number);

            while (!isParsed)
            {
                System.Console.WriteLine("Неверный ввод! Введите еще раз:");
                dataString = System.Console.ReadLine();
                isParsed = int.TryParse(dataString, out number);
            }
        }

        public static void GetAllCities(List<City> cities)
        {
            int i = 0;

            System.Console.WriteLine(string.Format("{0}) {1,-20}", "№", "Название"));
            foreach (var city in cities)
            {
                System.Console.WriteLine(string.Format("{0}) {1,-20}", ++i, city.Name));
            }
        }

        public static List<TResult> ToList<TResult>(this IQueryable<TResult> source)
        {
            return new List<TResult>(source);
        }
    }
}
