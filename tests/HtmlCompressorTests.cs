namespace PvcPlugins.HtmlCompressor.UnitTests
{
	using System;
	using System.Collections.Generic;
	using System.Text.RegularExpressions;
    using Xunit;
    using ZetaHtmlCompressor;
    using PvcPlugins.HtmlCompressor.UnitTests.Properties;

	public sealed class HtmlCompressorTests
	{
		[Fact]
		public void Test01()
		{
			var comp = new HtmlCompressor();

			var input = Resources.Html01;
			var output = comp.compress(input);

			Assert.NotEmpty(output);
			var ratio = Math.Round((double)output.Length / input.Length * 100.0, 2);
			Assert.True(ratio < 100.0);
		}

		[Fact]
		public void testEnabled()
		{
			var source = Resources.testEnabled;
			var result = Resources.testEnabledResult;

			var compressor = new HtmlCompressor();
			compressor.setEnabled(false);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);
		}

		[Fact]
		public void testRemoveSpacesInsideTags()
		{
			var source = Resources.testRemoveSpacesInsideTags;
			var result = Resources.testRemoveSpacesInsideTagsResult;

			var compressor = new HtmlCompressor();
			compressor.setRemoveMultiSpaces(false);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}

		[Fact]
		public void testRemoveComments()
		{
			var source = Resources.testRemoveComments;
			var result = Resources.testRemoveCommentsResult;

			var compressor = new HtmlCompressor();
			compressor.setRemoveComments(true);
			compressor.setRemoveIntertagSpaces(true);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}

		[Fact]
		public void testRemoveQuotes()
		{
			var source = Resources.testRemoveQuotes;
			var result = Resources.testRemoveQuotesResult;

			var compressor = new HtmlCompressor();
			compressor.setRemoveQuotes(true);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}

		[Fact]
		public void testRemoveMultiSpaces()
		{
			var source = Resources.testRemoveMultiSpaces;
			var result = Resources.testRemoveMultiSpacesResult;

			var compressor = new HtmlCompressor();
			compressor.setRemoveMultiSpaces(true);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}

		[Fact]
		public void testRemoveIntertagSpaces()
		{
			var source = Resources.testRemoveIntertagSpaces;
			var result = Resources.testRemoveIntertagSpacesResult;

			var compressor = new HtmlCompressor();
			compressor.setRemoveIntertagSpaces(true);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}

		[Fact]
		public void testPreservePatterns()
		{
			var source = Resources.testPreservePatterns;
			var result = Resources.testPreservePatternsResult;

			var preservePatterns = new List<Regex>();
			preservePatterns.Add(HtmlCompressor.PHP_TAG_PATTERN); //<?php ... ?> blocks
			preservePatterns.Add(HtmlCompressor.SERVER_SCRIPT_TAG_PATTERN); //<% ... %> blocks
			preservePatterns.Add(HtmlCompressor.SERVER_SIDE_INCLUDE_PATTERN); //<!--# ... --> blocks
			preservePatterns.Add(new Regex("<jsp:.*?>", RegexOptions.Singleline | RegexOptions.IgnoreCase)); //<jsp: ... > tags

			var compressor = new HtmlCompressor();
			compressor.setPreservePatterns(preservePatterns);
			compressor.setRemoveComments(true);
			compressor.setRemoveIntertagSpaces(true);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}

		[Fact]
		public void testCompress()
		{
			var source = Resources.testCompress;
			var result = Resources.testCompressResult;

			var compressor = new HtmlCompressor();

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);
		}

		[Fact]
		public void testSimpleDoctype()
		{
			var source = Resources.testSimpleDoctype;
			var result = Resources.testSimpleDoctypeResult;

			var compressor = new HtmlCompressor();
			compressor.setSimpleDoctype(true);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}

		[Fact]
		public void testRemoveScriptAttributes()
		{
			var source = Resources.testRemoveScriptAttributes;
			var result = Resources.testRemoveScriptAttributesResult;

			var compressor = new HtmlCompressor();
			compressor.setRemoveScriptAttributes(true);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}

		[Fact]
		public void testRemoveStyleAttributes()
		{
			var source = Resources.testRemoveStyleAttributes;
			var result = Resources.testRemoveStyleAttributesResult;

			var compressor = new HtmlCompressor();
			compressor.setRemoveStyleAttributes(true);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}

		[Fact]
		public void testRemoveLinkAttributes()
		{
			var source = Resources.testRemoveLinkAttributes;
			var result = Resources.testRemoveLinkAttributesResult;

			var compressor = new HtmlCompressor();
			compressor.setRemoveLinkAttributes(true);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}

		[Fact]
		public void testRemoveFormAttributes()
		{
			var source = Resources.testRemoveFormAttributes;
			var result = Resources.testRemoveFormAttributesResult;

			var compressor = new HtmlCompressor();
			compressor.setRemoveFormAttributes(true);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}

		[Fact]
		public void testRemoveInputAttributes()
		{
			var source = Resources.testRemoveInputAttributes;
			var result = Resources.testRemoveInputAttributesResult;

			var compressor = new HtmlCompressor();
			compressor.setRemoveInputAttributes(true);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}

		[Fact]
		public void testRemoveJavaScriptProtocol()
		{
			var source = Resources.testRemoveJavaScriptProtocol;
			var result = Resources.testRemoveJavaScriptProtocolResult;

			var compressor = new HtmlCompressor();
			compressor.setRemoveJavaScriptProtocol(true);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}

		[Fact]
		public void testRemoveHttpProtocol()
		{
			var source = Resources.testRemoveHttpProtocol;
			var result = Resources.testRemoveHttpProtocolResult;

			var compressor = new HtmlCompressor();
			compressor.setRemoveHttpProtocol(true);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}

		[Fact]
		public void testRemoveHttpsProtocol()
		{
			var source = Resources.testRemoveHttpsProtocol;
			var result = Resources.testRemoveHttpsProtocolResult;

			var compressor = new HtmlCompressor();
			compressor.setRemoveHttpsProtocol(true);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}

		[Fact]
		public void testPreserveLineBreaks()
		{
			var source = Resources.testPreserveLineBreaks;
			var result = Resources.testPreserveLineBreaksResult;

			var compressor = new HtmlCompressor();
			compressor.setPreserveLineBreaks(true);

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}

		[Fact]
		public void testSurroundingSpaces()
		{
			var source = Resources.testSurroundingSpaces;
			var result = Resources.testSurroundingSpacesResult;

			var compressor = new HtmlCompressor();
			compressor.setRemoveIntertagSpaces(true);
			compressor.setRemoveSurroundingSpaces("p,br");

			var compress = compressor.compress(source);
			Assert.Equal(result, compress);

		}
	}
}