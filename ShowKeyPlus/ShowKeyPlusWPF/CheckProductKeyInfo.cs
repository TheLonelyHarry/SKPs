using System;
using Prism.Mvvm;
using ShowKeyPlus;

namespace ShowKeyPlusWPF
{
	// Token: 0x0200001E RID: 30
	public class CheckProductKeyInfo : BindableBase
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00004FDE File Offset: 0x000031DE
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00004FE8 File Offset: 0x000031E8
		public string ProductKey
		{
			get
			{
				return this.productKey;
			}
			set
			{
				bool flag = this.productKey != value;
				if (flag)
				{
					this.productKey = value;
					this.OnPropertyChanged(<>PropertyChangedEventArgs.ProductKey);
				}
				base.RaisePropertyChanged(this.ProductKey);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00005025 File Offset: 0x00003225
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00005030 File Offset: 0x00003230
		public string EULA
		{
			get
			{
				return this.eEULA;
			}
			set
			{
				bool flag = this.eEULA != value;
				if (flag)
				{
					this.eEULA = value;
					this.OnPropertyChanged(<>PropertyChangedEventArgs.EULA);
				}
				base.RaisePropertyChanged(this.EULA);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000506D File Offset: 0x0000326D
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00005078 File Offset: 0x00003278
		public string PKDescription
		{
			get
			{
				return this.pKDescription;
			}
			set
			{
				bool flag = this.pKDescription != value;
				if (flag)
				{
					this.pKDescription = value;
					this.OnPropertyChanged(<>PropertyChangedEventArgs.PKDescription);
				}
				base.RaisePropertyChanged(this.PKDescription);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000050B5 File Offset: 0x000032B5
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000050C0 File Offset: 0x000032C0
		public string MakCount
		{
			get
			{
				return this.makcount;
			}
			set
			{
				bool flag = this.makcount != value;
				if (flag)
				{
					this.makcount = value;
					this.OnPropertyChanged(<>PropertyChangedEventArgs.MakCount);
				}
				base.RaisePropertyChanged(this.makcount);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000050FD File Offset: 0x000032FD
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00005108 File Offset: 0x00003308
		public bool CurrentProgress
		{
			get
			{
				return this.currentProgress;
			}
			set
			{
				bool flag = this.currentProgress != value;
				if (flag)
				{
					this.currentProgress = value;
				}
				base.RaisePropertyChanged("CurrentProgress");
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00005139 File Offset: 0x00003339
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00005144 File Offset: 0x00003344
		public bool MAKvisibility
		{
			get
			{
				return this.makvisibility;
			}
			set
			{
				bool flag = this.makvisibility != value;
				if (flag)
				{
					this.makvisibility = value;
					this.OnPropertyChanged(<>PropertyChangedEventArgs.MAKvisibility);
				}
				base.RaisePropertyChanged("makvisibility");
			}
		}

		// Token: 0x040007A1 RID: 1953
		private bool currentProgress;

		// Token: 0x040007A2 RID: 1954
		private bool makvisibility;

		// Token: 0x040007A3 RID: 1955
		private string productKey;

		// Token: 0x040007A4 RID: 1956
		private string eEULA;

		// Token: 0x040007A5 RID: 1957
		private string pKDescription;

		// Token: 0x040007A6 RID: 1958
		private string makcount;
	}
}
