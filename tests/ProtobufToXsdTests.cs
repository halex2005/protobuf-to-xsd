using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace tests
{
	[UseReporter(typeof(DiffReporter))]
	[TestFixture]
	public class ProtobufToXsdTests
	{
		private string ConvertProtobufToXsd(string protoFilePath)
		{
			var currentAssemblyDirectory = Path.GetDirectoryName(new Uri(typeof(ProtobufToXsdTests).Assembly.CodeBase).AbsolutePath);
			var protogenPath = Path.Combine(
				currentAssemblyDirectory,
				"protobuf-to-xsd",
				"protogen.exe");
			var tempFile = Path.GetTempFileName();
			try
			{
				var startInfo = new ProcessStartInfo(protogenPath, $"-i:{protoFilePath} -o:{tempFile} -d -t:xsd-attributes")
				{
					CreateNoWindow = true,
					UseShellExecute = false,
					WorkingDirectory = currentAssemblyDirectory,
					RedirectStandardError = true,
					RedirectStandardOutput = true,
				};

				var process = Process.Start(startInfo);
				process.BeginErrorReadLine();
				process.BeginOutputReadLine();
				process.OutputDataReceived += (s, o) => { Console.Write(o.Data); };
				process.ErrorDataReceived += (s, o) => { Console.Write(o.Data); };
				process.WaitForExit();
				if (process.ExitCode != 0)
					return $"Process returned code {process.ExitCode}";
				return File.ReadAllText(tempFile);
			}
			finally
			{
				File.Delete(tempFile);
			}
		}

		[Test]
		public void FieldsConvertingTests()
		{
			var result = ConvertProtobufToXsd("protobuf-fields.proto");
			Approvals.VerifyXml(result);
		}
	}
}
