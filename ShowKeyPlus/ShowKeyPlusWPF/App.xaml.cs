using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;
using Microsoft.Win32;

namespace ShowKeyPlusWPF
{
	// Token: 0x02000029 RID: 41
	public partial class App : Application
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00007133 File Offset: 0x00005333
		public App()
		{
			SystemEvents.UserPreferenceChanged += this.SystemEvents_UserPreferenceChanged;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007150 File Offset: 0x00005350
		public void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			UserPreferenceCategory category = e.Category;
			UserPreferenceCategory userPreferenceCategory = category;
			if (userPreferenceCategory == UserPreferenceCategory.General)
			{
				this.SetTheme();
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007178 File Offset: 0x00005378
		private bool ThemeIsLight()
		{
			bool result;
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize");
				result = ((int)registryKey.GetValue("AppsUseLightTheme") == 1);
			}
			catch
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000071C4 File Offset: 0x000053C4
		public void SetTheme()
		{
			bool flag = this.ThemeIsLight();
			base.Resources.MergedDictionaries[1].Source = (flag ? new Uri("ThemeLight.xaml", UriKind.Relative) : new Uri("ThemeDark.xaml", UriKind.Relative));
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000720B File Offset: 0x0000540B
		protected override void OnStartup(StartupEventArgs e)
		{
			this.SetTheme();
			base.OnStartup(e);
		}
	}
}
