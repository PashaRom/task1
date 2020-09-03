using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ConsoleApp1;
using FileSystem.exception;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp1.Tests
{
    [TestFixture]
    class FileStorageTest
    {
        private List<string> fileNames;
        private List<File> files;
        private FileStorage fileStorage;
        string checkingFileName = "myfile-1.txt",
            bigSizeFileName = Configuration.Get["fileStorageTest:bigSizeFileName"],
            bigSizeContent = Configuration.Get["fileStorageTest:bigSizeContent"];
        File checkingFile,
            bigSizeFile;
        
        [OneTimeSetUp]
        public void OneTimeSetUp() {           
            checkingFile = new File(checkingFileName, "I am content.");
            bigSizeFile = new File(bigSizeFileName, bigSizeContent);
            fileNames = new List<string>();
            files = new List<File>();
            fileStorage = new FileStorage();
            for (int i = 0; i <= 3; i++) {
                fileNames.Add($"myfile{i}.txt");
            }
            foreach(string fileName in fileNames) {
                files.Add(new File(fileName, 
                    $"This is content for {fileName}."));
            }
            foreach (File file in files) {
                fileStorage.write(file);
            }
        }
        [Test]
        
        public void write_AddFile_True() {
            FileStorage tempFileStorage = new FileStorage();
            Assert.IsTrue(tempFileStorage.write(checkingFile),
                $"The file {checkingFile} has not writen.");
        }
        [Test]
        public void write_AddDifferentFile_True() {
            Assert.IsTrue(fileStorage.write(checkingFile));
        }

        [Test]
        public void write_AddBigSizeFile_False() {
            Assert.IsFalse(fileStorage.write(bigSizeFile),
                "Big size file is not able to write.");
        }      

        [Test]
        public void isExist_CheckWriteableDifferentFile_False() {           
            Assert.IsFalse(fileStorage.isExists(checkingFileName),
                $"File name {checkingFileName} is exist in FileStorage.");
        }

        [Test]
        public void isExist_CheckWriteableSameFile_True() {
            Assert.IsTrue(fileStorage.isExists(fileNames[1]),
                $"File name {fileNames[1]} has been added to FileStorage");
        }

        [Test]
        public void getFile_GetFileByName_File()
        {
            string checkingExistFileName = fileNames[1];
            File tempCheckingFile = new File("null", "null");
            foreach (File tempFile in files)
            {
                if (tempFile.getFilename().Equals(checkingExistFileName))
                    tempCheckingFile = tempFile;
            }
            Assert.AreEqual(tempCheckingFile, fileStorage.getFile(checkingExistFileName),
                $"Has not found file by file name {checkingExistFileName}.");
        }

        [Test]
        public void getFile_GetFileByNameWhichUnexist_Null() {
            Assert.IsNull(fileStorage.getFile(checkingFileName),
                $"File name \"{checkingFileName}\" has been found.");
        }

        [Test]
        public void getFiles_CompareFileLists_ListFiles() {
            CollectionAssert.AreEqual(files, fileStorage.getFiles(),
                "List files do not equal FileStorage.getFiles.");
        }

        [Test]
        public void delete_CheckDeleteFileFromFileStorage()
        {
            fileStorage.write(checkingFile);
            Assert.IsTrue(fileStorage.delete(checkingFile.getFilename()),
                $"File with file name \"{checkingFile.getFilename()}\" has not been deleted from FileStorage.");
        }

        [Test]
        public void write_AddSameFale_FileNameAlreadyExistsException()
        {
            Assert.Catch<FileNameAlreadyExistsException>(
                () => fileStorage.write(files[0]),
                $"File {files[0]} has not been written, becouse it had been written.");
        }

    }
}
