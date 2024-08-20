using System.Windows;

namespace DelegatesWPF;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		Environment.Exit(0);
	}
}