using PluginBase;

namespace PluginCalculator2;

public class Calculator : IPlugin
{
	public string Name => "Einfacher Rechner";

	public string Description => "Multiplizieren und Dividieren";

	public string Version => "1.0";

	public string Author => "Lukas Kern";

	[ReflectionVisible("Multipliziere")]
	public double Mult(double x, double y) => x + y;

	[ReflectionVisible("Dividiere")]
	public double Div(double x, double y) => x - y;
}
