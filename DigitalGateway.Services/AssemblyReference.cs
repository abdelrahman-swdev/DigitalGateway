using System.Reflection;

namespace DigitalGateway.Services;

public static class AssemblyReference
{
	public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
