using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Text.RegularExpressions;

namespace PictureRenameConsoleApp.Models
{
    internal class Folder
    {
        public string pathUri { get; set; }
        public int totalFiles { get; set; }
        public bool isValidPathAndExists { get; set; }
        public MessageStrings feedback { get; set; }


        public Folder()
        {
            pathUri = "";
            totalFiles = 0;
            feedback = new MessageStrings();
        }


        /// <summary>
        /// Determines if the path given is valid and exists
        /// </summary>
        /// <param name="path">String path</param>
        /// <returns>bool</returns>
        public bool CheckIsValidPathAndExists(string path)
        {
            Regex driveCheck = new Regex(@"^[a-zA-Z]:\\$");
            if (!driveCheck.IsMatch(path.Substring(0, 3)))
            {
                return false;
            }

            //Check for Bad Characters
            string strTheseAreInvalidFileNameChars = new string(Path.GetInvalidPathChars());
            strTheseAreInvalidFileNameChars += @":/?*" + "\"";
            Regex containsABadCharacter = new Regex("[" + Regex.Escape(strTheseAreInvalidFileNameChars) + "]");
            if (containsABadCharacter.IsMatch(path.Substring(3, path.Length - 3)))
            {
                return false;
            }

            //Check if the path exists
            DirectoryInfo dir = new DirectoryInfo(Path.GetFullPath(path));
            if (!dir.Exists)
            {
                return false;
            }

            //It is a valid path and exists
            pathUri = path;
            isValidPathAndExists = true;
            return true;
        }

        public ProcessingResult ProcessFiles(string[] files, string fileType, Regex regexPattern, ProcessingResult results)
        {
            for (int x = 0; x < files.Length; x++)
            {
                string dateTaken = "";
                using (FileStream fs = new FileStream(files[x], FileMode.Open, FileAccess.Read))
                using (Image image = Image.FromStream(fs, false, false))
                {
                    try
                    {
                        PropertyItem propItem = image.GetPropertyItem(36867); //Property 36867 is the date/time a picture was taken for jpg

                        //Parse Date time
                        dateTaken = regexPattern.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 4); //Change YYYY/MM/DD HH/MM/SS to YYYY-MM-DD HH-MM-SS
                        dateTaken = regexPattern.Replace(dateTaken, ""); //Remove trailing \0 from name
                        Console.WriteLine(value: feedback.PHOTO_FOUND_STRING + files[x] + " - " + feedback.PHOTO_RENAME_STRING + dateTaken + fileType);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(value: feedback.ERROR_FIND_DATE_MISSING_CORRUPT + files[x]);
                        results.failedPaths.Add(files[x]);
                        results.failedToChange++;
                        continue;
                    }
                }

                if (dateTaken.Length == 0)
                {
                    Console.WriteLine(value: feedback.ERROR_FIND_DATE_MISSING_CORRUPT + pathUri);
                    results.failedToChange++;
                    results.failedPaths.Add(files[x]);
                    continue;
                }

                try
                {
                    File.Move(files[x], pathUri + "\\" + dateTaken + fileType); //Move the file and rename
                    Console.WriteLine(value: feedback.SUCCESSFULLY_CHANGED);
                    results.successfulPaths.Add(files[x]);
                    results.successfullyChanged++;
                }
                catch (Exception e)
                {
                    Console.WriteLine(value: feedback.FAILED_CHANGED + pathUri);
                    results.failedPaths.Add(files[x]);
                    results.failedToChange++;
                }
            }
            return results;
        }
    }
}
