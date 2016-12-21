namespace Objects.Generator.Core.Managers
{
    using System.IO;
    using System.Text;

    public class CodeFileManager
    {

        public static void CreateFile(string filePath, string texto)
        {
            using (var writer = new StreamWriter(filePath, false))
            {
                writer.Write(texto);
                writer.Flush();
            }
        }

        public static void DeleteFile(string file)
        {
            if (File.Exists(file)) { File.Delete(file); }
        }

        public static void CopyFile(string sourceFile, string destFile, bool rewrite)
        {
            if (File.Exists(sourceFile)) { File.Copy(sourceFile, destFile, rewrite); }
        }

        public static void CopyFileWithRewrite(string sourceFile, string destFile)
        {
            CopyFile(sourceFile, destFile, true);
        }

        public static FileStream ReadFile(string path)
        {
            return File.OpenRead(path);
        }

        public static StreamReader ReadFileStream(string path, Encoding enconding)
        {
            return new StreamReader(ReadFile(path), enconding);
        }

    }

}
