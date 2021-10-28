using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SchtormRun.Controls.Pages.CliPages
{
    /// <summary>
    /// Логика взаимодействия для ChooseListPage.xaml
    /// </summary>
    public partial class ChooseListPage : Page
    {
        public delegate void SelectionChangedHandler(string selectedText);
        public event SelectionChangedHandler SelectionChanged;

        private List<string> _chooseList;

        public List<string> ChooseList
        {
            get => _chooseList;
            set
            {
                _chooseList = value;
                FillListBox();
            }
        }

        public ChooseListPage(List<string> chooseList)
        {
            InitializeComponent();
            ChooseList = chooseList;

            ItemsListBox.SelectionChanged += (o, e) => 
                SelectionChanged?.Invoke((o as ListBox).SelectedItem.ToString());
        }

        private void FillListBox()
        {
            ItemsListBox.Items.Clear();
            if (_chooseList == null || _chooseList.Count == 0)
                return;

            foreach (var item in _chooseList)
                ItemsListBox.Items.Add(item);
        }
    }
}
