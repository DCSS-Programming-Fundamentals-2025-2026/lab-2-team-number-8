using System;

namespace lab_2_team_number_8
{
    class Program
    {
        static DeliveryManager manager = new DeliveryManager();
        static DeliveryCollection collection = new DeliveryCollection();

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
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

                try
                {
                    switch (choice)
                    {
                        case "1": AddDelivery(); break;
                        case "2": EditDelivery(); break;
                        case "3": ShowAll(); break;
                        case "4": ShowPending(); break;
                        case "5": MarkAsDone(); break;
                        case "6": CancelDelivery(); break;
                        case "7": SortByPriority(); break;
                        case "8": ShowSummary(); break;
                        case "9": ResetDay(); break;
                        case "10": LoadIntoCollection(); break;
                        case "11": AddToEnd(); break;
                        case "12": AddOnPosition(); break;
                        case "13": RemoveFromPosition(); break;
                        case "14": ShowDeliveryFromPosition(); break;
                        case "15": ShowCollectionDeliveryCount(); break;
                        case "16": ShowAllDelieveresInCollection(); break;
                        case "17": SortCollectionById(); ShowAllDelieveresInCollection(); break;
                        case "18": SortCollectionByPriority(); ShowAllDelieveresInCollection(); break;
                        case "19": ShowStats(); break;
                        case "0": exit = true; break;
                        default: Console.WriteLine("Помилка, спробуйте ще раз."); break;
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Притримуйтесь формату ввода.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Виникла помилка");
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
            Console.WriteLine("\n----------- ДОДАТКОВЕ МЕНЮ ДЛЯ РОБОТИ З КОЛЕКЦІЄЮ-----------");
            Console.WriteLine("10. Завантажити в колекцію доставки з менеджеру доставок (колекції буде повністю оновлено)");
            Console.WriteLine("11. Додати нову доставку в кінець");
            Console.WriteLine("12. Додати нову доставку на конкретну позицію");
            Console.WriteLine("13. Видалення доставки з конкретної позиції");
            Console.WriteLine("14. Переглянути доставку з конкретної позиції");
            Console.WriteLine("15. Переглянути кількість доставок");          
            Console.WriteLine("16. Переглянути список всіх доставок");          
            Console.WriteLine("17. Відсортувати доставки за ID");          
            Console.WriteLine("18. Відсортувати доставки за пріоритетом");          
            Console.WriteLine("19. Переглянути статистику доставок");          
        }

        static void ShowAllDelieveresInCollection()
        {
            var it = collection.GetEnumerator();
            while (it.MoveNext())
            {
                if (it.Current == null)
                {
                    break;
                }
                Console.WriteLine(it.Current);
            }
        }

        static void LoadIntoCollection()
        {
            Delivery[] temp = new Delivery[200];
            int deliveryCount = manager.CopyAll(temp);
            collection = new DeliveryCollection(temp, deliveryCount);
        }

        static void AddToEnd()
        {
            Console.Write("ID доставки: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Назва замовлення: ");
            string title = Console.ReadLine();
            Console.Write("Адреса: ");
            string address = Console.ReadLine();
            Console.Write("Пріоритет (1=Low, 2=Medium, 3=High): ");
            int priority = int.Parse(Console.ReadLine());
            if (priority < 1 || priority > 3)
            {
                throw new FormatException();
            }

            Delivery newDelivery = new Delivery(id, title, address, (PriorityLevel)priority);

            if (collection.Add(newDelivery))
            {
                Console.WriteLine($"Доставку #{id} додано.");
            }                
            else
            {
                Console.WriteLine("Не вдалося додати доставку.");
            }
        }

        static void AddOnPosition()
        {
            Console.Write("ID доставки: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Назва замовлення: ");
            string title = Console.ReadLine();
            Console.Write("Адреса: ");
            string address = Console.ReadLine();
            Console.Write("Пріоритет (1=Low, 2=Medium, 3=High): ");
            int priority = int.Parse(Console.ReadLine());
            if (priority < 1 || priority > 3)
            {
                throw new FormatException();
            }
            Console.Write("Позиція: ");
            int index = int.Parse(Console.ReadLine());

            Delivery newDelivery = new Delivery(id, title, address, (PriorityLevel)priority);

            if (collection.SetAt(index, newDelivery))
            {
                Console.WriteLine($"Доставку #{id} додано.");
            }
            else
            {
                Console.WriteLine("Не вдалося додати доставку.");
            }
        }

        static void RemoveFromPosition()
        {
            Console.Write("Позиція: ");
            int index = int.Parse(Console.ReadLine());

            if (collection.RemoveAt(index))
            {
                Console.WriteLine($"Доставку видалено.");
            }
            else
            {
                Console.WriteLine("Не вдалося знайти доставку.");
            }
        }

        static void ShowDeliveryFromPosition()
        {
            Console.Write("Позиція: ");
            int index = int.Parse(Console.ReadLine());

            Delivery tempDelivery = collection.GetAt(index);

            if (tempDelivery != null)
            {
                Console.WriteLine(tempDelivery);
            }
            else
            {
                Console.WriteLine("Не вдалося знайти доставку.");
            }
        }

        static void ShowCollectionDeliveryCount()
        {
            Console.WriteLine($"Всього доставок: {collection.Count()}");
        }

        static void SortCollectionById()
        {
            Array.Sort(collection.Deliveries, 0, collection.DeliveryCount);
        }

        static void SortCollectionByPriority()
        {
            Array.Sort(collection.Deliveries, 0, collection.DeliveryCount, new XComparer());
        }

        static void ShowStats()
        {
            int pendingCount = 0;
            int deliveredCount = 0;
            int cancelledCount = 0;
            
            foreach(Delivery delivery in collection)
            {
                if (delivery == null)
                {
                    break;
                }

                if (delivery.Status == Status.Pending)
                {
                    pendingCount++;
                }
                else if (delivery.Status == Status.Delivered)
                {
                    deliveredCount++;
                }
                else
                {
                    cancelledCount++;
                }
            }

            Console.WriteLine($"Очікується: {pendingCount}");
            Console.WriteLine($"Доставлено: {deliveredCount}");
            Console.WriteLine($"Скасовано: {cancelledCount}");
            Console.WriteLine($"Всього доставок: {collection.DeliveryCount}");
        }

        static void AddDelivery()
        {
            Console.Write("Замовлення: ");
            string title = Console.ReadLine();
            Console.Write("Адреса: ");
            string address = Console.ReadLine();
            Console.Write("Пріоритет (1=Low, 2=Medium, 3=High): ");
            
            if (int.TryParse(Console.ReadLine(), out int p))
            {
                if (manager.Add(title, address, (PriorityLevel)p, out int id))
                    Console.WriteLine($"Доставка #{id} додана.");
                else
                    Console.WriteLine("Помилка додавання.");
            }
            else Console.WriteLine("Невірний формат.");
        }

        static void EditDelivery()
        {
            Console.Write("ID доставки: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Нова назва: ");
                string title = Console.ReadLine();
                Console.Write("Нова адреса: ");
                string address = Console.ReadLine();
                Console.Write("Новий пріоритет (1-3): ");
                
                if (int.TryParse(Console.ReadLine(), out int p))
                {
                    if (manager.Edit(id, title, address, (PriorityLevel)p)) Console.WriteLine("Оновлено.");
                    else Console.WriteLine("Помилка: не знайдено.");
                }
            }
        }

        static void MarkAsDone()
        {
            Console.Write("ID доставки: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (manager.MarkDone(id)) Console.WriteLine("Позначено як виконану.");
                else Console.WriteLine("Помилка: не знайдено.");
            }
        }

        static void CancelDelivery()
        {
            Console.Write("ID доставки: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (manager.Cancel(id)) Console.WriteLine("Скасовано.");
                else Console.WriteLine("Помилка: не знайдено.");
            }
        }

        static void ShowAll()
        {
            Delivery[] temp = new Delivery[200];
            int count = manager.CopyAll(temp);
            Console.WriteLine($"\nЗнайдено: {count}");
            for (int i = 0; i < count; i++) Console.WriteLine(temp[i]);
        }

        static void ShowPending()
        {
            Delivery[] temp = new Delivery[200];
            int count = manager.CopyPending(temp);
            Console.WriteLine($"\nОчікують: {count}");
            for (int i = 0; i < count; i++) Console.WriteLine(temp[i]);
        }

        static void SortByPriority()
        {
            manager.SortByPriority();
            Console.WriteLine("Відсортовано.");
        }

        static void ShowSummary()
        {
            manager.Summary(out int total, out int done, out int pending, out int cancelled);
            Console.WriteLine($"Всього: {total}, Виконано: {done}, Очікують: {pending}, Скасовано: {cancelled}");
        }

        static void ResetDay()
        {
            manager.ResetDay(false);
            Console.WriteLine("День скинуто.");
        }
    }
}