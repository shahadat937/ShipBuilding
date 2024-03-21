using System;

namespace RMS.Utility
{
    public class AppCommonInfo
    {
        public string AddNewButton { get; set; }
        public string UpdateButton { get; set; }
        public string Message { get; set; }
        public int IsSucceed { get; set; }
        public string SearchKey { get; set; }
        public int GetAge(DateTime dateOfBirth, DateTime ageEstimationDate)
        {
            DateTime n = ageEstimationDate;// To avoid a race condition around midnight
            int age = n.Year - dateOfBirth.Year;

            if (n.Month < dateOfBirth.Month || (n.Month == dateOfBirth.Month && n.Day < dateOfBirth.Day))
                age--;

            return age;
        }
        public int FileMove(string sourcePath, string destinationPath)
        {
            string sourceFile = sourcePath;
            string destinationFile = destinationPath;

            // To move a file or folder to a new location:
            System.IO.File.Move(sourceFile, destinationFile);

            // To move an entire directory. To programmatically modify or combine
            // path strings, use the System.IO.Path class.
            System.IO.Directory.Move(@"C:\Users\Public\public\test\", @"C:\Users\Public\private");
            return 1;
        }
 
    }

    public static class CustomUserLoginInfo
    {
        public static int IsLogged { get; set; }
    }
}
