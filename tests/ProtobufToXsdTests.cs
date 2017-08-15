using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using NUnit.Framework;
using tests.protobuf_fields;

namespace tests
{
	[UseReporter(typeof(DiffReporter))]
	[TestFixture]
	public class ProtobufToXsdTests
	{
		private string ConvertProtobufToXsd(string protoFilePath, string runMode)
		{
			var currentAssemblyDirectory = Path.GetDirectoryName(new Uri(typeof(ProtobufToXsdTests).Assembly.CodeBase).AbsolutePath);
			var tempFile = Path.GetTempFileName();
			try
			{
				var protogenPath = Path.Combine(
					currentAssemblyDirectory,
					"protobuf-to-xsd",
					"protogen.exe");
				var arguments = $"-i:\"{protoFilePath}\" -o:\"{tempFile}\" -d -t:{runMode}";
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
				process.OutputDataReceived += (s, o) => { Console.WriteLine(o.Data); };
				process.ErrorDataReceived += (s, o) => { Console.WriteLine(o.Data); };
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
		public void FieldsConverting_XsdAttributes_CheckContent()
		{
			var result = ConvertProtobufToXsd("protobuf-fields.proto", "xsd-attributes");
			Approvals.VerifyXml(result);
		}

		[Test]
		public void FieldsConverting_Xsd_CheckContent()
		{
			var result = ConvertProtobufToXsd("protobuf-fields.proto", "xsd");
			Approvals.VerifyXml(result);
		}

		[Test]
		public void FieldsConverting_Xsd_CheckSerializedXmlCorrespondsToGeneratedXsd()
		{
			var generatedSchema = ConvertProtobufToXsd("protobuf-fields.proto", "xsd");
			var root = GetProtobufClassInstance();
			var rootXml = SerializeProtobufClassInstanceToXml(root);
			var errors = ValidateXmlWithXsdSchema(rootXml, generatedSchema);
			Assert.That(errors.Count, Is.EqualTo(0));
		}

		[Test]
		public void GetProtobufXml()
		{
			var root = GetProtobufClassInstance();
			var rootXml = SerializeProtobufClassInstanceToXml(root);
			Console.WriteLine(rootXml);
		}

		private static string SerializeProtobufClassInstanceToXml(RootElement root)
		{
			var serializer = new XmlSerializer(typeof(RootElement));
			var memoryStream = new MemoryStream();
			serializer.Serialize(memoryStream, root);
			memoryStream.Seek(0, SeekOrigin.Begin);
			return Encoding.UTF8.GetString(memoryStream.ToArray());
		}

		private static List<string> ValidateXmlWithXsdSchema(string rootXml, string generatedSchema)
		{
			var document = XDocument.Parse(generatedSchema);
			var xmlNameTable = new NameTable();
			var namespaceResolver = new XmlNamespaceManager(xmlNameTable);
			namespaceResolver.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");
			var complexElements = document.XPathSelectElements("/*/*", namespaceResolver).JoinStringsWith(x => x.ToString(), Environment.NewLine);
			var newSchema = @"<xs:schema xmlns:xs=""http://www.w3.org/2001/XMLSchema"">"
				+ Environment.NewLine + @"<xs:element name=""RootElement"" type=""RootElement"" />"
				+ Environment.NewLine + complexElements
				+ Environment.NewLine + @"</xs:schema>";

			Console.WriteLine(newSchema);
			Console.WriteLine(rootXml);

			var xmlSchemaSet = new XmlSchemaSet();
			xmlSchemaSet.Add(XmlSchema.Read(new StringReader(newSchema), (o, e) => Console.WriteLine(e.Message)));
			var settings = new XmlReaderSettings
			{
				ValidationType = ValidationType.Schema,
				Schemas = xmlSchemaSet,
			};

			var errors = new List<string>();
			settings.ValidationEventHandler += (sender, args) =>
			{
				errors.Add(args.Message);
			};

			try
			{
				using (var validatingReader = XmlReader.Create(XDocument.Parse(rootXml).CreateReader(), settings))
				{
					while (validatingReader.Read()) { }
				}
			}
			catch (XmlSchemaException ex)
			{
				errors.Add($"XSD schema validation failed ({ex.SourceUri}, Line: {ex.LineNumber}, Position: {ex.LinePosition}): {ex.Message}");
			}

			foreach (var error in errors)
			{
				Console.WriteLine(error);
			}
			return errors;
		}

		private static RootElement GetProtobufClassInstance()
		{
			var root = new RootElement
			{
				SimpleRequired = "simple-required",
				SimpleOptional = "simple-optional",
				SimpleRequiredWithDefault = "simple-required-with-default",
				SimpleOptionalWithDefault = "simple-optional-with-default",
				EnumeratedRequiredWithDefault = EnumerationElement.FirstValue,
				EnumeratedOptionalWithDefault = EnumerationElement.SecondValue,
				EnumeratedRequired = EnumerationElement.FirstValue,
				EnumeratedOptional = EnumerationElement.SecondValue,
				ComplexRequired = new ChildElement {ChildValue = "complex-required-child-value"},
				ComplexOptional = new ChildElement {ChildValue = "complex-optional-child-value"},
				SimpleRepeated = {"first", "second"},
				EnumeratedRepeated = {EnumerationElement.FirstValue, EnumerationElement.SecondValue},
				ComplexRepeated =
				{
					new ChildElement {ChildValue = "child-value-1"},
					new ChildElement {ChildValue = "child-value-2"}
				}
			};
			return root;
		}
	}
}
