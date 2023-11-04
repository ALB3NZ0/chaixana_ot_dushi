using chaixanaa;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Добро пожаловать в Чайхану!");

        while (true)
        {
            
            Console.WriteLine("Выберите манты:");





            //Tsay_xochetsa menu = new Tsay_xochetsa();
            //menu.minStrelochka = 0;
            //menu.maxStrelochka = 4;
            //int pos = menu.Show();


            Tsay_xochetsa menu = new Tsay_xochetsa();
            menu.minStrelochka = 0;
            menu.maxStrelochka = 4;
            int pos = menu.Show();

            // Какие национальные манты
            var shapes = new List<MenuItem>
            {
                new MenuItem("Азербайджанские", 200),
                new MenuItem("Аварские", 150),
                new MenuItem("Грузинские", 120)
            };
            var shape = SelectMenuItem(shapes);

            // Размер порции
            var sizes = new List<MenuItem>
            {
                new MenuItem("Маленький", 200),
                new MenuItem("Средний", 300),
                new MenuItem("Большой", 400)
            };
            var size = SelectMenuItem(sizes);

            // с каким мясом манты
            var flavors = new List<MenuItem>
            {
                new MenuItem("Баранина", 50),
                new MenuItem("Говядина", 40),
                new MenuItem("Куриная", 45)
            };
            var flavor = SelectMenuItem(flavors);

            // Количество манты
            var quantity = EnterQuantity();

            // Дополнительные опции манты
            var glaze = SelectOption("Соусы", new List<MenuItem>
            {
                new MenuItem("Майонез", 20),
                new MenuItem("Сметана", 15),
                new MenuItem("Кефир с чесноком", 18)
            });

            var decor = SelectOption("Хлеб", new List<MenuItem>
            {
                new MenuItem("Тандырь", 10),
                new MenuItem("Хлеб", 12),
                new MenuItem("Черный хлеб", 8)
            });

            // Рассчитываем суммарную цену
            var totalPrice = shape.Price + size.Price + flavor.Price + glaze.Price + decor.Price;
            var totalOrder = new Order(shape.Description, size.Description, flavor.Description, quantity, glaze.Description, decor.Description, totalPrice);

            // Выводим итоговый заказ
            Console.WriteLine("Итоговый заказ:");
            Console.WriteLine(totalOrder);
            Console.WriteLine();

            // Сохраняем заказ в файл
            SaveOrder(totalOrder);

            // Запрашиваем повторный заказ
            Console.WriteLine("Хотите сделать еще один заказ? (Да/Нет)");
            var answer = Console.ReadLine();
            if (answer?.ToLower() != "да")
                break;

            Console.Clear();
        }

        Console.WriteLine("Спасибо, что воспользовались нашим сервисом! До свидания!");
        Console.ReadKey();
    }




    static MenuItem SelectMenuItem(List<MenuItem> menuItems)
    {
        while (true)
        {
            Console.WriteLine("Выберите пункт меню:");
            for (int i = 0; i < menuItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuItems[i].Description} - {menuItems[i].Price} руб.");
            }

            var selectedIndex = ReadIntFromConsole();
            if (selectedIndex >= 1 && selectedIndex <= menuItems.Count)
            {
                return menuItems[selectedIndex - 1];
            }
            else
            {
                Console.WriteLine("Неправильный выбор, пожалуйста, повторите.");
            }
        }
    }

    static int ReadIntFromConsole()
    {
        int number;
        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("Неправильный ввод, пожалуйста, повторите:");
        }
        return number;
    }

    static int EnterQuantity()
    {
        Console.WriteLine("Введите количество манты:");
        return ReadIntFromConsole();
    }

    static MenuItem SelectOption(string optionName, List<MenuItem> menuItems)
    {
        while (true)
        {
            Console.WriteLine($"Выберите {optionName}:");
            for (int i = 0; i < menuItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuItems[i].Description} - {menuItems[i].Price} руб.");
            }

            var selectedIndex = ReadIntFromConsole();
            if (selectedIndex >= 1 && selectedIndex <= menuItems.Count)
            {
                return menuItems[selectedIndex - 1];
            }
            else
            {
                Console.WriteLine("Неправильный выбор, пожалуйста, повторите.");
            }
        }
    }

    static void SaveOrder(Order order)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        Console.WriteLine(path + "\\orders.txt");
        string filePath = "orders.txt";
        string orderInfo = $"{order.Shape},{order.Size},{order.Flavor},{order.Quantity},{order.Sauces},{order.Bread},{order.TotalPrice}";

        using (StreamWriter sw = File.AppendText(filePath))
        {
            sw.WriteLine(orderInfo);
        }

        Console.WriteLine("Заказ сохранен в файл.");
    }
}
    


class MenuItem
{
    public string Description { get; set; }
    public int Price { get; set; }

    public MenuItem(string description, int price)
    {
        Description = description;
        Price = price;
    }
}

class Order
{
    public string Shape { get; set; }
    public string Size { get; set; }
    public string Flavor { get; set; }
    public int Quantity { get; set; }
    public string Sauces { get; set; }
    public string Bread { get; set; }
    public int TotalPrice { get; set; }

    public Order(string shape, string size, string flavor, int quantity, string sauces, string bread, int totalPrice)
    {
        Shape = shape;
        Size = size;
        Flavor = flavor;
        Quantity = quantity;
        Sauces = sauces;
        Bread = bread;
        TotalPrice = totalPrice;
    }

    public override string ToString()
    {
        return $"Форма: {Shape}\n" +
               $"Размер: {Size}\n" +
               $"Вкус: {Flavor}\n" +
               $"Количество: {Quantity}\n" +
               $"Соусы: {Sauces}\n" +
               $"Хлеб: {Bread}\n" +
               $"Итоговая цена: {TotalPrice} руб.";
    }
}
   