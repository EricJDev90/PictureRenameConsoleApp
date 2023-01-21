using PictureRenameConsoleApp;
using PictureRenameConsoleApp.Models;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;


MessageStrings feedback = new MessageStrings();
Console.WriteLine(value: feedback.INTRODUCTION_TEXT);
Folder folder = new Folder();
int tries = 0;
Regex regex = new Regex(":|\\0");
ProcessingResult results = new ProcessingResult();

//Get path and validate
while (!folder.isValidPathAndExists || tries >= 10 || folder.filesJPG.Length == 0)
{
    Console.WriteLine(value: feedback.BEGIN_TEXT);
    var enteredPath = Console.ReadLine();

    if (tries >= 10)
    {
        Console.WriteLine(value: feedback.ERROR_TOO_MANY_FAILED_ATTEMPTS);
        Console.ReadLine();
        Environment.Exit(0);
    }

    if (enteredPath != null && enteredPath.Length > 3)
    {
        folder.CheckIsValidPathAndExists(enteredPath);
        if (folder.isValidPathAndExists)
        {
            //Valid path, check for files a top directory
            folder.filesJPG = Directory.GetFiles(folder.pathUri, "*.jpg", SearchOption.TopDirectoryOnly);
            if (folder.filesJPG.Length == 0)
            {
                Console.WriteLine(value: feedback.ERROR_NO_JPG_FOUND);
                tries++;
                continue;
            }
        }
    }
    if (enteredPath == null || enteredPath.Length == 0)
    {
        Console.WriteLine(value: feedback.ERROR_NULL_PATH);
        tries++;
        continue;
    }
    if (enteredPath.Length is > 0 and < 3)
    {
        Console.WriteLine(value: feedback.ERROR_PATH_TOO_SHORT);
        tries++;
        continue;
    }
    if (!folder.isValidPathAndExists)
    {
        Console.WriteLine(value: feedback.PATH_DOESNT_EXIST);
        tries++;
        continue;
    }
   
}

Console.WriteLine();
Console.WriteLine(value: feedback.BEGIN_PROCESSING);
Console.WriteLine();

//Process the files, output results
folder.ProcessFiles(folder.filesJPG, ".jpg", regex, results);
results.generateOutputFile(folder.pathUri, feedback);


Console.WriteLine();
Console.WriteLine(value: feedback.SUCCESSFULLY_CHANGED + results.successfullyChanged + feedback.IMAGES);
Console.WriteLine(value: feedback.FAILED_CHANGED + results.failedToChange + feedback.IMAGES);
Console.WriteLine(value: feedback.RESULTS_LOCATION + folder.pathUri);
Console.WriteLine(value: feedback.THANK_YOU);
Console.ReadLine();