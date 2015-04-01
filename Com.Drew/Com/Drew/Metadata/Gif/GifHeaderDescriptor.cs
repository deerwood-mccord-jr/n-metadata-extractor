using Com.Drew.Metadata;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Metadata.Gif
{
	/// <author>Drew Noakes https://drewnoakes.com</author>
	public class GifHeaderDescriptor : TagDescriptor<GifHeaderDirectory>
	{
		public GifHeaderDescriptor([NotNull] GifHeaderDirectory directory)
			: base(directory)
		{
		}
		//    @Override
		//    public String getDescription(int tagType)
		//    {
		//        switch (tagType) {
		//            case GifHeaderDirectory.TAG_COMPRESSION:
		//                return getCompressionDescription();
		//            default:
		//                return super.getDescription(tagType);
		//        }
		//    }
	}
}
