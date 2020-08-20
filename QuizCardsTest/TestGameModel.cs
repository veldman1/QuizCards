using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizCardsModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizCardsTest
{
    [TestClass]
    public class TestGameModel
    {
        private GameModel model;

        [TestInitialize]
        public void Init()
        {
            model = new GameModel("./QuizTestFiles/Functional1.txt"); ;
        }

        /// <summary>
        /// Play the game with three answer sets, testing state, score, and current item contents
        /// </summary>
        /// <param name="submitAnswerOne"></param>
        /// <param name="submitAnswerTwo"></param>
        /// <param name="numCorrect"></param>
        [TestMethod]
        [DataTestMethod]
        [DataRow(1, 2, 2)]
        [DataRow(1, 1, 1)]
        [DataRow(2, 1, 0)]

        public void Test_PlayShortQuiz(int submitAnswerOne, int submitAnswerTwo, int numCorrect)
        {
            // Question 1
            Assert.AreEqual(GameState.Quiz, model.State);
            Assert.AreEqual("Please select \"Answer One\"", model.CurrentItem.Question.Question);
            Assert.AreEqual("Answer One", model.CurrentItem.Question.Choices[0]);
            Assert.AreEqual("Answer Two", model.CurrentItem.Question.Choices[1]);
            Assert.AreEqual(false, model.CurrentItem.Answered);
            Assert.AreEqual(false, model.CurrentItem.Correct);
            model.SubmitAnswer(submitAnswerOne);

            // Question 2
            Assert.AreEqual(GameState.Quiz, model.State);
            Assert.AreEqual("Please select \"Selection Two\"", model.CurrentItem.Question.Question);
            Assert.AreEqual("Selection One", model.CurrentItem.Question.Choices[0]);
            Assert.AreEqual("Selection Two", model.CurrentItem.Question.Choices[1]);
            Assert.AreEqual(false, model.CurrentItem.Answered);
            Assert.AreEqual(false, model.CurrentItem.Correct);
            model.SubmitAnswer(submitAnswerTwo);

            // Score
            Assert.AreEqual(GameState.Score, model.State);
            Assert.AreEqual(numCorrect, model.CorrectCount());
        }
    }
}
