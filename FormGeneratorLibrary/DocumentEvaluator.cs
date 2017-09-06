using FormGeneratorLibrary.FormControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FormGeneratorLibrary
{
    public class DocumentEvaluator
    {
        private List<string> FindValuesExpressions(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                List<string> expresions = new List<string>();
                string pattern = @"{{(.*?)}}";

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
        private List<AbstractFormElement> MapValueExpressions(List<string> valueExpressions)
        {
            if (valueExpressions != null && valueExpressions.Count > 0)
            {
                List<AbstractFormElement> valueElements = new List<AbstractFormElement>();
                foreach (var item in valueExpressions)
                {
                    Regex blockRx = new Regex("{{block:(.*?)}}");
                    Regex dateRx = new Regex("{{date:(.*?)}}");
                    if (blockRx.Match(item).Success)
                    {
                        FormTextBlock textBlock = new FormTextBlock();
                        textBlock.Name = item.Replace("{{block:", string.Empty).Replace("}}", string.Empty);
                        textBlock.Expression = item;
                        textBlock.Text = string.Empty;

                        valueElements.Add(textBlock);
                    }
                    else if (dateRx.Match(item).Success)
                    {
                        FormDate date = new FormDate();
                        date.Name = item.Replace("{{date:", string.Empty).Replace("}}", string.Empty); ;
                        date.Expression = item;
                        date.Date = DateTime.Now;

                        valueElements.Add(date);
                    }
                    else
                    {
                        FormTextBox textBox = new FormTextBox();
                        textBox.Name = item.Replace("{{", string.Empty).Replace("}}", string.Empty);
                        textBox.Expression = item;
                        textBox.Text = string.Empty;

                        valueElements.Add(textBox);
                    }
                }
                return valueElements;
            }
            return null;
        }
        internal List<AbstractFormElement> EvaluateDocument(string text)
        {
            List<string> evaluatingExpression = FindValuesExpressions(text);
            return MapValueExpressions(evaluatingExpression);
        }
    }
}
