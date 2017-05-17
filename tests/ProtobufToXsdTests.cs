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
			var tempFile = Path.GetTempFileName();
			try
			{
				var protogenPath = Path.Combine(
					currentAssemblyDirectory,
					"protobuf-to-xsd",
					"protogen.exe");
				var arguments = $"-i:\"{protoFilePath}\" -o:\"{tempFile}\" -d -t:xsd-attributes";
				Console.WriteLine($"Starting process: \"{protogenPath}\" {arguments}");

				var startInfo = new ProcessStartInfo(protogenPath, arguments)
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
				Console.WriteLine($"Exit code: {process.ExitCode}");
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
