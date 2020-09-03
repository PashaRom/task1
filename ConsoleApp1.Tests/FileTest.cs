using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ConsoleApp1;

namespace ConsoleApp1.Tests
{
    [TestFixture]
    class FileTest
    {
        private File fileWithEvenContent, fileWithUnEvenContent;
        private string fileName = Configuration.Get["fileTest:fileName"];
        private string evenContent = Configuration.Get["fileTest:evenContent"];
        private string unEvenContent = Configuration.Get["fileTest:unEvenContent"];
        private double evenContentSize = 10, unEvenContentSize = 9.5;
        private static string[] PathStrings = {
            Configuration.Get["fileTest:pathDirectory"],
            Configuration.Get["fileTest:pathHttp"]
        };

        [SetUp]
        public void SetUp()
        {
            fileWithEvenContent = new File(fileName, evenContent);
            fileWithUnEvenContent = new File(fileName, unEvenContent);
        }        

        [Test]
        public void getFilename_FileName_true() {           
            Assert.AreEqual(fileName, fileWithEvenContent.getFilename(),
                $"Expected value \"{fileName}\" do not equal actual getFilename() method which returned \"{fileWithEvenContent.getFilename()}\"");
        }
        [TestCaseSource(nameof(PathStrings))]        
        public void getFilename_FullFileName_true(string path) {
            File fullFileNameFile = new File(path, evenContent);
            Assert.AreEqual(fileName, fullFileNameFile.getFilename(),
                $"Expected value \"{fileName}\" do not equal actual getFilename() method which returned \"{fullFileNameFile.getFilename()}\"");
        }

        [Test]
        public void getSize_EvenContentSize_true() {
            Assert.AreEqual(evenContentSize, fileWithEvenContent.getSize(), 
                $"Expected value {evenContentSize} don not equal actual getSize() method which returned {fileWithEvenContent.getSize()}.");
        }
        [Test]
        public void getSize_UnEvenContentSize_true() {
            Assert.AreEqual(unEvenContentSize, fileWithUnEvenContent.getSize(),
                $"Expected value {unEvenContentSize} don not equal actual getSize() method which returned {fileWithUnEvenContent.getSize()}.");
        }

    }
}
