using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Readability_Check.Pages
{
    public partial class TextTest
    {

        private string userInput { get; set; } = "";
        private int characterCount { get; set; } = 0;
        private int wordCount = 0;
        private int sentenceCount = 0;
        private int syllableCount = 0;
        private int polysyllableCount = 0;
        private float ariScore = 0;
        private float fkrScore = 0;
        private float smgScore = 0;
        private float cliScore = 0;
        private int ariAge = 0;
        private int fkrAge = 0;
        private int smgAge = 0;
        private int cliAge = 0;
        private double avgAge = 0;
        private float avgScore = 0;
        private string avgGradeLevel = "";

        private bool IsShow = true;




        public void ReadableCheck()
        {
            ReadScore();
            SyllablesCount();
            AvgTest();
            AriTest();
            FkrTest();
            SmogTest();
            CliTest();
            Show();

        }

        private void Show()
        {
            if (characterCount == 0)
            {
                IsShow = true;
            }
            else
                IsShow = false;
        }

        public void ReadScore()
        {
            characterCount = 0;
            wordCount = 0;
            sentenceCount = 0;

            char[] charsToTrim = { '.', '!', '?' };
            userInput = userInput.TrimStart(' ');
            userInput = Regex.Replace(userInput, @"\s+", " ");

            // Separate each sentence in the given string
            foreach (string userinput in userInput.TrimEnd(charsToTrim).Split(charsToTrim))
            {
                wordCount += userinput.Trim().Split(' ').Count();
                sentenceCount++;
            }
            foreach (string word in userInput.Split(' '))
            {
                characterCount += word.Length;
            }

            if (characterCount == 0)
            {
                wordCount = 0;
                sentenceCount = 0;
            }
        }



        public void SyllablesCount()
        {
            char[] charsToTrim = { '.', '!', '?' };
            char[] vowelLetter = { 'a', 'e', 'i', 'o', 'u', 'y' };
            bool prevVowel;
            bool isPolysyllable = false;
            int localSyllableCount = 0;
            foreach (string userinput in userInput.TrimEnd(charsToTrim).Split(charsToTrim))
            {
                foreach (string word in userinput.Split(' '))
                {
                    prevVowel = false;
                    isPolysyllable = false;
                    localSyllableCount = 0;
                    int i = 0;
                    int wordLength = word.Length;

                    // if the word is blank space don't add syllable
                    if (wordLength != 0)
                    {
                        foreach (char character in word.ToLower())
                        {
                            bool isLastLetter = i == (wordLength - 1);

                            if (Array.Exists(vowelLetter, element => element == character))
                            {
                                //Rule3: if last letter in word is 'e' do not count as a vowel.
                                if (isLastLetter == true && character.Equals('e'))
                                {

                                }
                                // Rule 1: Count the Number of vowel
                                else if (prevVowel == false)
                                {
                                    localSyllableCount++;
                                    prevVowel = true;
                                }
                                // Rule 2: Do Not count float vowels
                                else if (prevVowel == true)
                                {
                                    prevVowel = true;
                                }
                            }
                            else
                            {
                                prevVowel = false;
                            }

                            if (localSyllableCount > 2 && isPolysyllable == false)
                            {
                                polysyllableCount++;
                                isPolysyllable = true;
                            }
                            i++;
                        }

                        // Rule 4:
                        // if a word contains 0 vowel then syllable is 1
                        if (localSyllableCount == 0)
                        {
                            syllableCount += 1;
                        }
                        else
                            syllableCount += localSyllableCount;
                    }

                }
            }
        }



        // Method for Automated Readability Index
        public void AriTest()
        {
            ariScore = (float)(4.71 * ((float)characterCount / (float)wordCount) + (0.5 * ((float)wordCount
                                                                            / (float)sentenceCount) - 21.43));

            ariAge = AgeGroupChecker(ariScore);
        }


        // Flesch–Kincaid readability tests
        public void FkrTest()
        {
            fkrScore = (float)((0.39 * (float)wordCount / (float)sentenceCount) + (float)syllableCount / (float)wordCount * 11.8 - 15.53);
            fkrAge = AgeGroupChecker(fkrScore);
        }

        public void SmogTest()
        {
            smgScore = (float)((1.043 * Math.Sqrt(polysyllableCount * (30 / (float)sentenceCount))) + 3.1291);
            smgAge = AgeGroupChecker(smgScore);
        }

        public void CliTest()
        {
            float L = ((float)characterCount / (float)wordCount) * 100;
            float S = ((float)sentenceCount / (float)wordCount) * 100;

            cliScore = (float)((0.0588 * L) - 0.296 * S - 15.8);
            cliAge = AgeGroupChecker(cliScore);

        }

        public void AvgTest()
        {
            AriTest();
            FkrTest();
            SmogTest();
            CliTest();
            avgAge = (double)((double)ariAge + (double)fkrAge + (double)smgAge + (double)cliAge) / 4;
            avgScore = (float)(ariScore + fkrScore + smgScore + cliScore) / 4;
            avgGradeLevel = GradeLevelChecker(avgScore);
        }


        // method for age group
        private static int AgeGroupChecker(float score)
        {
            int ageGroup = 0;
            if (score < 2)
            {
                ageGroup = 6;
            }
            else if (score < 3)
            {
                ageGroup = 7;
            }
            else if (score < 4)
            {
                ageGroup = 9;
            }
            else if (score < 5)
            {
                ageGroup = 10;
            }
            else if (score < 6)
            {
                ageGroup = 11;
            }
            else if (score < 7)
            {
                ageGroup = 12;
            }
            else if (score < 8)
            {
                ageGroup = 13;
            }
            else if (score < 9)
            {
                ageGroup = 14;
            }
            else if (score < 10)
            {
                ageGroup = 15;
            }
            else if (score < 11)
            {
                ageGroup = 16;
            }
            else if (score < 12)
            {
                ageGroup = 17;
            }
            else if (score < 13)
            {
                ageGroup = 18;
            }
            else if (score < 14)
            {
                ageGroup = 24;
            }
            return ageGroup;
        }

        private static string GradeLevelChecker(float score)
        {
            string gradeLevel = "";
            if (score < 2)
            {
                gradeLevel = "Kindergarten";
            }
            else if (score < 3)
            {
                gradeLevel = "First/Second";
            }
            else if (score < 4)
            {
                gradeLevel = "Third";
            }
            else if (score < 5)
            {
                gradeLevel = "Fourth";
            }
            else if (score < 6)
            {
                gradeLevel = "Fifth";
            }
            else if (score < 7)
            {
                gradeLevel = "Sixth";
            }
            else if (score < 8)
            {
                gradeLevel = "Seventh";
            }
            else if (score < 9)
            {
                gradeLevel = "Eighth";
            }
            else if (score < 10)
            {
                gradeLevel = "Ninth";
            }
            else if (score < 11)
            {
                gradeLevel = "Tenth";
            }
            else if (score < 12)
            {
                gradeLevel = "Eleventh";
            }
            else if (score < 13)
            {
                gradeLevel = "Twelfth";
            }
            else if (score < 14)
            {
                gradeLevel = "College";
            }
            else if (score > 14)
            {
                gradeLevel = "Professor";
            }
            return gradeLevel;
        }
    }
}
