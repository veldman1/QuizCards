using System;
using System.Collections.Generic;

namespace QuizCardsModel
{
    public class QuizItem
    {
        public QuizItem(int index, string question, int correctAnswerIndex, List<string> choices)
        {
            Index = index;
            Question = question;
            CorrectAnswerIndex = correctAnswerIndex;
            Choices = choices;
        }

        public int Index { get; }

        public string Question { get; }

        public int CorrectAnswerIndex { get; }

        public List<String> Choices { get; }
    }
}