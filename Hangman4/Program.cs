// See https://aka.ms/new-console-template for more information
using System.Text;

Console.WriteLine("Hello, World!");
//1: Visa spelets regler.  [X]
//2: Se till att användaren skall kunna gissa på hela ordet vid varje nytt försök. [X]
//3: Visa de felaktiga tecknen efter varje gissning.   [x]
//4: Se till att man får gissa max 10 gånger. [x]
//5: Visa hur många gånger man har gissat. [x]
//6: Efter man har misslyckats ska det visas vilka tecken som är korrekt. [x]


void GameRules()
{
    Console.WriteLine("Welcome Hajis Hangman Game! :)");
    // StallForTwoSec();
    Console.WriteLine("You will from now onwards be introduced to the game rules.");
    // StallForTwoSec();
    Console.WriteLine($"For each game there will be a secretword that is created.\nTo win the game, you  have to guess the correct word\nOBS: you only have 10 chances to guess the word.\nGood luck :)");
}
void printOutWrongLetters(string wrongLetters, int amountFailed)
{
    Console.WriteLine("Amount of times you failed: " + amountFailed);
    Console.Write("\nWrong Letters guessed so far: ");
    foreach (char letters in wrongLetters)
    {
        Console.Write(letters + " ");
    }
}

void printOutWrongLettersWholeWord(Dictionary<string, int> wrongWordsList, int amountFailed)
{
    Console.WriteLine("Amount of times you failed: " + amountFailed);
    foreach (string wrongWords in wrongWordsList.Keys)
    {
        Console.WriteLine("Current Guessed Wrong Words: " + wrongWords);
    }

}




int WordPrinted(List<char> PreviousGuessed, string randomChosenWord)
{
    int counter = 0;
    int rightLetters = 0;
    Console.Write("\r\n");
    foreach (char c in randomChosenWord)
    {
        if (PreviousGuessed.Contains(c))
        {
            Console.Write(c + " ");
            rightLetters += 1;
        }
        else
        {
            Console.Write("  ");
        }
        counter += 1;
    }
    return rightLetters;
}

void printLines(string randomChosenWord)
{
    Console.Write("\r");
    foreach (char c in randomChosenWord)
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        Console.Write("\u0305 ");
    }
}

void Main()
{
    GameRules();

    Random random = new Random();
    List<string> wordDictionary = new List<string> { "peaches", "Oranges", "Apples", "peron", "pinneaple", "Mango", "banana", "Avocado" };
    int index = random.Next(wordDictionary.Count);
    String randomChosenWord = wordDictionary[index];
    string currentWrongLetters = "";
    //the secret key
    Console.WriteLine(randomChosenWord);

    Console.WriteLine("Type 1: to Guess one letter each time: 2: To Guess the whole word");
    int choice = int.Parse(Console.ReadLine());


    foreach (char x in randomChosenWord)
    {
        Console.Write("_ ");
    }

    int lengthOfWordToGuess = randomChosenWord.Length;
    int amountOfTimesWrong = 0;
    List<char> currentLettersGuessed = new List<char>();
    Dictionary<string, int> previouslyGussed = new Dictionary<string, int>();

    int currentLettersRight = 0;

    while (amountOfTimesWrong <= 10 && currentLettersRight != lengthOfWordToGuess)
    {
        if (choice == 1)
        {
            //Console.WriteLine($"Amount of time you failed: {amountOfTimesWrong}");


            printOutWrongLetters(currentWrongLetters, amountOfTimesWrong);

            Console.Write("\nGuess a letter: ");

            char letterGuessed = Console.ReadLine()[0];

            if (currentLettersGuessed.Contains(letterGuessed))
            {
                Console.Write("\r\n You have already guessed this letter");
                GameLevel(amountOfTimesWrong);
                currentLettersRight = WordPrinted(currentLettersGuessed, randomChosenWord);
                printLines(randomChosenWord);
            }
            else
            {

                bool right = false;
                for (int i = 0; i < randomChosenWord.Length; i++)
                {
                    if (letterGuessed == randomChosenWord[i])
                    {
                        right = true;
                    }
                }


                if (right)
                {
                    GameLevel(amountOfTimesWrong);

                    currentLettersGuessed.Add(letterGuessed);
                    currentLettersRight = WordPrinted(currentLettersGuessed, randomChosenWord);
                    Console.Write("\r\n");
                    printLines(randomChosenWord);
                }

                else
                {
                    amountOfTimesWrong += 1;
                    currentLettersGuessed.Add(letterGuessed);
                    StringBuilder s = new StringBuilder();
                    currentWrongLetters += s.Append(letterGuessed) + ",";

                    GameLevel(amountOfTimesWrong);

                    currentLettersRight = WordPrinted(currentLettersGuessed, randomChosenWord);
                    Console.Write("\r\n");
                    printLines(randomChosenWord);
                }
            }


            if (currentLettersRight != randomChosenWord.Length)
            {
                printOutWrongLetters(currentWrongLetters, amountOfTimesWrong);

                try
                {
                    Console.WriteLine("Type 1: To continue guessing a letter or press enter");
                    Console.WriteLine("Type 2: To guess a the whole word");
                    choice = int.Parse(Console.ReadLine());

                }
                catch
                {
                    choice = 1;

                }

            }

        }

        else
        {
            if (choice == 2)
            {

                printOutWrongLettersWholeWord(previouslyGussed, amountOfTimesWrong);

                Console.WriteLine("Guess the whole word: ");
                //Console.WriteLine($"Amount of times tryed {amountOfTimesWrong}");


                string WholeWordGuess = Console.ReadLine();
                if (WholeWordGuess == randomChosenWord)
                {
                    Console.WriteLine("Congrats you Gussed the Rightword");
                    Console.WriteLine(WholeWordGuess);
                    // Console.Write("\r\n");
                    // printLines(randomChosenWord);
                    currentLettersRight = randomChosenWord.Length;
                    currentLettersRight = randomChosenWord.Length;
                    amountOfTimesWrong = 10;
                    continue;


                }

                if (previouslyGussed.ContainsKey(WholeWordGuess))
                {
                    Console.WriteLine("You already guessed this word: Im not consuming your guess");
                    printLines(randomChosenWord);
                    GameLevel(amountOfTimesWrong);




                }
                else
                {
                    previouslyGussed.Add(WholeWordGuess, 1);
                    Console.WriteLine("You guessed Wrong");
                    printOutWrongLettersWholeWord(previouslyGussed, amountOfTimesWrong);

                    printLines(randomChosenWord);
                    amountOfTimesWrong += 1;
                    GameLevel(amountOfTimesWrong);

                }

                // Console.WriteLine("Im gussing the whole letter");

                try
                {
                    if (WholeWordGuess != randomChosenWord)
                    {
                        Console.WriteLine("Type 1: To continue guessing a word");
                        Console.WriteLine("Type 2: To guess a letter");
                        choice = int.Parse(Console.ReadLine());
                    }

                }
                catch
                {
                    choice = 2;
                }

            }
        }

    }
    Console.WriteLine("\r\nGame is over! Thank you for playing ");
    Console.WriteLine("The secret word is: " + randomChosenWord);
}


void GameLevel(int wrong)
{
    switch (wrong)
    {
        case 0:

            break;

        case 1:

            break;

        case 2:

            break;

        case 3:

            break;


        case 4:


            break;

        case 5:


            break;

        case 6:

            break;

        case 7:

            break;

        case 8:

            break;

        case 9:

            break;

        case 10:

            break;

    }

}

Main();


// Hej Haji,
// Jag körde ditt program och är inte helt nöjd med följande punkter:
// Man får ingen information om spelets regler (typ hur många gissningar som gäller). [X] = unchecked
// Du tvingar användaren att välja en bokstav hela tiden. Sådan är inte tanken. Användaren ska kunna gissa på hela ordet för varje nytt försök. [x] = unchecked
// Programmet ska visa de felaktiga tecknen efter varje gissning. Du visar alla. [x] = unchecked
// Man ser inte hur många försök som är kvar. [x] = unchecked
// När jag testade så avslutades programmet efter 14 försök. 10 gånger ska man få gissa som max. [X] = unchecked
// Du har många varianter på Hangman. Vilken är det som gäller? Jag testade på Hangman E. [X] = unchecked
// Det ska även visas vilket ord som ska vara den korrekta, när man misslyckas. [X] = unchecked