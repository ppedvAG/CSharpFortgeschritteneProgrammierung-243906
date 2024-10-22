﻿using System.Net.Http;
using System.Windows;

namespace AsyncAwaitWPF;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i< 100; i++)
		{
			Thread.Sleep(20); //GUI Updates werden blockiert
			Info.Text += i + "\n";
		}
	}

	private void Button_Click_ContinueWith(object sender, RoutedEventArgs e)
	{
		//GUI Updates werden nicht mehr blockiert
		Task.Run(() =>
		{
			for (int i = 0; i < 100; i++)
			{
				Thread.Sleep(100);
				Dispatcher.Invoke(() => Info.Text += i + "\n"); //Mit Dispatcher müssen UI Updates von Side Threads/Tasks auf den Main Thread gelegt werden
			}
		});
	}

	private async void Button_Click_Async(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 100; i++)
		{
			await Task.Delay(20);
			Info.Text += i + "\n";
			Scroll.ScrollToEnd();
		}
	}

	private async void Request(object sender, RoutedEventArgs e)
	{
		//Aufbau:
		//- Aufgabe(n) starten
		//- Zwischenschritte (optional)
		//- Auf Ergebnisse warten

		string url = "http://www.gutenberg.org/files/54700/54700-0.txt";

		//Request starten
		using HttpClient client = new();
		Task<HttpResponseMessage> request = client.GetAsync(url);

		//Zwischenschritte
		Info.Text = "Text wird geladen...";
		ReqButton.IsEnabled = false;

		//Auf Ergebnis warten
		HttpResponseMessage msg = null;
		try
		{
			msg = await request;
		}
		catch (HttpRequestException ex)
		{
			Info.Text = ex.Message;
			ReqButton.IsEnabled = true;
			return;
		}

		///////////////////////////////////////////////////////

		//Auslesen starten
		Task<string> str = msg.Content.ReadAsStringAsync();

		//Zwischenschritte
		Info.Text = "Text wird ausgelesen...";
		await Task.Delay(1000); //künstliches Delay

		//Auf Ergebnis warten
		string s = await str;

		Info.Text = s;
		ReqButton.IsEnabled = true;
	}

	private async void Button_Click_AsyncDataSource(object sender, RoutedEventArgs e)
	{
		//Wenn eine Zahl erzeugt wird, soll diese angezeigt werden
		//Wir wissen nicht, wann die nächste Zahl kommt -> await
		AsyncDataSource ds = new();
		await foreach (int x in ds.GetNumbers()) //Wenn die Schleife läuft, warte hier auf den nächsten Wert
		{
			Info.Text += x + "\n";
		}
	}

	private async void Button_Click_Parallel_ForEachAsync(object sender, RoutedEventArgs e)
	{
		List<int> ints = Enumerable.Range(0, 100).Select(e => Random.Shared.Next()).ToList();

		//List<int> zahlen = [];
		//for (int i = 0; i < 100; i++)
		//	zahlen.Add(Random.Shared.Next());

		//Mehrere Tasks gleichzeitig starten
		//await Parallel.ForEachAsync(ints, (i, ct) =>
		//{
		//	Dispatcher.Invoke(() => Info.Text += i + "\n");
		//	return ValueTask.CompletedTask;
		//});

		//Alternative
		List<Task> tasks = [];
		foreach (int x in ints)
		{
			Task t = Task.Run(() => Dispatcher.Invoke(() => Info.Text += x + "\n"));
			tasks.Add(t);
		}
		await Task.WhenAll(tasks);
	}
}