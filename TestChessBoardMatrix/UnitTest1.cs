using ChessMatrix;

namespace TestChessBoardMatrix
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConstructor()
        {
            CBMatrix matrix1 = new CBMatrix(3);
            Assert.AreEqual(matrix1.GetSize(), 3);
            Assert.AreEqual(matrix1.GetNumberOfValues(), 5);
            CBMatrix matrix2 = new CBMatrix(4);
            Assert.AreEqual(matrix2.GetSize(), 4);
            Assert.AreEqual(matrix2.GetNumberOfValues(), 8);
            CBMatrix matrix3 = new CBMatrix();
            Assert.AreEqual(matrix3.GetSize(), 3);
            Assert.AreEqual(matrix3.GetNumberOfValues(), 5);
            List<int> values = new List<int>() { 16, 9, 8, 15, 6 };
            CBMatrix matrix4 = new CBMatrix(values);
            Assert.AreEqual(matrix4.GetSize(), 3);
            Assert.AreEqual(matrix4.GetNumberOfValues(), 5);
            try
            {
                List<int> values2 = new List<int>() { 16, 9, 8, 15, 6, 32 };
                CBMatrix matrix5 = new CBMatrix(values2);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is CBMatrix.InvalidVectorException);
            }
            CBMatrix matrix6 = new CBMatrix();
            CBMatrix matrix7 = new CBMatrix(matrix6);
            matrix6.SetElement(1, 1, 100);
            Assert.AreEqual(matrix7.GetSize(), 3);
            Assert.AreEqual(matrix7.GetNumberOfValues(), 5);
            Assert.AreNotEqual(matrix7.GetElement(1, 1), 100);
        }

        [TestMethod]
        public void TestGetElement()
        {
            CBMatrix matrix1 = new CBMatrix();
            Assert.AreEqual(matrix1.GetElement(0, 0), 1);
            Assert.AreEqual(matrix1.GetElement(2, 2), 5);
            Assert.AreEqual(matrix1.GetElement(1, 0), 0);
            try
            {
                Assert.AreEqual(matrix1.GetElement(5, 5), 5);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is CBMatrix.InvalidIndexException);
            }
            matrix1.SetElement(1, 1, 100);
            Assert.AreEqual(matrix1.GetElement(1, 1), 100);
        }

        [TestMethod]
        public void TestAdd()
        {
            CBMatrix matrix1 = new CBMatrix();
            CBMatrix matrix2 = new CBMatrix(3);
            CBMatrix matrix3 = new CBMatrix(5);
            CBMatrix sum = CBMatrix.Add(matrix1, matrix2);
            Assert.AreEqual(sum.GetElement(0, 0), 2);
            Assert.AreEqual(sum.GetElement(2, 2), 6);
            Assert.AreEqual(sum.GetElement(1, 0), 0);
            try
            {
                CBMatrix sum2 = CBMatrix.Add(matrix1, matrix3);
                Assert.Fail("No exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is CBMatrix.DimensionMismatchException);
            }
        }

        [TestMethod]
        public void TestMultiply()
        {
            CBMatrix matrix1 = new CBMatrix();
            CBMatrix matrix2 = new CBMatrix(3);
            CBMatrix matrix3 = new CBMatrix(5);
            CBMatrix product = CBMatrix.Multiply(matrix1, matrix2);
            Assert.AreEqual(product.GetElement(0, 0), 3);
            Assert.AreEqual(product.GetElement(2, 2), 9);
            Assert.AreEqual(product.GetElement(0, 1), 0);
            try
            {
                CBMatrix product2 = CBMatrix.Multiply(matrix1, matrix3);
                Assert.Fail("No exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is CBMatrix.DimensionMismatchException);
            }
        }

        [TestMethod]
        public void TestSetElement()
        {
            CBMatrix matrix1 = new CBMatrix();
            matrix1.SetElement(0, 0, 100);
            matrix1.SetElement(2, 2, 100);
            Assert.AreEqual(matrix1.GetElement(0, 0), 100);
            Assert.AreEqual(matrix1.GetElement(2, 2), 100);
            try
            {
                matrix1.SetElement(1, 0, 100);
                Assert.Fail("No exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is CBMatrix.ZeroCellException);
            }
            try
            {
                matrix1.SetElement(5, 5, 100);
                Assert.Fail("No exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is CBMatrix.InvalidIndexException);
            }
        }

        [TestMethod]
        public void TestExtremeSize()
        {
            CBMatrix matrix1 = new CBMatrix(10000);
            Assert.AreEqual(matrix1.GetElement(9999, 9999), matrix1.GetElement(9999, 9999));
            try
            {
                CBMatrix matrix2 = new CBMatrix(-1);
                Assert.Fail("No exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is CBMatrix.InvalidSizeException);
            }
            try
            {
                CBMatrix matrix3 = new CBMatrix(0);
                Assert.Fail("No exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is CBMatrix.InvalidSizeException);
            }
        }
    }
}