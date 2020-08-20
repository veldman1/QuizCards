using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QuizCardsModel
{
    public class ScoredQuizItem : INotifyPropertyChanged
    {
        public QuizItem Question { get; set; }

        public bool Correct { get; set; }

        public bool Answered { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
