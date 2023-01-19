namespace EduHome.Business.Utilities
{
    public static class Helper
    {
        public static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return string.Concat(Path.GetFileNameWithoutExtension(fileName)
                           , "_"
                           , Guid.NewGuid().ToString().AsSpan(0, 4)
                           , Path.GetExtension(fileName));
        }


        public static void DeleteFile(string wwwroot,params string [] paths)
        {
            string resultPath = wwwroot;
            foreach (var path in paths)
            {
                resultPath= Path.Combine(resultPath, path);
            }

            if(System.IO.File.Exists(resultPath)) 
            { 
                File.Delete(resultPath);
            }
        }
    }
}


