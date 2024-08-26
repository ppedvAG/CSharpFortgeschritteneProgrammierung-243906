using PluginBase;

namespace PluginCalculator;

public class Calculator : IPlugin
{
	public string Name => "Einfacher Rechner";

	public string Description => "Addieren und Subtrahieren";

	public string Version => "1.0";

	public string Author => "Lukas Kern";

	[ReflectionVisible("Addiere")]
	public double Add(double x, double y) => x + y;

	[ReflectionVisible("Subtrahiere")]
	public double Sub(double x, double y) => x - y;
}
