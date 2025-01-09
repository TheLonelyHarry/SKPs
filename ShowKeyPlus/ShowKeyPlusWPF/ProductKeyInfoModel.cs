using System;
using System.Windows;
using Prism.Mvvm;
using ShowKeyPlus;

namespace ShowKeyPlusWPF
{
	// Token: 0x0200001C RID: 28
	public class ProductKeyInfoModel
	{
		// Token: 0x0200001D RID: 29
		public class ProductKeyInfo : BindableBase
		{
			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000108 RID: 264 RVA: 0x00004BD4 File Offset: 0x00002DD4
			// (set) Token: 0x06000109 RID: 265 RVA: 0x00004BEC File Offset: 0x00002DEC
			public Visibility MessageVisibilty
			{
				get
				{
					return this._MessageVisibilty;
				}
				set
				{
					if (this._MessageVisibilty == value)
					{
						return;
					}
					this.SetProperty<Visibility>(ref this._MessageVisibilty, value, "MessageVisibilty");
				}
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x0600010A RID: 266 RVA: 0x00004C1A File Offset: 0x00002E1A
			// (set) Token: 0x0600010B RID: 267 RVA: 0x00004C24 File Offset: 0x00002E24
			public string ProductName
			{
				get
				{
					return this.productName;
				}
				set
				{
					if (string.Equals(this.productName, value, StringComparison.Ordinal))
					{
						return;
					}
					this.SetProperty<string>(ref this.productName, value, "ProductName");
				}
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x0600010C RID: 268 RVA: 0x00004C56 File Offset: 0x00002E56
			// (set) Token: 0x0600010D RID: 269 RVA: 0x00004C60 File Offset: 0x00002E60
			public string Version
			{
				get
				{
					return this.version;
				}
				set
				{
					if (string.Equals(this.version, value, StringComparison.Ordinal))
					{
						return;
					}
					this.SetProperty<string>(ref this.version, value, "Version");
				}
			}

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x0600010E RID: 270 RVA: 0x00004C92 File Offset: 0x00002E92
			// (set) Token: 0x0600010F RID: 271 RVA: 0x00004C9C File Offset: 0x00002E9C
			public string ProductID
			{
				get
				{
					return this.productID;
				}
				set
				{
					if (string.Equals(this.productID, value, StringComparison.Ordinal))
					{
						return;
					}
					this.SetProperty<string>(ref this.productID, value, "ProductID");
				}
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x06000110 RID: 272 RVA: 0x00004CCE File Offset: 0x00002ECE
			// (set) Token: 0x06000111 RID: 273 RVA: 0x00004CD8 File Offset: 0x00002ED8
			public string InstalledKey
			{
				get
				{
					return this.installedKey;
				}
				set
				{
					if (string.Equals(this.installedKey, value, StringComparison.Ordinal))
					{
						return;
					}
					this.SetProperty<string>(ref this.installedKey, value, "InstalledKey");
				}
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x06000112 RID: 274 RVA: 0x00004D0A File Offset: 0x00002F0A
			// (set) Token: 0x06000113 RID: 275 RVA: 0x00004D14 File Offset: 0x00002F14
			public string OriginalKey
			{
				get
				{
					return this.originalKey;
				}
				set
				{
					bool flag = this.originalKey != value;
					if (flag)
					{
						this.originalKey = value;
						this.OnPropertyChanged(<>PropertyChangedEventArgs.OriginalKey);
					}
				}
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x06000114 RID: 276 RVA: 0x00004D45 File Offset: 0x00002F45
			// (set) Token: 0x06000115 RID: 277 RVA: 0x00004D50 File Offset: 0x00002F50
			public string OriginalEdition
			{
				get
				{
					return this.originalEdition;
				}
				set
				{
					if (string.Equals(this.originalEdition, value, StringComparison.Ordinal))
					{
						return;
					}
					this.SetProperty<string>(ref this.originalEdition, value, "OriginalEdition");
				}
			}

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x06000116 RID: 278 RVA: 0x00004D82 File Offset: 0x00002F82
			// (set) Token: 0x06000117 RID: 279 RVA: 0x00004D8C File Offset: 0x00002F8C
			public string OEMKey
			{
				get
				{
					return this.oEMKey;
				}
				set
				{
					if (string.Equals(this.oEMKey, value, StringComparison.Ordinal))
					{
						return;
					}
					this.SetProperty<string>(ref this.oEMKey, value, "OEMKey");
				}
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x06000118 RID: 280 RVA: 0x00004DBE File Offset: 0x00002FBE
			// (set) Token: 0x06000119 RID: 281 RVA: 0x00004DC8 File Offset: 0x00002FC8
			public string OEMDescription
			{
				get
				{
					return this.oEMDescription;
				}
				set
				{
					if (string.Equals(this.oEMDescription, value, StringComparison.Ordinal))
					{
						return;
					}
					this.SetProperty<string>(ref this.oEMDescription, value, "OEMDescription");
				}
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x0600011A RID: 282 RVA: 0x00004DFA File Offset: 0x00002FFA
			// (set) Token: 0x0600011B RID: 283 RVA: 0x00004E04 File Offset: 0x00003004
			public bool Generic
			{
				get
				{
					return this.generic;
				}
				set
				{
					bool flag = this.generic != value;
					if (flag)
					{
						this.generic = value;
					}
					base.RaisePropertyChanged("Generic");
				}
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x0600011C RID: 284 RVA: 0x00004E35 File Offset: 0x00003035
			// (set) Token: 0x0600011D RID: 285 RVA: 0x00004E40 File Offset: 0x00003040
			public bool Generic2
			{
				get
				{
					return this.generic2;
				}
				set
				{
					bool flag = this.generic2 != value;
					if (flag)
					{
						this.generic2 = value;
					}
					base.RaisePropertyChanged("Generic2");
				}
			}

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x0600011E RID: 286 RVA: 0x00004E71 File Offset: 0x00003071
			// (set) Token: 0x0600011F RID: 287 RVA: 0x00004E7C File Offset: 0x0000307C
			public bool CurrentProgress
			{
				get
				{
					return this.currentProgress;
				}
				set
				{
					if (this.currentProgress == value)
					{
						return;
					}
					this.SetProperty<bool>(ref this.currentProgress, value, "CurrentProgress");
				}
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x06000120 RID: 288 RVA: 0x00004EAA File Offset: 0x000030AA
			// (set) Token: 0x06000121 RID: 289 RVA: 0x00004EB4 File Offset: 0x000030B4
			public string ProgressLabel
			{
				get
				{
					return this.progressLabel;
				}
				set
				{
					if (string.Equals(this.progressLabel, value, StringComparison.Ordinal))
					{
						return;
					}
					this.SetProperty<string>(ref this.progressLabel, value, "ProgressLabel");
				}
			}

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x06000122 RID: 290 RVA: 0x00004EE6 File Offset: 0x000030E6
			// (set) Token: 0x06000123 RID: 291 RVA: 0x00004EF0 File Offset: 0x000030F0
			public GridLength HideOrig
			{
				get
				{
					return this.hideOriginal;
				}
				set
				{
					if (this.hideOriginal == value)
					{
						return;
					}
					this.SetProperty<GridLength>(ref this.hideOriginal, value, "HideOrig");
				}
			}

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x06000124 RID: 292 RVA: 0x00004F21 File Offset: 0x00003121
			// (set) Token: 0x06000125 RID: 293 RVA: 0x00004F2C File Offset: 0x0000312C
			public GridLength HideOEM
			{
				get
				{
					return this.hideOEM;
				}
				set
				{
					if (this.hideOEM == value)
					{
						return;
					}
					this.SetProperty<GridLength>(ref this.hideOEM, value, "HideOEM");
				}
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x06000126 RID: 294 RVA: 0x00004F5D File Offset: 0x0000315D
			// (set) Token: 0x06000127 RID: 295 RVA: 0x00004F68 File Offset: 0x00003168
			public GridLength HideGeneric
			{
				get
				{
					return this.hideGeneric;
				}
				set
				{
					if (this.hideGeneric == value)
					{
						return;
					}
					this.SetProperty<GridLength>(ref this.hideGeneric, value, "HideGeneric");
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x06000128 RID: 296 RVA: 0x00004F99 File Offset: 0x00003199
			// (set) Token: 0x06000129 RID: 297 RVA: 0x00004FA4 File Offset: 0x000031A4
			public GridLength HideUpgrades
			{
				get
				{
					return this.hideUpgrades;
				}
				set
				{
					if (this.hideUpgrades == value)
					{
						return;
					}
					this.SetProperty<GridLength>(ref this.hideUpgrades, value, "HideUpgrades");
				}
			}

			// Token: 0x04000790 RID: 1936
			private Visibility _MessageVisibilty;

			// Token: 0x04000791 RID: 1937
			private string productName;

			// Token: 0x04000792 RID: 1938
			private string version;

			// Token: 0x04000793 RID: 1939
			private string productID;

			// Token: 0x04000794 RID: 1940
			private string installedKey;

			// Token: 0x04000795 RID: 1941
			private string originalKey;

			// Token: 0x04000796 RID: 1942
			private string originalEdition;

			// Token: 0x04000797 RID: 1943
			private string oEMKey;

			// Token: 0x04000798 RID: 1944
			private string oEMDescription;

			// Token: 0x04000799 RID: 1945
			private string progressLabel;

			// Token: 0x0400079A RID: 1946
			private bool generic;

			// Token: 0x0400079B RID: 1947
			private bool generic2;

			// Token: 0x0400079C RID: 1948
			private GridLength hideOriginal;

			// Token: 0x0400079D RID: 1949
			private GridLength hideOEM;

			// Token: 0x0400079E RID: 1950
			private GridLength hideGeneric;

			// Token: 0x0400079F RID: 1951
			private GridLength hideUpgrades;

			// Token: 0x040007A0 RID: 1952
			private bool currentProgress;
		}
	}
}
