using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuizCardsModel
{
    public class QuizItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public QuizItem(int index, string question, int correctAnswerIndex, List<string> choices)
        {
            _index = index;
            _question = question;
            CorrectAnswerIndex = correctAnswerIndex;
            Choices = choices;
        }

        private int _index;

        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
                NotifyPropertyChanged();
            }
        }

        private string _question;
        public string Question
        {
            get
            {
                return _question;
            }
            set
            {
                _question = value;
                NotifyPropertyChanged();
            }
        }

        public int CorrectAnswerIndex { get; }

        public List<String> Choices { get; }

        public string CorrectAnswer {
            get {
                return Choices[CorrectAnswerIndex - 1];
            }
        }
    }
}