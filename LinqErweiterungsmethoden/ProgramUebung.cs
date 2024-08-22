using System.Diagnostics;
using System.Text.Json;

internal class Program
{
	static void Main(string[] args)
	{
		#region File lesen
		string readJson = File.ReadAllText(@"..\..\..\Personen.json");
		List<Person> personen = JsonSerializer.Deserialize<List<Person>>(readJson)!;
		#endregion
		
		//Hier eigenen Code schreiben
	}
}

///////////////////////////////////////////////////////////////////////////////

[DebuggerDisplay("Person - ID: {ID}, Vorname: {Vorname}, Nachname: {Nachname}, GebDat: {Geburtsdatum.ToString(\"yyyy.MM.dd\")}, Alter: {Alter}, " +
	"Jobtitel: {Job.Titel}, Gehalt: {Job.Gehalt}, Einstellungsdatum: {Job.Einstellungsdatum.ToString(\"yyyy.MM.dd\")}")]
public record Person(int ID, string Vorname, string Nachname, DateTime Geburtsdatum, int Alter, Beruf Job, List<string> Hobbies);

public record Beruf(string Titel, int Gehalt, DateTime Einstellungsdatum);

///////////////////////////////////////////////////////////////////////////////