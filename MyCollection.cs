using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Лаба12_часть2;
using Library_10;
using System.Collections;

namespace Лаба12_часть4
{
    public class MyCollection<T>: MyHashTable<T>, IEnumerable<T>, ICollection<T> where T: IInit, ICloneable, new()
    {
        public MyCollection(): base() { } //конструктор для создания пустой коллекции
        public MyCollection(int size) : base(size) { } //конструктор для создания коллекции длины length

        public MyCollection(T[] c) : base(c) { } //создание коллекции на основе массива 

        public bool IsReadOnly => false;

        int ICollection<T>.Count => base.Count;

        public void Add(T item) //добавление в коллекцию
        {
            AddPoint(item);
        }

        public void Clear() //очистка памяти
        {
            base.Clear();
            count = 0;
            this.table = null;
        }

        public bool Contains(T item) //проверка имеется ли элемент в коллекции
        {
            return base.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex) //копия элементов коллекции в массив
        {
            if (array == null) throw new Exception("empty collection");
            if (arrayIndex < 0) throw new Exception("индекс не может быть отрицательным или равным нулю");
            if (arrayIndex > array.Length) throw new Exception("индекс не может превышать величину массива");
            if (array.Length - arrayIndex < count) throw new Exception("не хватает места для элементов коллекции");
            int index = arrayIndex;
            foreach (var element in this) //применили цикл foreach
            {
                T data = (T)element.Clone();
                array[index] = data;
                index++;
            }
        }

        public bool Remove(T item) //удаление
        {
            return base.RemoveData(item);
        }

        //НУМЕРАТОР
        public IEnumerator<T> GetEnumerator()           //обобщенный нумератор, наследует все от обобщенного
        {                                               //возвращает в Current T  
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null) 
                {
                    PointHash<T>? p = table[i];
                    while (p != null)
                    {
                        yield return p.Data; 
                        p = p.Next;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() //необобщенный нумератор, не используется
        {
            throw new NotImplementedException();
        }
        //НУМЕРАТОР
    }
}

