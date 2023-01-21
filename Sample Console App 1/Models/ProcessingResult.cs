using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureRenameConsoleApp.Models
{
    internal class ProcessingResult
    {
        public int successfullyChanged { get; set; }
        public List<string> successfulPaths { get; set; }
        public int failedToChange { get; set; }
        public List<string> failedPaths { get; set; }

        public ProcessingResult()
        {
            successfullyChanged = 0;
            successfulPaths= new List<string>();
            failedToChange = 0;
            failedPaths= new List<string>();
        }

        public async void generateOutputFile(string outputPath, MessageStrings feedback)
        {
            //Create the output file in the same directory as the pictures
            DateTime dateTime= DateTime.Now;
            string fullOutputPath = outputPath + "\\" + dateTime.ToString("yyyy-MM-ddTHH-mm-ss") + "-Output.txt";
            using StreamWriter sw = new(fullOutputPath, append: true);

            //Display results of run
            sw.WriteLine(value: feedback.SUCCESSFULLY_CHANGED + successfullyChanged + " JPG");
            sw.WriteLine(value: feedback.FAILED_CHANGED + failedToChange + " JPG");
            sw.WriteLine();

            if (failedToChange > 0) 
            {
                sw.WriteLine(value: feedback.FAILED_TO_CHANGE_LABEL);
                sw.WriteLine();

                //Display failed pictures first
                foreach (string path in failedPaths)
                {
                    await sw.WriteLineAsync(path);
                }
                sw.WriteLine();
            }
            

        
            if (successfullyChanged > 0)
            {
                sw.WriteLine(value: feedback.SUCCESSFUL_CHANGE_LABEL);
                sw.WriteLine();

                //Display successful picutres
                foreach (string path in successfulPaths)
                {
                    await sw.WriteLineAsync(path);
                }
            }
            
            sw.Close();
        }
    }
}
