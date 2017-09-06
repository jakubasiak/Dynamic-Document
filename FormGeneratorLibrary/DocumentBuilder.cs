using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;


namespace FormGeneratorLibrary.FormControls
{
    public class DocumentBuilder
    {
        public string BuiltDocument { get; set; }
        public string EvaluatedDocument { get; set; }
        public List<KeyValuePair<string, string>> BuiltElements { get; set; }
        public List<KeyValuePair<string, string>> ValueElements { get; set; }

        public DocumentBuilder()
        {
            BuiltElements = new List<KeyValuePair<string, string>>();
            ValueElements = new List<KeyValuePair<string, string>>();
        }
        public string BuildDocument(string text, List<AbstractFormElement> buildingElements)
        {
            if (buildingElements != null && buildingElements.Count > 0)
            {
                BuiltElements.Clear();
                StringBuilder sb = new StringBuilder(text);
                foreach (var item in buildingElements)
                {
                    BuiltElements.Add(new KeyValuePair<string, string>(item.Expression, item.GetValue()));
                    sb.Replace(item.Expression, "[b]" + item.GetValue() + "[/b]");
                }
                BuiltDocument = sb.ToString();
                return BuiltDocument;
            }
            else
            {
                BuiltDocument = text;
                return text;
            }
        }
        public string EvaluateDocument(string text, List<AbstractFormElement> evaluatingElements)
        {
            if (evaluatingElements != null && evaluatingElements.Count > 0)
            {
                ValueElements.Clear();
                StringBuilder sb = new StringBuilder(text);
                foreach (var item in evaluatingElements)
                {
                    ValueElements.Add(new KeyValuePair<string, string>(item.Expression, item.GetValue()));
                    sb.Replace(item.Expression, "[b]" + item.GetValue() + "[/b]");
                }
                EvaluatedDocument = sb.ToString();
                return EvaluatedDocument;
            }
            else
            {
                EvaluatedDocument = text;
                return text;
            }
        }
        public bool CreateDocument(string templatePath, string exportPath)
        {
            try
            {

                if (Path.GetExtension(templatePath) == ".rtf")
                {
                    FlowDocument flowDocument = new FlowDocument();
                    TextRange textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);

                    using (FileStream fileStream = File.Open(templatePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        textRange.Load(fileStream, DataFormats.Rtf);
                    }
                    if (BuiltElements != null && BuiltElements.Count > 0)
                    {
                        foreach (var item in BuiltElements)
                        {
                            textRange.Text = textRange.Text.Replace(item.Key, item.Value);
                        }
                    }
                    if (ValueElements != null && ValueElements.Count > 0)
                    {
                        foreach (var item in ValueElements)
                        {
                            textRange.Text = textRange.Text.Replace(item.Key, item.Value);
                        }
                    }
                    using (FileStream fileStream = File.Create(exportPath, 1000000, FileOptions.Asynchronous))
                    {
                        textRange.Save(fileStream, DataFormats.Rtf);
                    }
                }
                else if (Path.GetExtension(templatePath) == ".txt")
                {
                    StringBuilder str =  new StringBuilder(File.ReadAllText(templatePath,Encoding.Default));
                    if (BuiltElements != null && BuiltElements.Count > 0)
                    {
                        foreach (var item in BuiltElements)
                        {
                            str = str.Replace(item.Key, item.Value);
                        }
                    }
                    if (ValueElements != null && ValueElements.Count > 0)
                    {
                        foreach (var item in ValueElements)
                        {
                            str = str.Replace(item.Key, item.Value);
                        }
                    }
                    File.WriteAllText(exportPath, str.ToString(),Encoding.Default);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return true;
        }
    }
}
