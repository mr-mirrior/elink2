using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Elink
{
    public static class FileIO
    {
        public static int DirectoryCopy(string sourceDirName, 
            string destDirName, 
            bool copySubDirs,
            bool overwrite)
        {
            int count = 0;

            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

//             destDirName = Path.Combine(destDirName, dir.Name);
//             Directory.CreateDirectory(destDirName);
            //dirs = dir.GetDirectories();

            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = Path.Combine(destDirName, file.Name);

                // Copy the file.
                try
                {
                    file.CopyTo(temppath, overwrite);
                    count++;
                }
                catch
                {
                    return 0;
                }
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {

                foreach (DirectoryInfo subdir in dirs)
                {
                    // Create the subdirectory.
                    string temppath = Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    count += DirectoryCopy(subdir.FullName, temppath, copySubDirs, overwrite);
                }
            }
            return count;
        }

    }
}
