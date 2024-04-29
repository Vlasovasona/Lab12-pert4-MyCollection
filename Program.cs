using System.Collections;
using System.Diagnostics.Metrics;
using Library_10;
using Лаба12_часть2;
using System.Collections.Generic;
namespace Лаба12_часть4
{
    internal class Program
    {
        static sbyte InputSbyteNumber(string msg = "Введите число")  //функция для проверки введенного числа на тип sbyte
        {
            Console.WriteLine(msg); //вывод сообщения msg
            bool isConvert; //объявление переменной, отвечающей за проверку на корректность
            sbyte number; //переменная, которой будет присвоено корректно введенное число
            do
            {
                isConvert = sbyte.TryParse(Console.ReadLine(), out number); //проверка на принадлежность типу sbyte
                if (!isConvert || number <= 0)
                {
                    Console.WriteLine("Неправильно введено число. Возможно вы ввели слишком длинное число. Попробуйте заново"); //в случае провала, вывод сообщения о некорректном вводе числа
                    isConvert = false;
                }
            } while (!isConvert); //повторение цикла до тех пор, пока пользователь не введет корректное число
            return number; //ф-ция принимает значение введенного корректного числа
        }

        static int InputIntNumber(string msg = "Введите число")  //функция для проверки введенного числа на тип sbyte
        {
            Console.WriteLine(msg); //вывод сообщения msg
            bool isConvert; //объявление переменной, отвечающей за проверку на корректность
            int number; //переменная, которой будет присвоено корректно введенное число
            do
            {
                isConvert = int.TryParse(Console.ReadLine(), out number); //проверка на принадлежность типу sbyte
                if (!isConvert || number <= 0)
                {
                    Console.WriteLine("Неправильно введено число. Возможно вы ввели слишком длинное число. Попробуйте заново"); //в случае провала, вывод сообщения о некорректном вводе числа
                    isConvert = false;
                }
            } while (!isConvert); //повторение цикла до тех пор, пока пользователь не введет корректное число
            return number; //ф-ция принимает значение введенного корректного числа
        }

        static HandTool[] MakeInstrumentList()
        {
            int size = InputIntNumber("введите ёмкость массива");
            HandTool[] list = new HandTool[size];
            for (int i = 0; i < list.Length; i++)
            {
                HandTool inst = new HandTool();
                inst.RandomInit();
                list[i] = inst;
            }
            return list;
        }

        static void DeleteElement(MyCollection<HandTool> table)
        {
            HandTool tool = new HandTool();
            Console.WriteLine("Введите элемент, который нужно найти и удалить");
            tool.Init();
            if (table.Contains(tool))
            {
                table.RemoveData(tool);
                if (table.Count == 0) Console.WriteLine("В ходе удаления была получена пустая таблица");
            }
            else
                throw new Exception("Элемент не найден в таблице. Удаление невозможно");
            Console.WriteLine("Удаление выполнено");
        }

        static void Main(string[] args)
        {
            MyCollection<HandTool> collection = new MyCollection<HandTool>();
            HandTool[] list = new HandTool[0];
            sbyte answer1; //объявление переменных, которые отвечают за выбранный пункт меню
            do
            {
                Console.WriteLine("1. Сформировать хеш-таблицу с помощью ввода длины");
                Console.WriteLine("2. Сформировать хеш-таблицу, копируя элементы массива T[]");
                Console.WriteLine("3. Вывести хеш-таблицу");
                Console.WriteLine("4. Добавить элемент в коллекцию");
                Console.WriteLine("5. Удалить элемент из коллекции");
                Console.WriteLine("6. Скопировать элеметы коллекции в массив");
                Console.WriteLine("7. Посмотреть количество элементов коллекции");
                Console.WriteLine("8. Удалить коллекцию (очистить память)");
                Console.WriteLine("9. Проверить, содержится ли элемент в коллекции");
                Console.WriteLine("10. Завершить работу программы");

                answer1 = InputSbyteNumber();

                switch (answer1)
                {
                    case 1: //формирование коллекции ввод длины
                        {
                            try
                            {
                                int size = InputIntNumber("Введите количество элементов хеш-таблицы");
                                if (size <= 0) throw new Exception("хеш-таблица не может быть нулевой или отрицательной длины");
                                collection = new MyCollection<HandTool>(size);
                                Console.WriteLine("Хеш-таблица сформирована");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 2: //формирование коллекции на основе массива
                        {
                            try
                            {
                                list = MakeInstrumentList();
                                collection = new MyCollection<HandTool>(list);
                                Console.WriteLine("Хеш-таблица сформирована");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 3: //вывод на экран
                        {
                            try
                            {
                                if (collection.Count <= 0 || collection == null) throw new Exception("Таблица пустая или не создана");
                                Console.WriteLine("Вывод хеш-таблицы");
                                collection.Print();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 4: //добавление элемента в коллекцию
                        {
                            try
                            {
                                if (collection.Count <= 0 || collection == null) throw new Exception("Таблица пустая или не создана");
                                HandTool tool = new HandTool();
                                tool.Init();
                                collection.Add(tool);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 5: //удаление элемента из коллекции
                        {
                            try
                            {
                                if (collection.Count <= 0 || collection == null) throw new Exception("Таблица пустая или не создана");
                                DeleteElement(collection);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 6: //копия элементов в массив
                        {
                            try
                            {
                                if (collection.Count <= 0 || collection == null) throw new Exception("Таблица пустая или не создана");
                                list = MakeInstrumentList();
                                int index = InputIntNumber("Введите индекс, начиная с которого будем добавлять элементы в массив");
                                collection.CopyTo(list, index-1);
                                for (int i = 0; i < list.Length; i++)
                                {
                                    Console.WriteLine(list[i].ToString());
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 7: //просмотр кол-ва элементов в коллекции
                        {
                            Console.WriteLine(collection.Count);
                            break;
                        }
                    case 8: //удалить коллекцию
                        {
                            collection.Clear();
                            Console.WriteLine("Таблица удалена");
                            break;
                        }
                    case 9: //проверить содержится ли элемент в коллекции
                        {
                            try
                            {
                                if (collection.Count <= 0 || collection == null) throw new Exception("Таблица пустая или не создана");
                                HandTool tool = new HandTool();
                                tool.Init();
                                Console.WriteLine(collection.Contains(tool));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 10: //завершение работы программы
                        {
                            Console.WriteLine("Демонстрация завершена");
                            break;
                        }
                    default: //неправильный ввод пункта меню
                        {
                            Console.WriteLine("Неправильно задан пункт меню");
                            break;
                        }
                }
            } while (answer1 != 10); //цикл повторяется пока пользователь не введет число 10
        }
    }
}


