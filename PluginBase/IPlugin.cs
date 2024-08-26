namespace PluginBase;

/// <summary>
/// Gemeinsame Basis
/// Wird beim Client und bei den Plugins als Depedency eingefügt
/// Bei Reflection kann dann nach diesem Interface gesucht werden
/// 
/// WICHTIG: Nur eine Klasse im ganzen Plugin darf dieses Interface bekommen
/// </summary>
public interface IPlugin
{
	string Name { get; }

	string Description { get; }

	string Version { get; }

	string Author { get; }
}