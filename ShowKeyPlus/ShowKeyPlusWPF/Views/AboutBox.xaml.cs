using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using ModernWpf.Controls;

namespace ShowKeyPlusWPF.Views
{
	// Token: 0x02000033 RID: 51
	public partial class AboutBox : UserControl
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x0000A65C File Offset: 0x0000885C
		public AboutBox()
		{
			this.InitializeComponent();
			Resource resource = new Resource();
			this.LblTitle.Text = resource.GetResourceString("txtAbout.Text") + " " + this.AssemblyProduct;
			this.LblVersion.Text = string.Concat(new string[]
			{
				resource.GetResourceString("txtVersion.Text") + ":",
				" ",
				this.AssemblyVersion,
				" (",
				this.GetExecutingArch(),
				")"
			});
			this.LblCopyright.Text = this.AssemblyCopyright + " " + resource.GetResourceString("txtCopyright.Text");
			this.txtEULA.Text = resource.GetResourceString("txtTermsOfUse.Text");
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000A73C File Offset: 0x0000893C
		public string AssemblyTitle
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				bool flag = customAttributes.Length != 0;
				if (flag)
				{
					AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute)customAttributes[0];
					bool flag2 = assemblyTitleAttribute.Title != "";
					if (flag2)
					{
						return assemblyTitleAttribute.Title;
					}
				}
				return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000A7A8 File Offset: 0x000089A8
		public string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000A7D0 File Offset: 0x000089D0
		public string AssemblyDescription
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				bool flag = customAttributes.Length == 0;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					result = ((AssemblyDescriptionAttribute)customAttributes[0]).Description;
				}
				return result;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000A818 File Offset: 0x00008A18
		public string AssemblyProduct
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				bool flag = customAttributes.Length == 0;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					result = ((AssemblyProductAttribute)customAttributes[0]).Product;
				}
				return result;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000A860 File Offset: 0x00008A60
		public string AssemblyCopyright
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				bool flag = customAttributes.Length == 0;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					result = ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
				}
				return result;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000A8A8 File Offset: 0x00008AA8
		public string AssemblyCompany
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				bool flag = customAttributes.Length == 0;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					result = ((AssemblyCompanyAttribute)customAttributes[0]).Company;
				}
				return result;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		private string GetExecutingArch()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			AssemblyName assemblyName = AssemblyName.GetAssemblyName(executingAssembly.Location);
			string text = assemblyName.ProcessorArchitecture.ToString();
			string text2 = text.ToUpper();
			string a = text2;
			string result;
			if (!(a == "AMD64"))
			{
				result = "32-bit";
			}
			else
			{
				result = "64-bit";
			}
			return result;
		}
	}
}
