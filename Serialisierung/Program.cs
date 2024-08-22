using CsvHelper;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisierung;

internal class Program
{
	static void Main(string[] args)
	{
		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new PKW(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		//File, Path, Directory
		string folderPath = Path.Combine("Output");
		string filePath = Path.Combine(folderPath, "Test.txt");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		//NewtonsoftJson();

		//SystemJson();

		//XML();

		//Binary();

		using (StreamWriter sw = new StreamWriter(filePath))
		{
			CsvWriter writer = new CsvWriter(sw, CultureInfo.CurrentCulture);
			writer.WriteRecords(fahrzeuge);
		}

		using (StreamReader sr = new StreamReader(filePath))
		{
			CsvReader reader = new CsvReader(sr, CultureInfo.CurrentCulture);
			IEnumerable<Fahrzeug> fzg = reader.GetRecords<Fahrzeug>();
		}
	}

	static void NewtonsoftJson()
	{
		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		//File, Path, Directory
		string folderPath = Path.Combine("Output");
		string filePath = Path.Combine(folderPath, "Test.txt");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		//Json
		//Zwei verschiedene Frameworks
		//Newtonsoft.Json, System.Text.Json

		///////////////////////
		/// Newtonsoft.Json ///
		///////////////////////

		//1. Serialisieren/Deserialisieren
		//string json = JsonConvert.SerializeObject(fahrzeuge);
		//File.WriteAllText(filePath, json);

		//string readJson = File.ReadAllText(filePath);
		//List<Fahrzeug> readFzg = JsonConvert.DeserializeObject<List<Fahrzeug>>(readJson);

		////2. Settings
		//JsonSerializerSettings settings = new();
		//settings.Formatting = Formatting.Indented;
		//settings.TypeNameHandling = TypeNameHandling.Objects; //Vererbung ermöglichen

		//File.WriteAllText(filePath, JsonConvert.SerializeObject(fahrzeuge, settings));

		////3. Attribute
		////JsonIgnore: Feld nicht mitspeichern
		////JsonExtensionData: Felder im Json einfangen, die in C# kein Feld haben
		////JsonRequired: Feld muss beim De-/Serialisieren vorhanden sein
		////JsonConverter: Beim De-/Serialisieren den Wert konvertieren (Typveränderungen), z.B. long zu Datum

		////4. Json per Hand
		//JToken token = JToken.Parse(readJson);
		//foreach (JToken obj in token)
		//{
		//	Console.WriteLine(obj["MaxV"]);
		//	Console.WriteLine(obj["Marke"]);
		//	Console.WriteLine("-----------");
		//}
	}

	static void SystemJson()
	{
		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new PKW(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		//File, Path, Directory
		string folderPath = Path.Combine("Output");
		string filePath = Path.Combine(folderPath, "Test.txt");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		//NewtonsoftJson();

		////////////////////////
		/// System.Text.Json ///
		////////////////////////

		//1. Serialisieren/Deserialisieren
		string json = JsonSerializer.Serialize(fahrzeuge);
		File.WriteAllText(filePath, json);

		string readJson = File.ReadAllText(filePath);
		List<Fahrzeug> readFzg = JsonSerializer.Deserialize<List<Fahrzeug>>(readJson);

		//2. Options
		JsonSerializerOptions options = new();
		options.WriteIndented = true;

		File.WriteAllText(filePath, JsonSerializer.Serialize(fahrzeuge, options));

		//3. Attribute
		//JsonDerivedType: Vererbung ermöglichen, benötigt ein Attribut pro Klasse in der Vererbungshierarchie mit einem Bezeichner

		//4. Json per Hand
		JsonDocument doc = JsonDocument.Parse(readJson);
		foreach (JsonElement element in doc.RootElement.EnumerateArray())
		{
			Console.WriteLine(element.GetProperty("MaxV").GetInt32());
			Console.WriteLine(element.GetProperty("Marke").GetInt32());
			Console.WriteLine("--------");
		}
	}

	static void XML()
	{
		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new PKW(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		//File, Path, Directory
		string folderPath = Path.Combine("Output");
		string filePath = Path.Combine(folderPath, "Test.txt");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		//NewtonsoftJson();

		//SystemJson();

		///////////
		/// XML ///
		///////////

		//1. Serialisieren/Deserialisieren
		XmlSerializer xml = new(fahrzeuge.GetType());
		using (StreamWriter sw = new StreamWriter(filePath))
			xml.Serialize(sw, fahrzeuge);

		using StreamReader sr = new StreamReader(filePath);
		List<Fahrzeug> readFzg = (List<Fahrzeug>) xml.Deserialize(sr);

		//2. Attribute
		//XmlIgnore
		//XmlAttribute: Definiert, das das gegebene Feld in der Attribut geschrieben/gelesen wird
		//XmlInclude: Vererbung

		//3. XML per Hand
		XmlDocument doc = new XmlDocument();
		sr.BaseStream.Position = 0;
		doc.Load(sr);

		foreach (XmlNode node in doc.DocumentElement)
		{
			Console.WriteLine(node.Attributes["MaxV"].InnerText);
			Console.WriteLine(node.Attributes["Marke"].InnerText);
			Console.WriteLine("----------------------");
		}
	}

	static void Binary()
	{
		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new PKW(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		//File, Path, Directory
		string folderPath = Path.Combine("Output");
		string filePath = Path.Combine(folderPath, "Test.txt");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		//NewtonsoftJson();

		//SystemJson();

		//XML();

#pragma warning disable
		BinaryFormatter formatter = new();
		using (StreamWriter sw = new StreamWriter(filePath))
		{
			formatter.Serialize(sw.BaseStream, fahrzeuge);
		}

		using StreamReader sr = new(filePath);
		List<Fahrzeug> fzg = (List<Fahrzeug>) formatter.Deserialize(sr.BaseStream);
	}
}

[DebuggerDisplay("Marke: {Marke}, MaxV: {MaxV}")]
//[JsonDerivedType(typeof(Fahrzeug), "F")]
//[JsonDerivedType(typeof(PKW), "P")]

[XmlInclude(typeof(Fahrzeug))]
[XmlInclude(typeof(PKW))]

[Serializable]
public class Fahrzeug
{
	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}

    public Fahrzeug()
    {
        
    }

	[XmlAttribute]
    public int MaxV { get; set; }

	[XmlAttribute]
	public FahrzeugMarke Marke { get; set; }

	//public Dictionary<string, object> keyValuePairs { get; set; } = new();
}

[Serializable]
public class PKW : Fahrzeug
{
	public PKW(int maxV, FahrzeugMarke marke) : base(maxV, marke)
	{
	}

    public PKW()
    {
        
    }
}

public enum FahrzeugMarke { Audi, BMW, VW }