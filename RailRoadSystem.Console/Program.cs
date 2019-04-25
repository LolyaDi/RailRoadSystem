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
        }

        public static void BuyTicket()
        {
            string userChoice, fullName;
            bool isParsed;

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
                System.Console.WriteLine("Go fuck yourself, please");
                return;
            }

            var ticket = new Ticket();

            int railwayCarriage, seat, cityNumber;
            DateTime dateTime;

            System.Console.WriteLine("Введите дату отправления:");
            userChoice = System.Console.ReadLine();

            isParsed = isParsed = DateTime.TryParse(userChoice, out dateTime);

            while (!isParsed)
            {
                System.Console.WriteLine("Неверный ввод! Введите еще раз: Например: 2009-05-08 14:40:52");
                userChoice = System.Console.ReadLine();
                isParsed = DateTime.TryParse(userChoice, out dateTime);
            }

            ticket.DepartureDate = dateTime;

            System.Console.WriteLine("Введите номер вагона:");
            userChoice = System.Console.ReadLine();

            isParsed = int.TryParse(userChoice, out railwayCarriage);

            while(!isParsed)
            {
                System.Console.WriteLine("Неверный ввод! Введите еще раз:");
                userChoice = System.Console.ReadLine();
                isParsed = int.TryParse(userChoice, out railwayCarriage);
            }

            ticket.RailwayCarriage = railwayCarriage;

            System.Console.WriteLine("Введите номер места:");
            userChoice = System.Console.ReadLine();

            isParsed = int.TryParse(userChoice, out seat);

            while (!isParsed)
            {
                System.Console.WriteLine("Неверный ввод! Введите еще раз:");
                userChoice = System.Console.ReadLine();
                isParsed = int.TryParse(userChoice, out seat);
            }

            ticket.Seat = seat;

            List<City> cities;
            int i = 0;

            using (var repository = new Repository())
            {
                var citiesData = repository.Select<City>();
                cities = new List<City>(citiesData);
            }

            System.Console.WriteLine("Введите номер города, куда направляется поезд:");
            userChoice = System.Console.ReadLine();

            isParsed = int.TryParse(userChoice, out cityNumber);

            i = 0;

            System.Console.WriteLine(string.Format("{0}) {1,-20}","№","Название"));
            foreach(var city in cities)
            {
                System.Console.WriteLine(string.Format("{0}) {1,-20}",++i,city.Name));
            }

            while (!isParsed || cityNumber > cities.Count || cityNumber <= 0)
            {
                System.Console.WriteLine("Неверный ввод! Введите еще раз:");
                userChoice = System.Console.ReadLine();
                isParsed = int.TryParse(userChoice, out cityNumber);
            }

            var cityTo = cities[cityNumber - 1];

            ticket.CityTo = cityTo;
            ticket.CityToId = cityTo.Id;

            System.Console.WriteLine("Введите номер города, откуда отправляется поезд:");
            userChoice = System.Console.ReadLine();

            isParsed = int.TryParse(userChoice, out cityNumber);

            i = 0;

            System.Console.WriteLine(string.Format("{0}) {1,-20}", "№", "Название"));
            foreach (var city in cities)
            {
                System.Console.WriteLine(string.Format("{0}) {1,-20}", ++i, city.Name));
            }

            while (!isParsed || cityNumber > cities.Count || cityNumber <= 0)
            {
                System.Console.WriteLine("Неверный ввод! Введите еще раз:");
                userChoice = System.Console.ReadLine();
                isParsed = int.TryParse(userChoice, out cityNumber);
            }

            var cityFrom = cities[cityNumber - 1];

            ticket.CityFrom = cityFrom;
            ticket.CityFromId = cityFrom.Id;

            user.Tickets.Add(ticket);

            using (var repository = new Repository())
            {
                repository.Update(user);
            }

            System.Console.WriteLine("Билет был куплен!");
        }

        public static List<TResult> ToList<TResult>(this IQueryable<TResult> source)
        {
            return new List<TResult>(source);
        }
    }
}
