using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormGenerator;
using System.Collections.Generic;

namespace FormGeneratorLibrary.Tests
{
    [TestClass]
    public class DocumentProcessorTests
    {
    //    public const string path = @"c:\users\kubak\onedrive\dokumenty\visual studio 2017\Projects\FormGenerator\FormGeneratorLibrary.Tests\TestTextFile.txt";
    //    [TestMethod]
    //    public void VerifyPublicProperties()
    //    {
    //        //Arrange
    //        //Act
    //        DocumentProcessor dp = new DocumentProcessor(path);
    //        //Assert
    //        Assert.AreEqual(path, dp.TemplatePath,"Incorrect file path");
    //    }
    //    [TestMethod]
    //    public void VerifyReadTextFromFile()
    //    {
    //        //Arrange
    //        string textInFile = System.IO.File.ReadAllText(path);

    //        DocumentProcessor correctdp = new DocumentProcessor(path);
    //        var privateObject1 = new PrivateObject(correctdp);

    //        DocumentProcessor incorrectdp = new DocumentProcessor("sdsd");
    //        var privateObject2 = new PrivateObject(incorrectdp);

    //        //Act
    //        var correctText = privateObject1.Invoke("ReadTextFromFile");
    //        var incorrectText = privateObject2.Invoke("ReadTextFromFile");

    //        //Assert
    //        Assert.AreEqual(textInFile, correctText, "Incorrect file reading");
    //        Assert.IsNull(incorrectText);
    //    }
    //    [TestMethod]
    //    public void VerifyFindDocumentBuildingExpressions()
    //    {
    //        //Arrange
    //        var expectedString1 = "{{check:name(\"lorem ipsum\")}}";
    //        var expectedString2 = "{{combo:name(\"lorem\", \"ipsum\")}}";
    //        DocumentProcessor dp = new DocumentProcessor(path);
    //        dp.GetBuildingFormElements();
    //        var privateObject = new PrivateObject(dp);

    //        //Act
    //        List<string> listOfMathes = (List<string>) privateObject.Invoke("FindDocumentBuildingExpressions");

    //        //Assert
    //        Assert.AreEqual(listOfMathes[0], expectedString1);
    //        Assert.AreEqual(listOfMathes[1], expectedString2);
    //    }
    //    [TestMethod]
    //    public void VerifyFindValuesExpressions()
    //    {
    //        //Arrange
    //        var expectedString1 = "{{name}}";
    //        var expectedString2 = "{{dropdown:name(\"lorem ghhfgh\", \"ipsum\")}}";
    //        var expectedString3 = "{{block:name}}";
    //        var expectedString4 = "{{date:name}}";

    //        DocumentProcessor dp = new DocumentProcessor(path);
    //        dp.GetBuildingFormElements();
    //        var privateObject = new PrivateObject(dp);

    //        //Act
    //        List<string> listOfMathes = (List<string>)privateObject.Invoke("FindValuesExpressions");

    //        //Assert
    //        Assert.AreEqual(listOfMathes[0], expectedString1);
    //        Assert.AreEqual(listOfMathes[1], expectedString2);
    //        Assert.AreEqual(listOfMathes[2], expectedString3);
    //        Assert.AreEqual(listOfMathes[3], expectedString4);
    //    }

    }
}
