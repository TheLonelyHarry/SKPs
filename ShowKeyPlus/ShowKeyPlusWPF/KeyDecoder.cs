using System;
using System.Collections;
using System.Windows;
using Microsoft.Win32;

namespace ShowKeyPlusWPF
{
	// Token: 0x0200001A RID: 26
	public class KeyDecoder
	{
		// Token: 0x06000103 RID: 259 RVA: 0x000043E4 File Offset: 0x000025E4
		public byte[] GetRegistryDigitalProductId(KeyDecoder.Key key, bool hive, string dpId, string regsourcekey)
		{
			byte[] array = null;
			byte[] array2 = null;
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\Setup");
			string text = string.Empty;
			RegistryView view = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Default;
			try
			{
				bool flag = !hive;
				if (flag)
				{
					if (key != KeyDecoder.Key.Windows)
					{
						if (key == KeyDecoder.Key.IE)
						{
							text = "SOFTWARE\\Microsoft\\Internet Explorer\\Registration";
							using (RegistryKey registryKey2 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, view))
							{
								using (RegistryKey registryKey3 = registryKey2.OpenSubKey(text))
								{
									bool flag2 = registryKey3 == null || registryKey3.GetValue("DigitalProductId") == null;
									if (flag2)
									{
										text = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
									}
								}
							}
							bool flag3 = registryKey != null;
							if (flag3)
							{
								foreach (string text2 in registryKey.GetSubKeyNames())
								{
									bool flag4 = text2.ToString().Contains("Source OS");
									if (flag4)
									{
										text = "SYSTEM\\Setup\\" + text2.ToString();
										break;
									}
								}
								bool flag5 = text.Contains("Source OS");
								if (flag5)
								{
									string text3 = string.Empty;
									string b = string.Empty;
									using (RegistryKey registryKey4 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, view))
									{
										using (RegistryKey registryKey5 = registryKey4.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Registration"))
										{
											bool flag6 = registryKey5.GetValue("ProductId") != null;
											if (flag6)
											{
												text3 = (registryKey5.GetValue("ProductId") as string);
											}
										}
										using (RegistryKey registryKey6 = registryKey4.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion"))
										{
											b = (registryKey6.GetValue("ProductId") as string);
										}
									}
									bool flag7 = !string.IsNullOrEmpty(text3) && text3 != b;
									if (flag7)
									{
										text = "SOFTWARE\\Microsoft\\Internet Explorer\\Registration";
									}
								}
							}
						}
					}
					else
					{
						text = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
					}
				}
				else
				{
					text = regsourcekey;
				}
				using (RegistryKey registryKey7 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, view))
				{
					using (RegistryKey registryKey8 = registryKey7.OpenSubKey(text))
					{
						bool flag8 = registryKey8 != null;
						if (flag8)
						{
							bool flag9 = key == KeyDecoder.Key.Windows;
							if (flag9)
							{
								array = (registryKey8.GetValue("DigitalProductId") as byte[]);
								this.prod = (registryKey8.GetValue("ProductName") as string);
								this.prodID = (registryKey8.GetValue("ProductID") as string);
								this.build = (registryKey8.GetValue("BuildLabEx") as string);
								this.currentBuild = ((registryKey8.GetValue("CurrentBuild") != null) ? (registryKey8.GetValue("CurrentBuild") as string) : string.Empty);
								this.prod = this.CheckWin11(this.prod, this.currentBuild);
								bool flag10 = registryKey8.GetValue("UBR") != null;
								if (flag10)
								{
									this.UBR = registryKey8.GetValue("UBR").ToString();
								}
								else
								{
									bool flag11 = registryKey8.GetValue("CSDVersion") != null;
									if (flag11)
									{
										this.UBR = registryKey8.GetValue("CSDVersion").ToString();
									}
									else
									{
										this.UBR = string.Empty;
									}
								}
							}
							else
							{
								bool flag12 = key == KeyDecoder.Key.IE;
								if (flag12)
								{
									array = (registryKey8.GetValue("DigitalProductId") as byte[]);
									array2 = (registryKey8.GetValue("DigitalProductId4") as byte[]);
								}
							}
							registryKey8.Close();
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error: GetRegistryDigitalProductId", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			bool flag13 = dpId.Contains("dpId4");
			byte[] result;
			if (flag13)
			{
				result = array2;
			}
			else
			{
				result = array;
			}
			return result;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004898 File Offset: 0x00002A98
		public string CheckWin11(string prodname, string buildversion)
		{
			string[] array = buildversion.Split(new char[]
			{
				'.'
			});
			return (Convert.ToInt32(array[0]) >= 22000) ? prodname.Replace("10", "11") : prodname;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000048E0 File Offset: 0x00002AE0
		public string DecodeProductKey(byte[] digitalProductId)
		{
			char[] array = new char[29];
			char[] array2 = new char[]
			{
				'B',
				'C',
				'D',
				'F',
				'G',
				'H',
				'J',
				'K',
				'M',
				'P',
				'Q',
				'R',
				'T',
				'V',
				'W',
				'X',
				'Y',
				'2',
				'3',
				'4',
				'6',
				'7',
				'8',
				'9'
			};
			string result;
			try
			{
				int num = digitalProductId[66] >> 3 & 1;
				digitalProductId[66] = (byte)((int)(digitalProductId[66] & 247) | (num & 2) << 2);
				ArrayList arrayList = new ArrayList();
				for (int i = 52; i <= 67; i++)
				{
					arrayList.Add(digitalProductId[i]);
				}
				for (int j = 28; j >= 0; j--)
				{
					bool flag = (j + 1) % 6 == 0;
					if (flag)
					{
						array[j] = '-';
					}
					else
					{
						int num2 = 0;
						for (int k = 14; k >= 0; k--)
						{
							int num3 = num2 << 8 | (int)((byte)arrayList[k]);
							arrayList[k] = (byte)(num3 / 24);
							num2 = num3 % 24;
							array[j] = array2[num2];
						}
					}
				}
				bool flag2 = num != 0;
				if (flag2)
				{
					int num4 = 0;
					for (int l = 0; l < array.Length; l++)
					{
						bool flag3 = array[0] != array2[l];
						if (!flag3)
						{
							num4 = l;
							break;
						}
					}
					string text = new string(array);
					text = text.Replace("-", string.Empty).Remove(0, 1);
					text = text.Substring(0, num4) + "N" + text.Remove(0, num4);
					text = string.Concat(new string[]
					{
						text.Substring(0, 5),
						"-",
						text.Substring(5, 5),
						"-",
						text.Substring(10, 5),
						"-",
						text.Substring(15, 5),
						"-",
						text.Substring(20, 5)
					});
					array = text.ToCharArray();
				}
				result = new string(array);
			}
			catch (Exception ex)
			{
				string[] array3 = new string[6];
				array3[0] = "Error: DecodeProductKey";
				array3[1] = Environment.NewLine;
				array3[2] = ex.Message;
				array3[3] = Environment.NewLine;
				array3[4] = "Decodedchars:";
				int num5 = 5;
				char[] array4 = array;
				array3[num5] = ((array4 != null) ? array4.ToString() : null);
				MessageBox.Show(string.Concat(array3), "Windows Key information:" + ex.Source, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				result = "Unable to decode product key";
			}
			return result;
		}

		// Token: 0x04000787 RID: 1927
		public string prod = string.Empty;

		// Token: 0x04000788 RID: 1928
		public string prodID = string.Empty;

		// Token: 0x04000789 RID: 1929
		public string UBR = string.Empty;

		// Token: 0x0400078A RID: 1930
		public string IEName = string.Empty;

		// Token: 0x0400078B RID: 1931
		public string build = string.Empty;

		// Token: 0x0400078C RID: 1932
		public string currentBuild = string.Empty;

		// Token: 0x0200001B RID: 27
		public enum Key
		{
			// Token: 0x0400078E RID: 1934
			Windows,
			// Token: 0x0400078F RID: 1935
			IE
		}
	}
}
