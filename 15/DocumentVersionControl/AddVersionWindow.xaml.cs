using System;
using System.Windows;
using Microsoft.Win32;

namespace DocumentVersionControl
{
    public partial class AddVersionWindow : Window
    {
        public string VersionNumber { get; private set; }
        public string Description { get; private set; }
        public string FilePath { get; private set; }
        public bool IsConfirmed { get; private set; }

        public AddVersionWindow()
        {
            InitializeComponent();
            IsConfirmed = false;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите файл документа";
            openFileDialog.Filter = "Все файлы (*.*)|*.*|PDF файлы (*.pdf)|*.pdf|DOCX файлы (*.docx)|*.docx";

            if (openFileDialog.ShowDialog() == true)
            {
                FilePathBox.Text = openFileDialog.FileName;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(VersionNumberBox.Text))
            {
                MessageBox.Show("Введите номер версии!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            VersionNumber = VersionNumberBox.Text;
            Description = DescriptionBox.Text;
            FilePath = FilePathBox.Text;
            IsConfirmed = true;
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            IsConfirmed = false;
            this.Close();
        }
    }
}