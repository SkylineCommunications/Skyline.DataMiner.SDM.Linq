namespace SDM.LinqTests.Shared
{
	using System;

	using Skyline.DataMiner.SDM;

	public class SubClass
	{
		public string Description { get; set; }

		public double Size { get; set; }

		public SdmObjectReference<TestClass> Reference { get; set; }

		public Guid Guid { get; set; }
	}
}
