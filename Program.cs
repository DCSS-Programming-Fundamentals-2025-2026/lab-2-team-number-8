using System;
using DeliveryRoutePlanner.Core;

namespace DeliveryRoutePlanner
{
    class Program
    {
        static DeliveryManager manager = new DeliveryManager();
        static void Main()
        {
            Run();
        }
        static void Run()
        {
            bool exit = false;
            while (!exit)
            {
                ShowMenu();
                Console.Write("\nОберіть пункт: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddDelivery();
                        break;
                    case "2":
                        EditDelivery();
                        break;
                    case "3":
                        ShowAll();
                        break;
                    case "4":
                        ShowPending();
                        break;
                    case "5":
                        MarkAsDone();
                        break;
                    case "6":
                        CancelDelivery();
                        break;
                    case "7":
                        SortByPriority();
                        break;
                    case "8":
                        ShowSummary();
                        break;
                    case "9":
                        ResetDay();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default: Console.WriteLine("помилка, спробуйте переобрати"); break;
                }
            }
        }
        static void ShowMenu()
        {
            Console.WriteLine("\n----------- МЕНЮ -----------");
            Console.WriteLine("1. Додати доставку");
            Console.WriteLine("2. Редагувати");
            Console.WriteLine("3. Список усіх");
            Console.WriteLine("4. Список невиконаних");
            Console.WriteLine("5. Позначити доставку виконаною");
            Console.WriteLine("6. Скасувати доставку");
            Console.WriteLine("7. Впорядкувати за пріоритетом");
            Console.WriteLine("8. Підсумок дня");
            Console.WriteLine("9. Скинути день");
            Console.WriteLine("0. Вихід");
        }
        static void AddDelivery()
        {
            Console.Write("Замовлення: ");
            string title = Console.ReadLine();
            Console.Write("Адреса: ");
            string address = Console.ReadLine();
            Console.Write("Пріоритет (1=низька, 2=середня, 3=висока): ");
            int p = int.Parse(Console.ReadLine());
            if (manager.Add(title, address, (PriorityLevel)p, out int id))
                Console.WriteLine($"Доставка #{id} додана.");
            else
                Console.WriteLine("помилка, не вдалося додати доставку.");
        }
        static void EditDelivery()
        {
            Console.Write("ID доставки: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Нова назва: ");
            string title = Console.ReadLine();
            Console.Write("Нова адреса: ");
            string address = Console.ReadLine();
            Console.Write("Новий пріоритет (1-3): ");
            int p = int.Parse(Console.ReadLine());
            if (manager.Edit(id, title, address, (PriorityLevel)p))
                Console.WriteLine("Оновлено.");
            else
                Console.WriteLine("помилка, не знайдено доставку.");
        }

        static void MarkAsDone()
        {
            Console.Write("ID доставки: ");
            int id = int.Parse(Console.ReadLine());
            if (manager.MarkDone(id))
                Console.WriteLine("Позначено як виконану.");
            else
                Console.WriteLine("помилка, не знайдено доставку.");
        }
        static void CancelDelivery()
        {
            Console.Write("ID доставки: ");
            int id = int.Parse(Console.ReadLine());
            if (manager.Cancel(id))
                Console.WriteLine("Скасовано.");
            else
                Console.WriteLine("помилка, не знайдено доставку.");
        }
        static void ShowAll()
        {
            Delivery[] temp = new Delivery[200];
            int count = manager.CopyAll(temp);
            for (int i = 0; i < count; i++)
                Console.WriteLine(temp[i]);
        }
        static void ShowPending()
        {
            Delivery[] temp = new Delivery[200];
            int count = manager.CopyPending(temp);
            for (int i = 0; i < count; i++)
                Console.WriteLine(temp[i]);
        }
        static void SortByPriority()
        {
            manager.SortByPriority();
            Console.WriteLine("Відсортовано за пріоритетом.");
        }
        static void ShowSummary()
        {
            manager.Summary(out int total, out int done, out int pending, out int cancelled);
            Console.WriteLine($"Всього: {total}, Виконано: {done}, Очікують: {pending}, Скасовано: {cancelled}");
        }
        static void ResetDay()
        {
            manager.ResetDay(resetIds: false);
            Console.WriteLine("День скинуто.");
        }
    }
}