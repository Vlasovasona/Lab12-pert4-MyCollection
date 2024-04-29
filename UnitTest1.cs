using Лаба12_часть3_Дерево;
using Library_10;

namespace Tree_Tests
{
    [TestClass]
    public class UnitTest1
    {
        //блок ошибок
        [TestMethod]
        public void Print_EmptyTree_Exception() //метод проверки возникновения ошибки при попытке печати пустого дерева
        {
            MyTree<Instrument> tree = new MyTree<Instrument>();
            Assert.ThrowsException<Exception>(() =>
            {
                tree.ShowTree();
            });
        }

        [TestMethod]
        public void Print_FindMinElement_InEmptyTree_Exception() //метод проверки возникновения ошибки при попытке найти минимальный элемент в пустой дереве
        {
            MyTree<Instrument> tree = new MyTree<Instrument>();
            Assert.ThrowsException<Exception>(() =>
            {
                tree.FindMin();
            });
        }
        //блок ошибок закончен

        //проверка конструктора дерева
        [TestMethod]
        public void CreateTree_CheckCount_Test() //проверка работы Count
        {
            MyTree<Instrument> tree = new MyTree<Instrument>(5);
            Assert.AreEqual(5, tree.Count);
        }

        //проверка класса Point_Tree

        [TestMethod]
        public void CreatePointTree_CheckNull_Test() //проверка 
        {
            Point_Tree<Instrument> p = new Point_Tree<Instrument>();
            Assert.IsNull(p.Left);
        }

        [TestMethod]
        public void Point_ToString_NotNull_Test()
        {
            HandTool tool = new HandTool();
            Point_Tree<Library_10.Instrument> p = new Point_Tree<Library_10.Instrument>(tool);
            Assert.AreEqual(p.ToString(), tool.ToString());
        }

        [TestMethod]
        public void Point_ToString_Null_Test()
        {
            HandTool tool = new HandTool();
            tool = null;
            Point_Tree<Library_10.Instrument> p = new Point_Tree<Library_10.Instrument>(tool);
            Assert.AreEqual(p.ToString(), "");
        }

        //проеврка методов MyTree
        [TestMethod]
        public void TestFindMinRecursive_WhenRootIsNull_ShouldThrowException()
        {
            MyTree<Instrument> tree = new MyTree<Instrument>();

            Assert.ThrowsException<Exception>(() => tree.FindMin(), "Дерево пусто. Не существует минимального значения.");
        }

        [TestMethod]
        public void TestFindMin_OneNodeTree()
        {
            MyTree<Instrument> tree = new MyTree<Instrument>(1);
            Instrument tool1 = new Instrument();
            tool1 = tree.GetRoot();
            Instrument min = tree.FindMin();
            Assert.AreEqual(tool1, min);
        }

        [TestMethod]
        public void TestFindMin_FullTree()
        {
            MyTree<Instrument> tree = new MyTree<Instrument>(1);
            Instrument tool1 = new Instrument("E", 10);
            Instrument tool2 = new Instrument("Er", 5);
            Instrument tool3 = new Instrument("E", 20);
            Instrument tool4 = new Instrument("Er", 3);
            Instrument tool5 = new Instrument("E", 15);
            tree.AddPoint(tool1);
            tree.AddPoint(tool2);
            tree.AddPoint(tool3);
            tree.AddPoint(tool4);
            tree.AddPoint(tool5);

            Instrument min = tree.FindMin();

            Assert.AreEqual(tool4, min);
        }

        [TestMethod]
        public void Test_TransformOneNodeTree()
        {
            MyTree<Instrument> tree = new MyTree<Instrument>(1);
            Instrument tool1 = new Instrument();
            Instrument tool2 = new Instrument();

            tool1 = tree.GetRoot();
            tree.TransformToSearchTree();
            tool2 = tree.GetRoot();
            Assert.AreEqual(tool1, tool2);
        }

        [TestMethod]
        public void Test_TransformFullNodeTree()
        {
            MyTree<Instrument> tree = new MyTree<Instrument>(1);
            Instrument tool1 = new Instrument("E", 182);
            Instrument tool2 = new Instrument("Er", 145);
            Instrument tool3 = new Instrument("E", 198);
            Instrument tool4 = new Instrument("Er", 185);
            Instrument tool5 = new Instrument("E", 170);

            tree.AddPoint(tool1);
            tree.AddPoint(tool2);
            tree.AddPoint(tool3);
            tree.AddPoint(tool4);
            tree.AddPoint(tool5);

            tree.TransformToSearchTree();
            tree.BalanceSearchTree();
            Instrument toolRoot = tree.GetRoot();
            Assert.AreEqual(tool5, toolRoot);
        }

        //тестирование методов удаления
        [TestMethod]
        public void Test_DeleteFullNodeTree() //проверка удаления элемента из заполненного дерева
        {
            MyTree<Instrument> tree = new MyTree<Instrument>(1);
            MyTree<Instrument> treeNew = new MyTree<Instrument>(1);
            Instrument toolFromTree1 = tree.GetRoot();
            Instrument toolFromTreeNew = treeNew.GetRoot();

            Instrument tool1 = new Instrument("E", 177);
            Instrument tool2 = new Instrument("Er", 152);
            Instrument tool3 = new Instrument("E", 124);
            Instrument tool4 = new Instrument("Er", 108);
            Instrument tool5 = new Instrument("E", 170);
            Instrument tool6 = new Instrument("E", 171);

            tree.AddPoint(tool1);
            tree.AddPoint(tool2);
            tree.AddPoint(tool3);
            tree.AddPoint(tool4);
            tree.AddPoint(tool5);
            tree.AddPoint(tool6);
            tree.RemoveElement(toolFromTree1);

            treeNew = tree.TransformToSearchTree();
            treeNew.BalanceSearchTree();
            treeNew.RemoveElement(tool3);
            treeNew.RemoveElement(toolFromTreeNew);
            treeNew.BalanceSearchTree();
            Instrument toolRoot = treeNew.GetRoot();
            Assert.AreEqual(tool5, toolRoot);
        }

        [TestMethod]
        public void Test_DeleteFullNodeTree_2() //проверка удаления элемента из заполненного дерева
        {
            MyTree<Instrument> tree = new MyTree<Instrument>(1);
            MyTree<Instrument> treeNew = new MyTree<Instrument>(1);
            Instrument toolFromTree1 = tree.GetRoot();
            Instrument toolFromTreeNew = treeNew.GetRoot();

            Instrument tool1 = new Instrument("E", 177);
            Instrument tool2 = new Instrument("Er", 152);
            Instrument tool3 = new Instrument("E", 124);
            Instrument tool4 = new Instrument("Er", 108);

            tree.AddPoint(tool1);
            tree.AddPoint(tool2);
            tree.AddPoint(tool3);
            tree.AddPoint(tool4);
            tree.RemoveElement(toolFromTree1);

            treeNew = tree.TransformToSearchTree();
            treeNew.BalanceSearchTree();
            treeNew.RemoveElement(tool3);
            treeNew.RemoveElement(toolFromTreeNew);
            treeNew.BalanceSearchTree();
            Instrument toolRoot = treeNew.GetRoot();
            Assert.AreEqual(tool2, toolRoot);
        }

        [TestMethod]
        public void Test_DeleteFullNodeTree_ExtremeObjectRight() //проверка удаления крайнего рпвого элемента из дерева
        {
            MyTree<Instrument> tree = new MyTree<Instrument>(1);
            MyTree<Instrument> treeNew = new MyTree<Instrument>(1);
            Instrument toolFromTree1 = tree.GetRoot();
            Instrument toolFromTreeNew = treeNew.GetRoot();


            Instrument tool1 = new Instrument("E", 177);
            Instrument tool2 = new Instrument("Er", 152);
            Instrument tool3 = new Instrument("E", 124);
            Instrument tool4 = new Instrument("Er", 108);
            tree.AddPoint(tool1);
            tree.AddPoint(tool2);
            tree.AddPoint(tool3);
            tree.AddPoint(tool4);
            tree.RemoveElement(toolFromTree1);

            treeNew = tree.TransformToSearchTree();
            treeNew.BalanceSearchTree();
            treeNew.RemoveElement(tool4);
            treeNew.RemoveElement(toolFromTreeNew);
            treeNew.BalanceSearchTree();
            Instrument toolRoot = treeNew.GetRoot();
            Assert.AreEqual(tool2, toolRoot);
        }

        [TestMethod]
        public void Test_DeleteFullNodeTree_ExtremeObjectLeft() //Проверка удаления крайнего элемента из дерева
        {
            MyTree<Instrument> tree = new MyTree<Instrument>(1);
            Instrument toolFromTree1 = tree.GetRoot();


            Instrument tool1 = new Instrument("E", 177);
            Instrument tool2 = new Instrument("Er", 152);
            tree.AddPoint(tool1);
            tree.AddPoint(tool2);
            tree.RemoveElement(toolFromTree1);
            tree.RemoveElement(tool1);

            Instrument toolRoot = tree.GetRoot();
            Assert.AreEqual(tool2, toolRoot);
        }

        [TestMethod]
        public void TestFindMaxValue_OneElement() //проверка работы метода FindMaxValue
        {
            // Создаем экземпляр дерева и добавляем в него элементы
            MyTree<Instrument> tree = new MyTree<Instrument>(1);
            Instrument tool1 = new Instrument("E", 177);
            Instrument t = tree.GetRoot();

            tree.AddPoint(tool1);

            tree.RemoveElement(t);
            Instrument toolRoot = tree.GetRoot();
            Point_Tree<Instrument> root = new Point_Tree<Instrument>(toolRoot);
            // Находим максимальное значение в дереве
            Point_Tree<Instrument> maxNode = tree.FindMaxValue(root);

            // Проверяем, что максимальное значение соответствует ожидаемому
            Assert.AreEqual(tool1, maxNode.Data);
        }

        [TestMethod]
        public void TestDeleteOneElementTree() //проверка удаления единственного элемента дерева 
        {
            MyTree<Instrument> tree = new MyTree<Instrument>(1);
            Instrument t = tree.GetRoot();

            tree.RemoveElement(t);
            Assert.AreEqual(0, tree.Count);
        }
    }
}