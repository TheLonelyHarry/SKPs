using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ShowKeyPlusWPF
{
	// Token: 0x02000024 RID: 36
	internal class PKChecker
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00005802 File Offset: 0x00003A02
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00005809 File Offset: 0x00003A09
		public static byte[] BPrivateKey { get; private set; } = new byte[]
		{
			254,
			49,
			152,
			117,
			251,
			72,
			132,
			134,
			156,
			243,
			241,
			206,
			153,
			168,
			144,
			100,
			171,
			87,
			31,
			202,
			71,
			4,
			80,
			88,
			48,
			36,
			226,
			20,
			98,
			135,
			121,
			160
		};

		// Token: 0x0600014F RID: 335 RVA: 0x00005814 File Offset: 0x00003A14
		[DebuggerStepThrough]
		public Task<List<string>> CheckProductKeyAsync(string ProductKey, string Edition, bool Check)
		{
			PKChecker.<CheckProductKeyAsync>d__5 <CheckProductKeyAsync>d__ = new PKChecker.<CheckProductKeyAsync>d__5();
			<CheckProductKeyAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<string>>.Create();
			<CheckProductKeyAsync>d__.<>4__this = this;
			<CheckProductKeyAsync>d__.ProductKey = ProductKey;
			<CheckProductKeyAsync>d__.Edition = Edition;
			<CheckProductKeyAsync>d__.Check = Check;
			<CheckProductKeyAsync>d__.<>1__state = -1;
			<CheckProductKeyAsync>d__.<>t__builder.Start<PKChecker.<CheckProductKeyAsync>d__5>(ref <CheckProductKeyAsync>d__);
			return <CheckProductKeyAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005870 File Offset: 0x00003A70
		private void CleanUp(string dir)
		{
			string[] files = Directory.GetFiles(dir, "*.xrm-ms", SearchOption.TopDirectoryOnly);
			foreach (string path in files)
			{
				bool flag = File.Exists(path);
				if (flag)
				{
					File.Delete(path);
				}
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000058B8 File Offset: 0x00003AB8
		private string GetString(byte[] bytes, int index)
		{
			int num = index;
			while (bytes[num] != 0 || bytes[num + 1] > 0)
			{
				num++;
			}
			return Encoding.ASCII.GetString(bytes, index, num - index).Replace("\0", "");
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005904 File Offset: 0x00003B04
		internal string GetCount(string pid)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("ActivationRequest", "http://www.microsoft.com/DRM/SL/BatchActivationRequest/1.0");
			xmlDocument.AppendChild(xmlElement);
			XmlElement xmlElement2 = xmlDocument.CreateElement("VersionNumber", xmlElement.NamespaceURI);
			xmlElement2.InnerText = "3.0";
			xmlElement.AppendChild(xmlElement2);
			XmlElement xmlElement3 = xmlDocument.CreateElement("RequestType", xmlElement.NamespaceURI);
			xmlElement3.InnerText = "2";
			xmlElement.AppendChild(xmlElement3);
			XmlElement xmlElement4 = xmlDocument.CreateElement("Requests", xmlElement.NamespaceURI);
			XmlElement xmlElement5 = xmlDocument.CreateElement("Request", xmlElement4.NamespaceURI);
			XmlElement xmlElement6 = xmlDocument.CreateElement("PID", xmlElement5.NamespaceURI);
			xmlElement6.InnerText = pid;
			xmlElement5.AppendChild(xmlElement6);
			xmlElement4.AppendChild(xmlElement5);
			xmlElement.AppendChild(xmlElement4);
			byte[] bytes = Encoding.Unicode.GetBytes(xmlDocument.InnerXml);
			string newValue = Convert.ToBase64String(bytes);
			HMACSHA256 hmacsha = new HMACSHA256
			{
				Key = PKChecker.BPrivateKey
			};
			string newValue2 = Convert.ToBase64String(hmacsha.ComputeHash(bytes));
			string text = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Body><BatchActivate xmlns=\"http://www.microsoft.com/BatchActivationService\"><request><Digest>REPLACEME1</Digest><RequestXml>REPLACEME2</RequestXml></request></BatchActivate></soap:Body></soap:Envelope>";
			text = text.Replace("REPLACEME1", newValue2);
			text = text.Replace("REPLACEME2", newValue);
			XmlDocument xmlDocument2 = new XmlDocument();
			xmlDocument2.LoadXml(text);
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://activation.sls.microsoft.com/BatchActivation/BatchActivation.asmx");
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "text/xml; charset=\"utf-8\"";
			httpWebRequest.Headers.Add("SOAPAction", "http://www.microsoft.com/BatchActivationService/BatchActivate");
			httpWebRequest.Host = "activation.sls.microsoft.com";
			try
			{
				using (Stream requestStream = httpWebRequest.GetRequestStream())
				{
					xmlDocument2.Save(requestStream);
				}
			}
			catch (WebException ex)
			{
				Resource resource = new Resource();
				bool flag = ex.Status.ToString().Contains("Failure");
				if (flag)
				{
					return resource.GetResourceString("txtErrorActivationServer");
				}
				return ex.Message;
			}
			IAsyncResult asyncResult = httpWebRequest.BeginGetResponse(null, null);
			asyncResult.AsyncWaitHandle.WaitOne();
			string s;
			using (WebResponse webResponse = httpWebRequest.EndGetResponse(asyncResult))
			{
				using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
				{
					s = streamReader.ReadToEnd();
				}
			}
			string result;
			using (XmlReader xmlReader = XmlReader.Create(new StringReader(s)))
			{
				xmlReader.ReadToFollowing("ResponseXml");
				string text2 = xmlReader.ReadElementContentAsString();
				text2 = text2.Replace("&gt;", ">");
				text2 = text2.Replace("&lt;", "<");
				text2 = text2.Replace("utf-16", "utf-8");
				using (XmlReader xmlReader2 = XmlReader.Create(new StringReader(text2)))
				{
					xmlReader2.ReadToFollowing("ActivationRemaining");
					string text3 = xmlReader2.ReadElementContentAsString();
					bool flag2 = Convert.ToInt32(text3) < 0;
					if (flag2)
					{
						xmlReader2.ReadToFollowing("ErrorCode");
						string a = xmlReader2.ReadElementContentAsString();
						bool flag3 = a == "0x67";
						if (flag3)
						{
							return "Blocked";
						}
					}
					result = text3;
				}
			}
			return result;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005CAC File Offset: 0x00003EAC
		internal string GetProductDescription(string pkey, string aid, string edi)
		{
			bool flag = edi == null;
			if (flag)
			{
				throw new ArgumentNullException("edi");
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(pkey);
			Stream inStream = new MemoryStream(Convert.FromBase64String(xmlDocument.GetElementsByTagName("tm:infoBin")[0].InnerText));
			xmlDocument.Load(inStream);
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("pkc", "http://www.microsoft.com/DRM/PKEY/Configuration/2.0");
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/pkc:ProductKeyConfiguration/pkc:Configurations/pkc:Configuration[pkc:ActConfigId='" + aid + "']", xmlNamespaceManager);
			bool flag2 = xmlNode == null;
			if (flag2)
			{
				xmlNode = xmlDocument.SelectSingleNode("/pkc:ProductKeyConfiguration/pkc:Configurations/pkc:Configuration[pkc:ActConfigId='" + aid.ToUpper() + "']", xmlNamespaceManager);
			}
			string result;
			try
			{
				result = xmlNode.ChildNodes.Item(3).InnerText;
			}
			catch
			{
				result = this.res.GetResourceString("txtProductKeyConfigurationInvalid");
			}
			return result;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005DA4 File Offset: 0x00003FA4
		public string GetResourceTextFile(string filename)
		{
			string result;
			try
			{
				Stream manifestResourceStream = base.GetType().Assembly.GetManifestResourceStream(Assembly.GetExecutingAssembly().GetName().Name + ".Resources." + filename);
				using (StreamReader streamReader = new StreamReader(manifestResourceStream))
				{
					result = streamReader.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			return result;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005E24 File Offset: 0x00004024
		public void GetResourceFile(string path)
		{
			try
			{
				string text = Environment.Is64BitOperatingSystem ? "x64" : "x86";
				string fileName = Path.GetFileName(path);
				Stream manifestResourceStream = base.GetType().Assembly.GetManifestResourceStream(string.Concat(new string[]
				{
					Assembly.GetExecutingAssembly().GetName().Name,
					".Resources.",
					text,
					".",
					fileName
				}));
				int count = Convert.ToInt32(manifestResourceStream.Length);
				using (BinaryReader binaryReader = new BinaryReader(manifestResourceStream))
				{
					File.WriteAllBytes(path, binaryReader.ReadBytes(count));
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x040007AE RID: 1966
		internal Resource res = new Resource();
	}
}
