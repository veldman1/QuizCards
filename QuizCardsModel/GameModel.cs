using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace QuizCardsModel
{
    public enum GameState { Quiz, Feedback, Score }

    public class GameModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public List<ScoredQuizItem> QuizItems { get; private set; }
        public int QuizItemListIndex { get; private set; }

        public ScoredQuizItem CurrentItem { get; private set; }

        public GameState _state { get; set; }
        public GameState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                NotifyPropertyChanged();
            }
        }

        public GameModel(string filePath)
        {
            State = GameState.Quiz;

            // Read questions from file
            var simpleQuizItems = FileReader.ReadQuizFile(filePath);

            // Convert the item to an ordere list of ScoredQuizItems
            QuizItems = simpleQuizItems.Select(item => new ScoredQuizItem { Question = item }).ToList();
            QuizItems = QuizItems.OrderBy(x => x.Question.Index).ToList();

            CurrentItem = QuizItems.First();

            NotifyPropertyChanged("TotalQuestionCount");
        }

        public bool SubmitAnswer(int answerInt)
        {
            var correct = CurrentItem.Question.CorrectAnswerIndex == answerInt;

            CurrentItem.Correct = correct;
            CurrentItem.Answered = true;

            State = GameState.Feedback;

            return correct;
        }

        public void AdvanceCard()
        {
            QuizItemListIndex++;

            if (QuizItemListIndex < QuizItems.Count)
            {
                CurrentItem = QuizItems[QuizItemListIndex];
                State = GameState.Quiz;
            }
            else
            {
                // The last question was answered
                State = GameState.Score;
            }
            NotifyPropertyChanged("CorrectCount");
        }

        public int CorrectCount
        {
            get
            {
                return QuizItems.Where(x => x.Answered && x.Correct).Count();
            }
        }

        public int TotalQuestionCount
        {
            get
            {
                return QuizItems.Count();
            }
        }
    }
}
