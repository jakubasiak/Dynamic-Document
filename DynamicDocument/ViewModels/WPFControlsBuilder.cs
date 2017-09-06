using FormGeneratorLibrary.FormControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DynamicDocument.ViewModels
{
    public class WPFControlsBuilder
    {
        public List<UIElement> BuildUIElements(List<AbstractFormElement> list)
        {
            Thickness defaultMargin = new Thickness(20, 5, 20, 5);
            List<UIElement> uiList = new List<UIElement>();
            foreach (var item in list)
            {
                if(item is FormCheckBox)
                {
                    FormCheckBox formCheckBox = item as FormCheckBox;

                    Grid grid = new Grid();

                    ColumnDefinition columnDefinition1 = new ColumnDefinition();
                    columnDefinition1.Width = new GridLength(1.0, GridUnitType.Star);
                    ColumnDefinition columnDefinition2 = new ColumnDefinition();
                    columnDefinition2.Width = new GridLength(1.0, GridUnitType.Star);

                    grid.ColumnDefinitions.Add(columnDefinition1);
                    grid.ColumnDefinitions.Add(columnDefinition2);

                    CheckBox checkBox = new CheckBox();
                    checkBox.Margin = defaultMargin;

                    Binding isChecked = new Binding("IsChecked");
                    isChecked.Source = formCheckBox;
                    isChecked.Mode = BindingMode.TwoWay;
                    checkBox.SetBinding(CheckBox.IsCheckedProperty, isChecked);

                    TextBlock textBlock = new TextBlock();
                    textBlock.TextWrapping = TextWrapping.Wrap;
                    textBlock.Text = formCheckBox.Text;

                    checkBox.Content = textBlock;

                    Grid.SetColumnSpan(checkBox, 2);

                    grid.Children.Add(checkBox);

                    uiList.Add(grid);
                }
                if (item is FormDropDown)
                {
                    FormDropDown formDropDown = item as FormDropDown;

                    Grid grid = new Grid();

                    ColumnDefinition columnDefinition1 = new ColumnDefinition();
                    columnDefinition1.Width = new GridLength(1.0, GridUnitType.Star);
                    ColumnDefinition columnDefinition2 = new ColumnDefinition();
                    columnDefinition2.Width = new GridLength(2.0, GridUnitType.Star);

                    grid.ColumnDefinitions.Add(columnDefinition1);
                    grid.ColumnDefinitions.Add(columnDefinition2);
                    grid.HorizontalAlignment = HorizontalAlignment.Stretch;
                    grid.VerticalAlignment = VerticalAlignment.Stretch;

                    TextBlock textBlock = new TextBlock();
                    textBlock.Margin = defaultMargin;
                    textBlock.FontWeight = FontWeights.Bold;

                    Binding text = new Binding("Name");
                    text.Source = formDropDown;
                    text.Mode = BindingMode.OneWay;
                    textBlock.SetBinding(TextBlock.TextProperty, text);

                    ComboBox comboBox = new ComboBox();
                    comboBox.Margin = defaultMargin;

                    Binding options = new Binding("Options");
                    options.Source = formDropDown;
                    options.Mode = BindingMode.TwoWay;
                    comboBox.SetBinding(ComboBox.ItemsSourceProperty, options);

                    Binding selectedIndex = new Binding("SelectedIndex");
                    selectedIndex.Source = formDropDown;
                    selectedIndex.Mode = BindingMode.TwoWay;
                    comboBox.SetBinding(ComboBox.SelectedIndexProperty, selectedIndex);

                    Binding selectedValue = new Binding("SelectedValue");
                    selectedValue.Source = formDropDown;
                    selectedValue.Mode = BindingMode.OneWay;
                    comboBox.SetBinding(ComboBox.SelectedValueProperty, selectedValue);

                    Grid.SetColumn(textBlock, 0);
                    Grid.SetColumn(comboBox, 1);

                    grid.Children.Add(textBlock);
                    grid.Children.Add(comboBox);

                    uiList.Add(grid);
                }
                if(item is FormRadioButton)
                {
                    FormRadioButton formRadioButton = item as FormRadioButton;

                    StackPanel stackPanel = new StackPanel();

                    stackPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
                    stackPanel.VerticalAlignment = VerticalAlignment.Stretch;

                    for (int i = 0; i < formRadioButton.Options.Count; i++)
                    {
                        TextBlock textBlock = new TextBlock();
                        textBlock.TextWrapping = TextWrapping.Wrap;
                        textBlock.Text = formRadioButton.Options[i];

                        RadioButton radioButton = new RadioButton();
                        radioButton.Margin = defaultMargin;

                        radioButton.Content = textBlock;
                        radioButton.GroupName = item.Name;
                        radioButton.Checked += (sender, args) => 
                        {
                            formRadioButton.SelectedIndex = formRadioButton.Options.IndexOf(textBlock.Text);
                        };
                        if (i == formRadioButton.SelectedIndex)
                            radioButton.IsChecked = true;

                        RowDefinition rowDefinition = new RowDefinition();
                        stackPanel.Children.Add(radioButton);
                    }

                    GroupBox groupBox = new GroupBox();
                    groupBox.Header = item.Name;
                    groupBox.Content = stackPanel;
                    groupBox.Margin = defaultMargin;

                    uiList.Add(groupBox);
                }
                if(item is FormTextBox)
                {
                    FormTextBox formTextBox = item as FormTextBox;

                    Grid grid = new Grid();

                    ColumnDefinition columnDefinition1 = new ColumnDefinition();
                    columnDefinition1.Width = new GridLength(1.0, GridUnitType.Star);
                    ColumnDefinition columnDefinition2 = new ColumnDefinition();
                    columnDefinition2.Width = new GridLength(2.0, GridUnitType.Star);

                    grid.ColumnDefinitions.Add(columnDefinition1);
                    grid.ColumnDefinitions.Add(columnDefinition2);
                    grid.HorizontalAlignment = HorizontalAlignment.Stretch;
                    grid.VerticalAlignment = VerticalAlignment.Stretch;

                    TextBlock textBlock = new TextBlock();
                    textBlock.FontWeight = FontWeights.Bold;
                    textBlock.Margin = defaultMargin;

                    Binding label = new Binding("Name");
                    label.Source = formTextBox;
                    label.Mode = BindingMode.OneWay;
                    textBlock.SetBinding(TextBlock.TextProperty, label);

                    TextBox textBox = new TextBox();
                    textBox.Margin = defaultMargin;

                    Binding text= new Binding("Text");
                    text.Source = formTextBox;
                    text.Mode = BindingMode.TwoWay;
                    text.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    textBox.SetBinding(TextBox.TextProperty, text);

                    textBox.Text = item.Name;

                    Grid.SetColumn(textBlock, 0);
                    Grid.SetColumn(textBox, 1);

                    grid.Children.Add(textBlock);
                    grid.Children.Add(textBox);

                    uiList.Add(grid);
                }
                if (item is FormTextBlock)
                {
                    FormTextBlock formTextBlock = item as FormTextBlock;

                    StackPanel stackPanel = new StackPanel();

                    stackPanel.Orientation = Orientation.Vertical;
                    stackPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
                    stackPanel.VerticalAlignment = VerticalAlignment.Stretch;
                    stackPanel.Margin = defaultMargin;

                    TextBlock textBlock = new TextBlock();
                    textBlock.FontWeight = FontWeights.Bold;
                    textBlock.TextWrapping = TextWrapping.Wrap;

                    Binding label = new Binding("Name");
                    label.Source = formTextBlock;
                    label.Mode = BindingMode.OneWay;
                    textBlock.SetBinding(TextBlock.TextProperty, label);

                    TextBox textBox = new TextBox();
                    textBox.MinLines = 5;
                    textBox.AcceptsReturn = true;
                    textBox.AcceptsTab = true;
                    textBox.TextWrapping = TextWrapping.Wrap;

                    Binding text = new Binding("Text");
                    text.Source = formTextBlock;
                    text.Mode = BindingMode.TwoWay;
                    text.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    textBox.SetBinding(TextBox.TextProperty, text);

                    textBox.Text = item.Name;

                    stackPanel.Children.Add(textBlock);
                    stackPanel.Children.Add(textBox);

                    uiList.Add(stackPanel);
                }
                if (item is FormDate)
                {
                    FormDate formDate = item as FormDate;

                    Grid grid = new Grid();

                    ColumnDefinition columnDefinition1 = new ColumnDefinition();
                    columnDefinition1.Width = new GridLength(1.0, GridUnitType.Star);
                    ColumnDefinition columnDefinition2 = new ColumnDefinition();
                    columnDefinition2.Width = new GridLength(2.0, GridUnitType.Star);

                    grid.ColumnDefinitions.Add(columnDefinition1);
                    grid.ColumnDefinitions.Add(columnDefinition2);
                    grid.HorizontalAlignment = HorizontalAlignment.Stretch;
                    grid.VerticalAlignment = VerticalAlignment.Stretch;

                    TextBlock textBlock = new TextBlock();
                    textBlock.FontWeight = FontWeights.Bold;
                    textBlock.Margin = defaultMargin;
                    textBlock.TextWrapping = TextWrapping.Wrap;

                    Binding label = new Binding("Name");
                    label.Source = formDate;
                    label.Mode = BindingMode.OneWay;
                    textBlock.SetBinding(TextBlock.TextProperty, label);

                    DatePicker datePicker = new DatePicker();
                    datePicker.Margin = defaultMargin;

                    Binding date = new Binding("Date");
                    date.Source = formDate;
                    date.Mode = BindingMode.TwoWay;
                    date.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    datePicker.SetBinding(DatePicker.SelectedDateProperty, date);

                    Grid.SetColumn(textBlock, 0);
                    Grid.SetColumn(datePicker, 1);

                    grid.Children.Add(textBlock);
                    grid.Children.Add(datePicker);

                    uiList.Add(grid);
                }
            }
            return uiList;
        }
    }
}
