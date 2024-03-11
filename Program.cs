/*
* Joaquin Gomez
* COMP003A
* Final Project 
* A Health app to help you stay healthy.
*/
using System.Text.RegularExpressions;

namespace COMP003A.FinalProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /// 
            ///
            Console.Title = "How is your Health?";
            ///
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            ///
            Console.ForegroundColor = ConsoleColor.White;
            ///

            //Integers for form application
            string firstName;

            string lastName;

            string gender;

            int YourAge = 0;
            DateTime today = DateTime.Today;

            //Lists and arrays for application
            List<string> identityInfo = new List<string>();
            string[] questions = QuestionList();
            string[] answers = new string[10];

            //Beginning of form
            SectionSeparator("***We are going to assess your health ***");

            //Personal Info Questions Section
            SectionSeparator("\t\t\tPersonal Info");

            //Get first name
            do
            {
                Console.Write("What is your first name? ");
                firstName = Console.ReadLine();
                if (NameInvalid(firstName) == true)
                {
                    Console.WriteLine("Input invalid. Try again.");
                }

            } while (NameInvalid(firstName) == true);
            identityInfo.Add(firstName);


            //Get last name

            do
            {
                Console.Write("What is your last name? ");
                lastName = Console.ReadLine();
                if (NameInvalid(lastName) == true)
                {
                    Console.WriteLine("Input invalid. Try again.");
                }

            } while (NameInvalid(lastName) == true);
            identityInfo.Add(lastName);



            //Get year born
            do
            {
                Console.WriteLine($"What year were you born? ");

                try
                {
                    YourAge = Convert.ToInt32(Console.ReadLine());
                }

                catch (Exception)
                {
                    YourAge = 0;
                }

                if (YearInRange(YourAge) == false)
                {
                    Console.WriteLine("Invalid input. Try again");
                }

            } while (YearInRange(YourAge) == false);



            //Gender selection    
            do
            {
                // Here we ask what is your gender.
                Console.WriteLine($"What is your gender:\nEnter M for Male " +
                                    $"| F for Female | O for Other");
                gender = Console.ReadLine();
                gender = gender.ToUpper();
                char genderAnswer = '0';
                //=======First check on response to gender is if Empty or Null.
                if (String.IsNullOrEmpty(gender) == true)

                {
                    Console.WriteLine("Invalid input. Try again");
                }
                else
                {
                    //Try catch in case of failed char conversion.
                    try
                    {
                        genderAnswer = Convert.ToChar(gender);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid input. Try again");
                        gender = "";
                    }

                    switch (genderAnswer)
                    {
                        // Here we get the answer to gender
                        case 'F':
                            identityInfo.Add("Female");
                            break;
                        case 'M':
                            identityInfo.Add("Male");
                            break;
                        case 'O':
                            identityInfo.Add("Other");
                            break;
                        default:
                            Console.WriteLine("Can you please try again");
                            gender = "";
                            break;
                    }

                }

            } while (String.IsNullOrEmpty(gender) == true);



            //Questionnaire section
            SectionSeparator("\t\t\tQuestionnaire");
            Console.WriteLine("Thank you for that Health information.\n");

            //Questions to user and answers from user
            answers = AskQ(QuestionList());

            //Review section
            SectionSeparator("\t\t\tReview");

            //Provides review of info on screen.
            Console.WriteLine("\nHere is your information...");
            Console.WriteLine($"\n{identityInfo[1]}, {identityInfo[0]}");
            Console.WriteLine($"Gender is {identityInfo[2]}");
            Console.WriteLine($"Age: {AgeCalc(YourAge)} years old\n");

            //Array traversal for questions and answer review
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Question {i + 1} was\n{questions[i]}");
                Console.WriteLine($"Your response to Question {i + 1}.......{answers[i]}\n");
            }

            //Method to determine if you are held for further questioning
            HoldForFurtherQuestions();


        }
        //-------------------------Main End---------------------------------//


        //----------------------Module Section------------------------------//



        /// <summary>
        /// Validates if user response contains numbers or special characters.
        /// </summary>
        /// <param name="response">User response</param>
        /// <returns>True or false</returns>
        static bool NameInvalid(string response)
        {
            if (String.IsNullOrEmpty(response) == true)
            {
                return true;
            }
            else if (response.Any(ch => !char.IsDigit(ch)) == false)
            {
                return true;
            }
            else if (Regex.IsMatch(response, @"^[a-zA-z]+$") == false)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Method checks that year is in range.
        /// </summary>
        /// <param name="year">Checks year against range</param>
        /// <returns>True or false</returns>
        static bool YearInRange(int year)

        {
            DateTime today = DateTime.Today;

            if (year <= 1900 || year >= today.Year)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// Creates seperator for each section
        /// </summary>
        /// <param name="section">Name or message of new section.</param>
        static void SectionSeparator(string section)
        {
            Console.WriteLine("".PadRight(68, '*') + $"\n{section}\n" + "".
           PadRight(68, '*'));
        }

        /// <summary>
        /// Calculates age from year provided by user
        /// </summary>
        /// <param name="YourAge">Users year of birth</param>
        /// <returns></returns>
        static int AgeCalc(int YourAge)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - YourAge;

            return age;
        }
        /// <summary>
        /// Method to load questions in array and pass to global array
        /// </summary>
        /// <returns>Array holding questions is returned</returns>
        static string[] QuestionList()
        {
            string[] questionArray = new string[10];
            questionArray[0] = "How are you doing today? ";
            questionArray[1] = "Do you know what your daily intake of calories should be? ";
            questionArray[2] = "Do you eat everyday? ";
            questionArray[3] = "Do you drink water? ";
            questionArray[4] = "Do you talk to people? ";
            questionArray[5] = "Do you have a jourenl? ";
            questionArray[6] = "Do you excirce? ";
            questionArray[7] = "Do you drive? ";
            questionArray[8] = "Do you work? ";
            questionArray[9] = "Do you go to school? ";

            return questionArray;
        }

        /// <summary>
        /// Method to ask questions and record answers.
        /// </summary>
        /// <param name="questions">Questions to ask from an array.</param>
        /// <returns>answer array.</returns>
        static string[] AskQ(string[] questions)
        {
            string[] answers = new string[10];
            for (int i = 0; i < 10; i++)
            {
                string userResponse = "";
                do
                {
                    Console.WriteLine($"Question {i + 1}: {questions[i]}");
                    userResponse = Console.ReadLine();
                }
                while (String.IsNullOrEmpty(userResponse) == true);
                answers[i] = userResponse;

            }
            return answers;
        }

        /// <summary>
        /// Method to determine if you are held for further questioning by using a random number
        /// If the number is even, you will be held for further questioning
        /// If the number is odd, you are free to go
        /// </summary>
        static void HoldForFurtherQuestions()
        {
            Random holdNo = new Random();
            int random = holdNo.Next(1, 2);
            if (random % 2 == 0)
            {
                Console.WriteLine("\n\t- We need you to see a doctor now.");
            }
            else
            {
                Console.WriteLine("\n\t- You are Fine _");
            }
            Console.ReadKey();
        }
    }
}