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
        public void GetEnumerator_WhenCollectionHasItems_ShouldEnumerateAllItems() //нумератор для коллекции 
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
            CollectionAssert.AreEqual(new Instrument[] { tool1, tool2, tool3 }, result);
        }

        [TestMethod]
        public void GetEnumerator_CollectionHasRemovedElement() //нумератор для коллекции с удаленным элементом
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
        public void ICollection_CopyTo() // проверка CopyTo 
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>(5); //создаем коллекцию и заполняем ее элементами
            Instrument[] list = new Instrument[5];
            collection.CopyTo(list, 0);
            PointHash<Instrument> value = collection.GetFirstValue();
            Assert.AreEqual(value.Data, list[0]);
        }

        [TestMethod]
        public void ICollection_Count() //проверка счетчика элементов
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>(5); //создаем коллекцию и заполняем ее элементами
            Instrument[] list = new Instrument[5];
            collection.CopyTo(list, 0);
            Assert.AreEqual(collection.Count, list.Length);
        }
        //тестирование ICollection

        //блок Exception 
        [TestMethod]
        public void ICollection_CopyTo_ExceptionIndexOutsideOfListLength() //проверка исключения при некорректном вводе индекса при попытке скопирровать значения коллекции в массив
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>(5); //создаем коллекцию и заполняем ее элементами
            Instrument[] list = new Instrument[5];
            Assert.ThrowsException<Exception>(() =>
            {
                collection.CopyTo(list, 6);
            });
        }

        [TestMethod]
        public void ICollection_CopyTo_ExceptionNotEnoughListLength() //ошибка, когда не хватает места для всех элементов в массиве
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>(5); //создаем коллекцию и заполняем ее элементами
            Instrument[] list = new Instrument[5];
            Assert.ThrowsException<Exception>(() =>
            {
                collection.CopyTo(list, 2);
            });
        }

        [TestMethod]
        public void TestClear() //проверка очистки памяти
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>(6);
            collection.Clear();
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void TestAdd() //проврека добавления элемента
        {
            // Arrange
            MyCollection<Instrument> collection = new MyCollection<Instrument>(1);
            Instrument t = new Instrument();
            collection.Add(t);
            Assert.IsTrue(collection.Contains(t));
            
        }

        //ТЕСТЫ ИЗ ВТОРОЙ ЧАСТИ ДЛЯ ПОКРЫТИЯ КЛАССОВ ХЕШ-ТАБЛИЦЫ
        //блок Exception
        [TestMethod]
        public void Test_CreateTable_Exception() //тестирование ошибки при попытке формирования пустой таблицы
        {
            Assert.ThrowsException<Exception>(() =>
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(-1);
            });
        }

        [TestMethod]
        public void Test_AddExistingElement_Exception() //тестирование ошибки при попытке формирования пустой таблицы
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
            Instrument tool = new Instrument("q", 1);
            table.AddPoint(tool);
            Assert.ThrowsException<Exception>(() =>
            {
                table.AddPoint(tool);
            });
        }

        [TestMethod]
        public void Test_PrintNullTable_Exception() //тестирование ошибки при попытке печати пустой таблицы
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>();
            Assert.ThrowsException<Exception>(() =>
            {
                table.Print();
            });
        }//блок Exception закончен

        [TestMethod]
        public void TestCreateTable() //тестирование конструктора для создания хеш-таблицы
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(5);
            Assert.AreEqual(table.Capacity, 5);
        }

        //тестривание AddPoint 
        [TestMethod]
        public void TestAddPointToHashTable() //тестирование добавления элемента в таблицу
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(5);
            HandTool tool = new HandTool();
            table.AddPoint(tool);
            Assert.IsTrue(table.Contains(tool));
        }

        [TestMethod]
        public void TestAddCount() //тестирование увеличения Count после добавления элемента в таблицу
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(5);
            HandTool tool = new HandTool();
            table.AddPoint(tool);
            Assert.AreEqual(6, table.Count);
        }

        //тестиование удаления элемента из таблицы
        [TestMethod]
        public void TestRemovePointFromHashTableTrue() //тестирование добавления удаления существующего элемента из таблицы
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
            HandTool tool = new HandTool();
            table.AddPoint(tool);
            table.RemoveData(tool);
            Assert.IsFalse(table.Contains(tool));
        }

        [TestMethod]
        public void TestRemovePointFromHashTable_False() //тестирование удаления несуществующего элемента из таблицы
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
            HandTool tool = new HandTool();
            table.AddPoint(tool);
            table.RemoveData(tool);
            Assert.IsFalse(table.Contains(tool));
        }

        [TestMethod]
        public void TestRemovePointFromHashTable_OutOfKey_False() //тестирование удаления несуществующего элемента из таблицы
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
            Instrument tool = new Instrument("Бензопила дружба нового поколения", 9999);
            Assert.IsFalse(table.RemoveData(tool));
        }

        [TestMethod]
        public void TestRemovePoint_FromBeginingOfTableTable() //тестирование удаления первого в цепочке элемента из таблицы
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
            Instrument tool2 = new Instrument("Перфоратор", 98);
            Instrument tool3 = new Instrument("Штангенциркуль", 85);
            Instrument tool4 = new Instrument("Микрометр", 41);
            Instrument tool5 = new Instrument("RRR", 1234);
            Instrument tool6 = new Instrument("RRR", 1235);

            table.AddPoint(tool2);
            table.AddPoint(tool3);
            table.AddPoint(tool4);
            table.AddPoint(tool5);
            table.AddPoint(tool6);

            PointHash<Instrument> tool = new PointHash<Instrument>();
            PointHash<Instrument> pointHash = table.GetFirstValue();
            tool = pointHash;
            table.RemoveData(tool.Data);
            Assert.IsFalse(table.Contains(tool.Data));
        }


        //тестирование метода Contains
        [TestMethod]
        public void TestContainsPointTrue() //метод Contains когда элемент есть в таблице
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
            HandTool tool = new HandTool();
            table.AddPoint(tool);
            Assert.IsTrue(table.Contains(tool));
        }

        [TestMethod]
        public void TestContainsPointFalse() //когда элемента нет в таблице
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
            HandTool tool = new HandTool();
            Assert.IsFalse(table.Contains(tool));
        }

        //тестирование ToString для PointHash
        [TestMethod]
        public void TestToStringPoint() //тестирование ToString для класса узла
        {
            HandTool tool = new HandTool();
            PointHash<Library_10.Instrument> p = new PointHash<Library_10.Instrument>(tool);
            Assert.AreEqual(p.ToString(), tool.ToString());
        }

        [TestMethod]
        public void TestConstructWhithoutParamNext() //конструктор узла без параметров, Next = null
        {
            PointHash<Instrument> p = new PointHash<Instrument>();
            Assert.IsNull(p.Next);
        }

        [TestMethod]
        public void TestConstructWhithoutParamPred() //конструктор узла без параметров, Pred = null
        {
            PointHash<Instrument> p = new PointHash<Instrument>();
            Assert.IsNull(p.Pred);
        }

        //тестирование методов ToString и GetHashCode для класса PointHash
        [TestMethod]
        public void ToString_WhenDataIsNull_ReturnEmptyString() //конструктор без параметров метод ToString
        {
            PointHash<Instrument> point = new PointHash<Library_10.Instrument>();
            string result = point.ToString();
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void ToString_WhenDataIsNotNull_ReturnDataToString()
        {
            Library_10.Instrument tool = new Instrument();
            tool.RandomInit();
            PointHash<Instrument> point = new PointHash<Instrument>(tool);
            string result = point.ToString();
            Assert.AreEqual(tool.ToString(), result);
        }

        [TestMethod]
        public void GetHashCode_WhenDataIsNull_ReturnZero() //тестирование GetHashCode для узла, созданного с помощью конструктора без параметров
        {
            PointHash<Instrument> point = new PointHash<Library_10.Instrument>();
            int result = point.GetHashCode();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetHashCode_WhenDataIsNotNull_ReturnDataHashCode() //тестиование GetHashCode для заполненного узла
        {
            Library_10.Instrument tool = new Instrument();
            tool.RandomInit();
            PointHash<Instrument> point = new PointHash<Library_10.Instrument>(tool);
            int result = point.GetHashCode();
            Assert.AreEqual(tool.GetHashCode(), result);
        }
    }
}

