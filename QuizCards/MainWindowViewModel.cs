using QuizCardsModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows;

namespace QuizCards
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int _selectedAnswer;
        public int SelectedAnswer
        {
            get
            {
                return _selectedAnswer;
            }

            set
            {
                _selectedAnswer = value;
                NotifyPropertyChanged();
            }
        }

        public GameModel GameModel { get; set; }

        public ICommand SubmitCommand { get; private set; }
        public ICommand RetryCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }


        public MainWindowViewModel()
        {
            GameModel = new GameModel("./Quizzes/Central American Capitals.txt");

            SubmitCommand = new SimpleCommand(Submit);
            RetryCommand = new SimpleCommand(Retry);
            OpenCommand = new SimpleCommand(OpenNew);
        }

        private void OpenNew(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // openFileDialog.InitialDirectory = "./Quizzes/";
            openFileDialog.ShowDialog();
            try
            {
                GameModel = new GameModel(openFileDialog.FileName);
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not open file.\n\n" + e.Message);
            }
        }

        private void Retry(object obj)
        {
            GameModel = new GameModel("./Quizzes/Central American Capitals.txt");
            NotifyPropertyChanged("GameModel");
        }

        private void Submit(object parameter)
        {
            GameModel.SubmitAnswer(SelectedAnswer + 1);
            NotifyPropertyChanged("GameModel");
        }
    }
}
