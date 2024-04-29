using Лаба12_часть2;
using Лаба12_часть4;
using Library_10;
using System.Collections.Generic;
namespace MyCollection_Test
{
    [TestClass]
    public class Test_MyCollection
    {
        //тестирование конструкторов 
        [TestMethod]
        public void Test_ConstuctorWithoutParams() //тест проверка на создание пустого объекта MyCollection
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>();
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void Test_Constructor_CollectionT() //проверка создания хеш-таблицы с помощью переноса элементов из сформировавнного массива
        {
            Instrument[] list = new Instrument[5]; //создаем массив, содержащий объекты типа Instrument и заполняем его случайным способом
            for (int i = 0; i < list.Length; i++)
            {
                Instrument tool = new Instrument();
                tool.RandomInit();
                list[i] = tool;
            }

            MyCollection<Instrument> collection = new MyCollection<Instrument>(list); //создаем коллекцию на основе созданного массива
            Assert.AreEqual(collection.Count, list.Length); //проверяем чтобы обе коллекции были одинаковые по длине
            Assert.IsTrue(collection.Contains(list[2])); //проверяем чтобы какой-нибудь элемент из массива содержался в новой коллекции. Должно вернуть true
        }

        [TestMethod]
        public void Test_Constructor_Length() //проверка конструктора, формаирующего коллекцию по ее длине
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>(5); //создаем коллекцию с помощью ввода ее длины
            Assert.AreEqual(collection.Count, 5); //проверяем чтобы коллекция содержала 5 элементов
        }
        //тестирование конструкторов завершено

        //тестирование нумератора
        [TestMethod]
        public void GetEnumerator_WhenCollectionHasItems_ShouldEnumerateAllItems() //проверка нумератора для заполненной коллекции
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>(1); //создаем коллекцию и заполняем ее элементами
            Instrument tool1 = new Instrument("Q", 1);
            Instrument tool2 = new Instrument("W", 12);
            Instrument tool3 = new Instrument("E", 123);
            PointHash<Instrument> firstElement = collection.GetFirstValue();

            collection.Add(tool1);
            collection.Add(tool2);
            collection.Add(tool3);
            collection.Remove(firstElement.Data);

            Instrument[] result = new Instrument[3];
            int index = 0;
            foreach (Instrument item in collection)
            {
                result[index] = item;
                index++;
            }
            CollectionAssert.AreEqual(new Instrument[] { tool1, tool2, tool3 }, result) ;
        }

        [TestMethod]
        public void GetEnumerator_CollectionHasRemovedElement() //проверка нумератора для коллекции, из которой был удален элемент
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>(1); //создаем коллекцию и заполняем ее элементами
            Instrument tool1 = new Instrument("Q", 1);
            Instrument tool2 = new Instrument("W", 12);
            Instrument tool3 = new Instrument("E", 123);
            PointHash<Instrument> firstElement = collection.GetFirstValue();

            collection.Add(tool1);
            collection.Add(tool2);
            collection.Add(tool3);

            collection.Remove(tool2);
            collection.Remove(firstElement.Data);

            Instrument[] result = new Instrument[2];
            int index = 0;
            foreach (Instrument item in collection)
            {
                result[index] = item;
                index++;
            }

            CollectionAssert.AreEqual(new Instrument[] { tool1, tool3 }, result);
        }
        //тестирование нумератора завершено

        //тестирование ICollection
        [TestMethod]
        public void ICollection_CopyTo() //проверка метода copyTo
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>(5); //создаем коллекцию и заполняем ее элементами
            Instrument[] list = new Instrument[5];
            collection.CopyTo(list, 0);
            PointHash<Instrument> value = collection.GetFirstValue();
            Assert.AreEqual(value.Data, list[0]);
        }

        [TestMethod]
        public void ICollection_Count() //проверка Count
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>(5); //создаем коллекцию и заполняем ее элементами
            Instrument[] list = new Instrument[5];
            collection.CopyTo(list, 0);
            Assert.AreEqual(collection.Count, list.Length);
        }
        //тестирование ICollection

        //блок Exception 
        [TestMethod]
        public void ICollection_CopyTo_ExceptionIndexOutsideOfListLength() //проверка исключения при вводе некорректного индекса
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>(5); //создаем коллекцию и заполняем ее элементами
            Instrument[] list = new Instrument[5];
            Assert.ThrowsException<Exception>(() =>
            {
                collection.CopyTo(list, 6);
            });
        }

        [TestMethod]
        public void ICollection_CopyTo_ExceptionNotEnoughListLength() //проверка исключения при попытке скопировать элементы в массив при нехватке места для элементов в массиве
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>(5); //создаем коллекцию и заполняем ее элементами
            Instrument[] list = new Instrument[5];
            Assert.ThrowsException<Exception>(() =>
            {
                collection.CopyTo(list, 2);
            });
        }

    }
}
