using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace QuizCardsModel
{
    public static class FileReader
    {
        enum FileReaderState
        {
            ReadQuestion,
            ReadChoiceOrCorrectAnswer,
        }

        public static List<QuizItem> ReadQuizFile(string path)
        {
            List<QuizItem> quizItems = new List<QuizItem>();

            string[] quizFileLines = System.IO.File.ReadAllLines(path);
            var state = FileReaderState.ReadQuestion;

            // Setup regexes
            Regex readQuestionRegex = new Regex(@"\(([0-9]+)\) +([^\n]+)", RegexOptions.Compiled);
            Regex readChoicesRegex = new Regex(@"([0-9])\. ([^\n]+)");

            // Initialize temporary variables per quiz item
            int tempItemIndex = 0;
            string tempQuestion = string.Empty;
            int tempCorrectAnswerIndex = 0;
            var tempChoices = new List<String>();

            // Read the file line by line

            for (int i = 0; i < quizFileLines.Length; i++)
            {
                string line = quizFileLines[i];

                // Skip this line if it is whitespace
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                switch (state)
                {
                    // The reader is expecting the question number and question text
                    case FileReaderState.ReadQuestion:
                        {
                            tempChoices = new List<string>();

                            var questionMatch = readQuestionRegex.Matches(line);

                            // Ensure the line is matched
                            if (questionMatch.Count == 1 && questionMatch[0].Groups.Count == 3)
                            {
                                var questionGroups = questionMatch[0].Groups;
                                tempItemIndex = int.Parse(questionGroups[1].Value);
                                tempQuestion = questionGroups[2].Value;

                                state = FileReaderState.ReadChoiceOrCorrectAnswer;
                            } else
                            {
                                throw new Exception("Error on line " + i + " expected \"(QUESTION_NUMBER) QUESTION TEXT\"");
                            }
                        }
                        break;

                    // The reader is expecting a choice or the single integer correct answer
                    case FileReaderState.ReadChoiceOrCorrectAnswer:
                        {
                            var choicesMatch = readChoicesRegex.Matches(line);

                            if (choicesMatch.Count == 1 && choicesMatch[0].Groups.Count == 3)
                            {
                                // This block if the string is formatted as a choice properly

                                var choicesGroups = choicesMatch[0].Groups;

                                //
                                tempChoices.Add(choicesGroups[2].Value);
                            } else if (int.TryParse(line.Trim(), out int lineValue))
                            {
                                // This block if the string is a single integer, and is a correct answer index
                                tempCorrectAnswerIndex = lineValue;

                                // Add the quiz item to the list and reset the state
                                quizItems.Add(new QuizItem(tempItemIndex, tempQuestion, tempCorrectAnswerIndex, tempChoices));
                                state = FileReaderState.ReadQuestion;
                            } else
                            {
                                throw new Exception("Error on line " + i + " expected \"RESPONSE_NUMBER. RESPONSE TEXT\"");
                            }
                        }
                        break;
                }
            }

            return quizItems;
        }
    }
}
