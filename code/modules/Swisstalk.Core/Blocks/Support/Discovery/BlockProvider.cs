namespace Swisstalk.Core.Blocks.Support.Discovery
{
	public static class BlockProvider
	{
		private static BlockStorage _locator;

		static BlockProvider()
		{
			_locator = new BlockStorage();
		}

		public static IBlockLocator Locator
		{
			get
			{
				return _locator;	
			}
		}

		public static IBlockRegistrar Registrar
		{
			get
			{
				return _locator;
			}
		}
	}
}
