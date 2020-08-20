using QuizCardsModel;
using System;
using System.Linq;

namespace QuizCardsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var filename = "./Quizzes/Central American Capitals.txt";

            // Read args if user specifies file
            if (args.Length >= 1)
            {
                filename = args[0];
            }

            // Setup game from specified file file
            GameModel model = null;
            try
            {
                model = new GameModel(filename);
            } catch
            {
                Console.WriteLine("There was an error reading or setting up: " + filename);
                return;
            }

            Console.WriteLine("Playing from " + filename);
            Console.WriteLine();

            // Answer questions
            while (model.State == GameState.Quiz) {
                Console.WriteLine(model.CurrentItem.Question.Question);
                for (int i = 0; i < model.CurrentItem.Question.Choices.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + model.CurrentItem.Question.Choices[i]);
                }
                Console.Write("Answer #: ");
                var answer = Console.ReadLine();

                int answerInt = int.Parse(answer.Trim());

                var correct = model.SubmitAnswer(answerInt);

                if (correct)
                {
                    Console.WriteLine("Correct");
                } else
                {
                    Console.WriteLine("Incorrect");
                }

                model.AdvanceCard();

                Console.WriteLine();
            }

            // Print Results
            Console.WriteLine();
            var correctCount = model.CorrectCount;
            var totalCount = model.QuizItems.Count;
            Console.WriteLine("Answered " + correctCount + " / " + totalCount + " correctly.");
        }
    }
}
