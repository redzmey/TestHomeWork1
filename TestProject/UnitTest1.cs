using System;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperDict;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            TranslationHandler.Clear();
        }
       
        #region Clear

        [TestMethod]
        [DataRow("Hello", "EN", "Привет", "RU")]
        public void Clear_TranllationCountIsZero(string fromWord, string fromLanguage, string toWord, string toLanguage)
        {
            TranslationHandler.AddEntry(fromWord, fromLanguage, toWord, toLanguage); //we don't check if it added because it's tested in other place
            TranslationHandler.Clear();
            Assert.AreEqual(0,TranslationHandler.Translations.Count);
        }
        #endregion

        #region Remove
        [DataRow("Hello", "EN")]
        [TestMethod]
        public void Remove_HasNullParameters(string fromWord, string fromLanguage)
        {
            Assert.ThrowsException<ArgumentNullException>(() => TranslationHandler.Remove(null, fromLanguage));
            Assert.ThrowsException<ArgumentNullException>(() => TranslationHandler.Remove(fromWord, null));
        }

        [DataRow("Hello", "EN", "Привет", "RU")]
        [TestMethod]
        public void RemoveRecord_RemovesRecord(string fromWord,
                                                   string fromLanguage, string toWord,
                                                   string toLanguage)
        {
            int count= TranslationHandler.Translations.Count;
            TranslationHandler.AddEntry(fromWord, fromLanguage, toWord, toLanguage);
            TranslationHandler.Remove(fromWord,fromLanguage);
            Assert.AreEqual(count,TranslationHandler.Translations.Count);

            TranslationHandler.AddEntry(fromWord, fromLanguage, toWord, toLanguage);
            TranslationHandler.Remove(toWord, toLanguage);
            Assert.AreEqual(count, TranslationHandler.Translations.Count);
        }

        [DataRow("Hello", "EN", "Привет", "RU")]
        [TestMethod]
        public void RemoveRecord_ThrowNotFoundException(string fromWord,
                                               string fromLanguage, string toWord,
                                               string toLanguage)
        {
           // int count = TranslationHandler.Translations.Count;
            TranslationHandler.AddEntry(fromWord, fromLanguage, toWord, toLanguage);
            
            Assert.ThrowsException<Exception>(()=> TranslationHandler.Remove("a", fromLanguage));
            Assert.ThrowsException<Exception>(()=> TranslationHandler.Remove(fromWord, "a"));
        }

        #endregion

        #region Translate
        [DataRow("Hello", "EN","RU")]
        [TestMethod]
        public void Translate_HasNullParameters(string fromWord, string fromLanguage, string toLanguage)
        {
            Assert.ThrowsException<ArgumentNullException>(() => TranslationHandler.Translate(null, fromLanguage, toLanguage));
            Assert.ThrowsException<ArgumentNullException>(() => TranslationHandler.Translate(fromWord, null, toLanguage));
            Assert.ThrowsException<ArgumentNullException>(() => TranslationHandler.Translate(fromWord, fromLanguage, null));
        }


        [TestMethod]
        [DataRow("Hello1", "EN", "Привет", "RU")]
        [DataRow("Hello", "EN1", "Привет", "RU")]
        [DataRow("Hello", "EN", "Привет", "RU1")]
        public void Translate_TranllationThwowsExceptionIfNotFound(string fromWord, string fromLanguage, string toWord, string toLanguage)
        {
            TranslationHandler.AddEntry("Hello", "EN", "Привет", "RU"); 
            Assert.ThrowsException<Exception>(()=>TranslationHandler.Translate(fromWord, fromLanguage, toLanguage),$"{fromWord} {fromLanguage} {toLanguage}");        
        }

        [TestMethod]
        [DataRow("Hello", "EN", "Привет", "RU")]
        public void Translate_TranllationReturnsValidValue(string fromWord, string fromLanguage, string toWord, string toLanguage)
        {
            TranslationHandler.AddEntry(fromWord, fromLanguage, toWord, toLanguage);
            Assert.AreEqual(toWord, TranslationHandler.Translate(fromWord, fromLanguage, toLanguage));
        }


        #endregion

        #region AddEntry
        [DataRow("Hello", "EN", "Привет", "RU")]
        [TestMethod]
        public void AddNewRecord_HasNullParameters(string fromWord, string fromLanguage, string toWord, string toLanguage)
        {
            Assert.ThrowsException<ArgumentNullException>(() => TranslationHandler.AddEntry(null, fromLanguage, toWord, toLanguage));
            Assert.ThrowsException<ArgumentNullException>(() => TranslationHandler.AddEntry(fromWord, null, toWord, toLanguage));
            Assert.ThrowsException<ArgumentNullException>(() => TranslationHandler.AddEntry(fromWord, fromLanguage, null, toLanguage));
            Assert.ThrowsException<ArgumentNullException>(() => TranslationHandler.AddEntry(fromWord, fromLanguage, toWord, null));
        }

        [DataRow("Hello", "EN", "Привет", "RU1")]
        [DataRow("Hello", "EN1", "Привет", "RU")]
        [TestMethod]
        public void AddNewRecord_HasInvalidLanguage(string fromWord, string fromLanguage, string toWord, string toLanguage)
        {
            Assert.ThrowsException<ArgumentException>(() => TranslationHandler.AddEntry(fromWord, fromLanguage, toWord, toLanguage));
        }

        [TestMethod]
        [DataRow("Hello", "EN", "Привет", "RU")]
        public void AddNewRecord_AddedNewValue(string fromWord, string fromLanguage, string toWord, string toLanguage)
        {
            int count = TranslationHandler.Translations.Count;
            TranslationHandler.AddEntry(fromWord, fromLanguage, toWord, toLanguage);
            Assert.AreEqual(count+1, TranslationHandler.Translations.Count);
        }

        [TestMethod]
        [DataRow("Hello", "EN", "Привет", "RU")]
        public void AddNewRecord_SameWordIsNotAdded(string fromWord, string fromLanguage, string toWord, string toLanguage)
        {
            TranslationHandler.AddEntry(fromWord, fromLanguage, toWord, toLanguage);
            int count = TranslationHandler.Translations.Count;
            Assert.AreEqual(count , TranslationHandler.Translations.Count);
            Assert.ThrowsException<Exception>(() => TranslationHandler.AddEntry(fromWord, fromLanguage, toWord, toLanguage));
        }
        #endregion
    }
}
