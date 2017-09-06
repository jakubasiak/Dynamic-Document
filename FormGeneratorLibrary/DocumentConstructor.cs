using FormGeneratorLibrary.FormControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FormGeneratorLibrary
{
    public class DocumentConstructor
    {
        private List<string> FindDocumentConstructingExpression(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                List<string> expresions = new List<string>();
                string pattern = @"{{options:(\w\S*)#(.*?)((\s\r\n)*?)#}}";

                Regex regex = new Regex(pattern, RegexOptions.Singleline);
                MatchCollection mc = regex.Matches(text);
                foreach (var math in mc)
                {
                    expresions.Add(math.ToString());
                }

                return expresions.Distinct().ToList();
            }
            return null;
        }
        private List<AbstractFormElement> MapDocumentConstructingExpressions(List<string> constructingExpressions)
        {
            if (constructingExpressions != null && constructingExpressions.Count > 0)
            {
                List<AbstractFormElement> constructingElements = new List<AbstractFormElement>();
                foreach (string item in constructingExpressions)
                {
                    string[] splitedExpression = item.Replace("{{options:", string.Empty).Replace("#}}", string.Empty).Split('#');
                    string name = splitedExpression[0];

                    if (splitedExpression.Length < 3)
                    {
                        FormCheckBox checkBox = new FormCheckBox();
                        checkBox.Name = name;
                        checkBox.Expression = item;
                        checkBox.Text = splitedExpression[1];
                        checkBox.IsChecked = false;

                        constructingElements.Add(checkBox);
                    }
                    else if (splitedExpression.Where((x, index) => index > 0).Where(s => s.Length > 50).Count() > 0)
                    {
                        FormRadioButton comboBox = new FormRadioButton();
                        comboBox.Name = name;
                        comboBox.Expression = item;
                        for (int i = 1; i < splitedExpression.Length; i++)
                        {
                            comboBox.Options.Add(splitedExpression[i]);
                        }
                        comboBox.SelectedIndex = 0;
                        constructingElements.Add(comboBox);
                    }
                    else
                    {
                        FormDropDown dropDown = new FormDropDown();
                        dropDown.Name = name;
                        dropDown.Expression = item;
                        for (int i = 1; i < splitedExpression.Length; i++)
                        {
                            dropDown.Options.Add(splitedExpression[i]);
                        }
                        dropDown.SelectedIndex = 0;
                        constructingElements.Add(dropDown);
                    }
                }
                return constructingElements;
            }
            return null;
        }

        internal List<AbstractFormElement> CunstructDocument(string text)
        {
            List<string> constructionExpressions = FindDocumentConstructingExpression(text);

            return MapDocumentConstructingExpressions(constructionExpressions);
        }
    }
}
