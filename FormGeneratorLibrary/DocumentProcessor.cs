using FormGeneratorLibrary;
using FormGeneratorLibrary.FormControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace FormGenerator
{
    public class DocumentProcessor
    {
        #region Properties
        public string TemplatePath { get; set; }
        public string ExportPath { get; set; }
        public string OrginalText { get; set; }
        public string ConstructedText { get; set; }
        public string EvaluatedText { get; set; }
        private DocumentBuilder DocumentBuilder { get; set; }
        private DocumentConstructor DocumentConstructor { get; set; }
        private DocumentEvaluator DocumentEvaluator { get; set; }
        private List<AbstractFormElement> _constructingFormElements;
        public List<AbstractFormElement> ConstructingFormElements
        {
            get
            {
                return _constructingFormElements;
            }
            set
            {
                    _constructingFormElements = value;
            }
        }
        private List<AbstractFormElement> _valuesFormElements;
        public List<AbstractFormElement> ValuesFormElements
        {
            get
            {
                return _valuesFormElements;
            }
            set
            {
                    _valuesFormElements = value;
            }
        }
        #endregion

        #region Constructors
        public DocumentProcessor()
        {
            DocumentBuilder = new DocumentBuilder();
            DocumentConstructor = new DocumentConstructor();
            DocumentEvaluator = new DocumentEvaluator();
        }
        public DocumentProcessor(string templatePath) : this()
        {
            TemplatePath = templatePath;
            if (Path.GetExtension(templatePath) == ".txt")
                OrginalText = ReadTextFromFile(templatePath);
            else if (Path.GetExtension(templatePath) == ".rtf")
                OrginalText = ReadRTFDocumentFromFile(templatePath);
        }
        #endregion

        #region Methods
        public void MapBuildingFormElements()
        {
            ConstructingFormElements = DocumentConstructor.CunstructDocument(OrginalText);
        }
        public void MapEvaluationFormElements()
        {
            ValuesFormElements = DocumentEvaluator.EvaluateDocument(ConstructedText);
        }
        public string BuildDocument(string text, List<AbstractFormElement> buildingElements)
        {
            return DocumentBuilder.BuildDocument(text, buildingElements);
        }
        public string EvaluateDocument(string text, List<AbstractFormElement> evaluatingElements)
        {
            return DocumentBuilder.EvaluateDocument(text, evaluatingElements);
        }
        public void GenerateDocument()
        {
            if(!string.IsNullOrEmpty(TemplatePath) && !string.IsNullOrEmpty(ExportPath) && TemplatePath!=ExportPath)
            {
                DocumentBuilder.CreateDocument(TemplatePath, ExportPath);
            }
        }
        public string ReadTextFromFile(string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                try
                {
                    return File.ReadAllText(path,Encoding.Default);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }
        public string ReadRTFDocumentFromFile(string path)
        {
            //TODO not working
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                try
                {
                    FlowDocument flowDocument = new FlowDocument();
                    TextRange textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);

                    using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        textRange.Load(fileStream, DataFormats.Rtf);
                    }
                    return textRange.Text;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }


        #endregion
    }
}

/*
Funkcjonalności:



{{name}}             Textfield Aproksymacja jednego bloku tekstowego na podstawie nazwy
{{block:name}} Tekstblock o nazwie name
{{date:name}}   Tworzey data picker             

Budowanie dokumentu

{{options:name#lorem ipsum#}} - Combo box lub dropdown lub Chck box o nazwie "name" który wstawia text "lorem ipsum" lub ""


 */

