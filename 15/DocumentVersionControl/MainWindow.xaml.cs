using System.Windows;
using System.Windows.Input;

namespace DocumentVersionControl
{
    public partial class MainWindow : Window
    {
        private ViewModels.MainViewModel ViewModel
        {
            get { return (ViewModels.MainViewModel)DataContext; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Система контроля версий документов\nВерсия 1.0", "О программе");
        }

        // Обработка горячих клавиш
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.N:
                        // Ctrl+N - Добавить версию
                        if (ViewModel.AddVersionCommand != null && ViewModel.AddVersionCommand.CanExecute(null))
                        {
                            ViewModel.AddVersionCommand.Execute(null);
                        }
                        e.Handled = true;
                        break;

                    case Key.H:
                        // Ctrl+H - Просмотреть историю
                        if (ViewModel.ViewHistoryCommand != null && ViewModel.ViewHistoryCommand.CanExecute(null))
                        {
                            ViewModel.ViewHistoryCommand.Execute(null);
                        }
                        e.Handled = true;
                        break;

                    case Key.D:
                        // Ctrl+D - Удалить версию
                        if (ViewModel.DeleteVersionCommand != null && ViewModel.DeleteVersionCommand.CanExecute(null))
                        {
                            ViewModel.DeleteVersionCommand.Execute(null);
                        }
                        e.Handled = true;
                        break;
                }
            }

            base.OnKeyDown(e);

            if (e.Key == Key.F2 && ViewModel.SelectedVersion != null)
            {
                // Начинаем редактирование текущей ячейки
                var dataGrid = FindName("VersionsDataGrid") as System.Windows.Controls.DataGrid;
                if (dataGrid != null)
                {
                    dataGrid.BeginEdit();
                }
                e.Handled = true;
            }
        }
    }
}