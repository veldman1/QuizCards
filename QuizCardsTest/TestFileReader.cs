using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizCardsModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizCardsTest
{
    [TestClass]
    public class TestFileReader
    {
        /// <summary>
        /// Tests a file in its expected format
        /// </summary>
        [TestMethod]
        public void Test_FunctionalFile1()
        {
            var quizActual = FileReader.ReadQuizFile("./QuizTestFiles/Functional1.txt");

            var quizExpected = new List<QuizItem>() {
                new QuizItem(index: 1,
                             question: "Please select \"Answer One\"",
                             correctAnswerIndex: 1,
                             choices: new List<string> { "Answer One", "Answer Two" }),
                new QuizItem(index: 2,
                             question: "Please select \"Selection Two\"",
                             correctAnswerIndex: 2,
                             choices: new List<string> { "Selection One", "Selection Two" }),
            };

            CompareQuizItemLists(quizActual, quizExpected);
        }

        /// <summary>
        /// Tests reading of a file with many new lines and special characters
        /// </summary>
        [TestMethod]
        public void Test_FunctionalFile2()
        {
            var quizActual = FileReader.ReadQuizFile("./QuizTestFiles/Functional2.txt");

            var quizExpected = new List<QuizItem>() {
                new QuizItem(index: 1,
                             question: "Please select \"Answer One\"",
                             correctAnswerIndex: 1,
                             choices: new List<string> { "Answer One", "Answer Two" }),
                new QuizItem(index: 2,
                             question: "Please select \"Selection Two\"",
                             correctAnswerIndex: 2,
                             choices: new List<string> { "Selection One", "Selection #2 !!" }),
            };

            CompareQuizItemLists(quizActual, quizExpected);
        }

        

        /// <summary>
        /// This test ensures several types of invalid files throw exceptions when reading.
        /// </summary>
        /// <param name="filename"></param>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        [DataTestMethod]
        [DataRow("Invalid1.txt")]
        [DataRow("Invalid2.txt")]
        [DataRow("Invalid3.txt")]
        public void Test_InvalidFiles(string filename)
        {
            var quizActual = FileReader.ReadQuizFile("./QuizTestFiles/" + filename);
        }

        /// <summary>
        /// Utility method to compare lists of quiz items
        /// </summary>
        /// <param name="quizActual"></param>
        /// <param name="quizExpected"></param>
        private static void CompareQuizItemLists(IList<QuizItem> quizActual, IList<QuizItem> quizExpected)
        {
            Assert.AreEqual(quizExpected.Count, quizActual.Count);
            for (int i = 0; i < quizExpected.Count; i++)
            {
                var expectedItem = quizExpected[i];
                var actualItem = quizActual[i];
                Assert.IsTrue(Enumerable.SequenceEqual(expectedItem.Choices, actualItem.Choices));
                Assert.AreEqual(expectedItem.Index, actualItem.Index);
                Assert.AreEqual(expectedItem.CorrectAnswerIndex, actualItem.CorrectAnswerIndex);
                Assert.AreEqual(expectedItem.Question, actualItem.Question);
            }
        }
    }
}
