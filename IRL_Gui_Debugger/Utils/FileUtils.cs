namespace IRL_Gui_Debugger.Utils
{
    public static class FileUtils
    {
        public static string CreateUniqeFileName(string path, string fileName, string extension)
        {
            string fullFilePath = $"{path}\\{fileName}{extension}";

            if (!File.Exists(fullFilePath))
            {
                return fullFilePath;
            }

            int number = 1;
            while (true)
            {
                fullFilePath = $"{path}\\{fileName}({number}){extension}";

                if (!File.Exists(fullFilePath))
                {
                    return fullFilePath;
                }

                number++;
            }
        }
    }
}
