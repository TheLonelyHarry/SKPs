using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using Prism.Mvvm;
using ShowKeyPlus;
using SourceChord.FluentWPF;

namespace ShowKeyPlusWPF
{
	// Token: 0x0200001F RID: 31
	public class AccentBrush : BindableBase
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00005180 File Offset: 0x00003380
		public Brush Background
		{
			get
			{
				return new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.Accent));
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000051A9 File Offset: 0x000033A9
		// (set) Token: 0x0600013A RID: 314 RVA: 0x000051B4 File Offset: 0x000033B4
		public string Accent
		{
			get
			{
				return this._accent;
			}
			set
			{
				if (string.Equals(this._accent, value, StringComparison.Ordinal))
				{
					return;
				}
				this.SetProperty<string>(ref this._accent, value, "Accent");
				this.OnPropertyChanged(<>PropertyChangedEventArgs.Background);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600013B RID: 315 RVA: 0x000051F4 File Offset: 0x000033F4
		public double SetTransparency
		{
			get
			{
				Uri source = Application.Current.Resources.MergedDictionaries[0].Source;
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize"))
				{
					object obj = (registryKey != null) ? registryKey.GetValue("EnableTransparency") : null;
					object value = (registryKey != null) ? registryKey.GetValue("AppsUseLightTheme") : null;
					bool flag = obj != null && Convert.ToInt32(obj) == 1;
					if (flag)
					{
						return (Convert.ToInt32(value) == 1) ? 0.9 : 0.95;
					}
				}
				return 1.0;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000052B8 File Offset: 0x000034B8
		private AccentBrush.Theme GetTheme
		{
			get
			{
				Uri source = Application.Current.Resources.MergedDictionaries[0].Source;
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize"))
				{
					object obj = (registryKey != null) ? registryKey.GetValue("AppsUseLightTheme") : null;
					bool flag = obj != null && Convert.ToInt32(obj) == 1;
					if (flag)
					{
						return AccentBrush.Theme.Light;
					}
				}
				return AccentBrush.Theme.Dark;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00005344 File Offset: 0x00003544
		public Brush AccentStandard
		{
			get
			{
				return (this.GetTheme == AccentBrush.Theme.Light) ? AccentColors.ImmersiveSystemAccentDark1Brush : AccentColors.ImmersiveSystemAccentLight1Brush;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000535B File Offset: 0x0000355B
		public Brush AccentBright
		{
			get
			{
				return (this.GetTheme == AccentBrush.Theme.Light) ? AccentColors.ImmersiveSystemAccentDark1Brush : AccentColors.ImmersiveSystemAccentLight2Brush;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00005372 File Offset: 0x00003572
		public Brush AccentMedium
		{
			get
			{
				return (this.GetTheme == AccentBrush.Theme.Light) ? AccentColors.ImmersiveSystemAccentDark2Brush : AccentColors.ImmersiveSystemAccentLight3Brush;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000538C File Offset: 0x0000358C
		public Brush AccentGradient
		{
			get
			{
				Color color = (Color)ColorConverter.ConvertFromString(Colors.DimGray.ToString());
				return new SolidColorBrush(color)
				{
					Opacity = ((this.GetTheme == AccentBrush.Theme.Light) ? 0.1 : 0.2)
				};
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000053E8 File Offset: 0x000035E8
		public Brush ButtonTextColour
		{
			get
			{
				return (this.GetTheme == AccentBrush.Theme.Light) ? Brushes.White : Brushes.Black;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000053FF File Offset: 0x000035FF
		public Brush ButtonTextClickedColour
		{
			get
			{
				return (this.GetTheme == AccentBrush.Theme.Light) ? Brushes.LightGray : Brushes.DarkSlateGray;
			}
		}

		// Token: 0x040007A7 RID: 1959
		private string _accent;

		// Token: 0x02000020 RID: 32
		private enum Theme
		{
			// Token: 0x040007A9 RID: 1961
			Dark,
			// Token: 0x040007AA RID: 1962
			Light
		}
	}
}
