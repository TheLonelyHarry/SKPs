using System;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace ShowKeyPlusWPF.ViewModel
{
	// Token: 0x02000046 RID: 70
	public class ViewModelLocator
	{
		// Token: 0x0600022A RID: 554 RVA: 0x0000DB38 File Offset: 0x0000BD38
		public ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
			SimpleIoc.Default.Register<MainViewModel>();
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000DB74 File Offset: 0x0000BD74
		public MainViewModel Main
		{
			get
			{
				return ServiceLocator.Current.GetInstance<MainViewModel>();
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000B003 File Offset: 0x00009203
		public static void Cleanup()
		{
		}
	}
}
