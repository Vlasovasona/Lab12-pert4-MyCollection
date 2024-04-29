﻿using Library_10;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Лаба12_часть2
{
    public class MyHashTable<T> where T: IInit, ICloneable, new()
    {
        public PointHash<T>?[] table; //можем присвоить нулевую ссылку

        public sbyte count = 0; //счетчик элементов в списке

        public int Capacity => table.Length;
        public sbyte Count => count;

        public MyHashTable() { }

        //конструктор с параметром
        public MyHashTable(int length)
        {
            if (length <= 0) throw new Exception("Размер не может быть нулевым или отрицательным");
            table = new PointHash<T>[length];
            for (int i = 0; i < table.Length; i++)
            {
                T value = PointHash<T>.MakeRandomItem();
                AddPoint(value);
            }
        }

        public MyHashTable(T[] c)
        {
            if (c.Length == 0) throw new Exception("Коллекция не может быть нулевой длины");
            table = new PointHash<T>[c.Length];
            for (int i = 0; i < c.Length; i++)
            {
                T value = (T)c[i].Clone();
                AddPoint(value);
            }
        }

        public void Print() //метод для вывода хеш-таблицы
        {
            if (table == null || count == 0) 
                throw new Exception("empty table");
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] == null) Console.WriteLine(i + " : ");
                else
                {
                    Console.Write(i + " : ");
                    PointHash<T> p = table[i];
                    while (p != null)
                    {
                        Console.Write(p.ToString() + "\t");
                        p = p.Next;
                    }
                    Console.WriteLine();
                }
            }
        }

        public void AddPoint(T data) //ф-ция добавления элемента в таблицу
        {
            if (Contains(data)) throw new Exception("Такой элемент уже есть в таблице");
            else
            {
                count++;
                int index = GetIndex(data);
                //позиция пустая
                if (table[index] == null)
                    table[index] = new PointHash<T>(data);
                else
                {
                    PointHash<T>? current = table[index];

                    while (current.Next != null)
                    {
                        if (current.Equals(data)) //элементы не должны дублироваться
                            return;
                        current = current.Next;
                    }
                    current.Next = new PointHash<T>(data); //созданиенового элемента, его адрес присваиваем в следующий от текущего
                    current.Next.Pred = current; //теперь current.Next - новый элемент, связываем его с предыдущим
                }
            }
        }

        public bool Contains(T data) //функция поиска элемента в таблице
        {
            int index = GetIndex(data);
            if (table == null) throw new Exception("empty table");
            if (table[index] == null) return false; //цепочка пустая, элемента нет
            if (table[index].Data.Equals(data)) return true; //попали на нужный элемент
            else
            {
                PointHash<T>? current = table[index]; //идем по цепочке
                while (current != null)
                {
                    if (current.Data.Equals(data)) 
                        return true; //сравниваем текущее поле с полем, которое мы получили
                    current = current.Next;
                }
            }
            return false;
        }

        public bool RemoveData(T data) //функция удаления элемента из таблицы
        {
            if (count == 0) throw new Exception("Таблица пустая, удаление невозможно");
            PointHash<T>? current; 
            int index = GetIndex(data); //генерируем ключ
            if (table[index] == null) return false; //если по вычисленному ключу ничего не найдено, возвращаем false
            if (table[index].Data.Equals(data)) //проверяем на сходство элементы, которые находятся под данным ключом
            {
                if (table[index].Next == null) //один элемент в цепочке
                    table[index] = null; //просто обнуляем ссылки, сборщик мусора все уберет
                else //если в цепочке не один элемент
                {
                    table[index] = table[index].Next; 
                    table[index].Pred = null;
                }
                count--;
                return true;        
            }
            else //если несколько элементов в цепочке и нам нужно удалить элемент из середины или из конца
            {
                current = table[index];
                while (current != null)
                {
                    if (current.Data.Equals(data))
                    {
                        PointHash<T>? pred = current.Pred; //левый от удаляемого
                        PointHash<T>? next = current.Next; //правый от удаляемого
                        pred.Next = next; //ссылка с левого на правый
                        current.Pred = null; //обнуление сслыки с текущего на левый
                        if (next != null) //проверка что следующий элемент существует
                            next.Pred = pred; //ссылку с крайнеко справа ставим на крайний от current слева
                        count--;
                        return true;
                    }
                    current = current.Next; //перезод к следующему элементу цепочки
                }
            }
            return false;
        }

        public PointHash<T> GetFirstValue()
        {
            PointHash<T> p = table[0];
            return p;
        }

        int GetIndex(T data) //получение ключа
        {
            return Math.Abs(data.GetHashCode()) % Capacity;
        }

        public void Clear()
        {
            table = null;
            GC.Collect();
        }
    }
}
