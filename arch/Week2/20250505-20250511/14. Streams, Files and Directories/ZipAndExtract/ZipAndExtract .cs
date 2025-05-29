namespace ZipAndExtract
{
    using System;
    using System.IO;
    using System.IO.Compression;

    public class ZipAndExtract
    {
        static void Main()
        {
            string inputFile = @"..\..\..\copyMe.png";
            string zipArchiveFile = @"..\..\..\archive.zip";
            string extractedFile = @"..\..\..\extracted.png";

            ZipFileToArchive(inputFile, zipArchiveFile);

            var fileNameOnly = Path.GetFileName(inputFile);
            ExtractFileFromArchive(zipArchiveFile, fileNameOnly, extractedFile);
        }

        public static void ZipFileToArchive(string inputFilePath, string zipArchiveFilePath)
        {
            using (FileStream fileToArchive = new FileStream(zipArchiveFilePath, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(fileToArchive, ZipArchiveMode.Create))
                {
                    archive.CreateEntryFromFile(inputFilePath, Path.GetFileName(inputFilePath));
                }
            }
        }

        public static void ExtractFileFromArchive(string zipArchiveFilePath, string fileName, string outputFilePath)
        {
            using (ZipArchive archive = ZipFile.Open(zipArchiveFilePath, ZipArchiveMode.Update))
            {
                ZipArchiveEntry entry = archive.GetEntry(fileName);
                using (StreamWriter writer = new StreamWriter(entry.Open()))
                {
                    writer.BaseStream.Seek(0, SeekOrigin.End);
                }
            }
        }
    }
}
