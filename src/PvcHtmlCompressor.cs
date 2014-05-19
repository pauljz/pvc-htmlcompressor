using PvcCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ZetaHtmlCompressor;

namespace PvcPlugins
{
    public class PvcHtmlCompressor : PvcPlugin
    {
        private readonly HtmlCompressor compressor;
        private readonly bool concurrent;

        /// <summary>
        /// Removes unnecessary whitespace and markup from HTML documents.
        /// </summary>
        /// <param name="removeComments">Removes comments</param>
        /// <param name="removeMultiSpaces">Removes multiple spaces</param>
        /// <param name="removeLineBreaks">Removes linebreaks</param>
        /// <param name="removeIntertagSpaces">Removes spaces between tags</param>
        /// <param name="removeQuotes">Remove unneeded quotes</param>
        /// <param name="simpleDoctype">Change doctype to &lt;!DOCTYPE html&gt;</param>
        /// <param name="removeStyleAttributes">Remove TYPE attribute from STYLE tags</param>
        /// <param name="removeLinkAttributes">Remove TYPE attribute from LINK tags</param>
        /// <param name="removeScriptAttributes">Remove TYPE and LANGUAGE from SCRIPT tags</param>
        /// <param name="removeFormAttributes">Remove METHOD="GET" from FORM tags</param>
        /// <param name="removeInputAttribtues">Remove TYPE="TEXT" from INPUT tags</param>
        /// <param name="simpleBooleanAttributes">Remove values from boolean tag attributes</param>
        /// <param name="removeJavaScriptProtocol">Remove "javascript:" from inline event handlers</param>
        /// <param name="removeHttpProtocol">Remove "http:" from tag attributes</param>
        /// <param name="removeHttpsProtocol">Remove "https:" from tag attributes</param>
        /// <param name="removeSurroundingSpacesTags">Predefined or custom comma separated list of tags [min|max|all|custom_list]</param>
        public PvcHtmlCompressor(
            bool concurrent = true,
            bool removeComments = true,
            bool removeMultiSpaces = true,
            bool removeLineBreaks = true,
            bool removeIntertagSpaces = false,
            bool removeQuotes = false,
            bool simpleDoctype = false,
            bool removeStyleAttributes = false,
            bool removeLinkAttributes = false,
            bool removeScriptAttributes = false,
            bool removeFormAttributes = false,
            bool removeInputAttributes = false,
            bool simpleBooleanAttributes = false,
            bool removeJavaScriptProtocol = false,
            bool removeHttpProtocol = false,
            bool removeHttpsProtocol = false,
            string removeSurroundingSpacesTags = ""
            )
        {
            this.compressor = new HtmlCompressor();
            this.concurrent = concurrent;
            compressor.setRemoveComments(removeComments);
            compressor.setRemoveMultiSpaces(removeMultiSpaces);
            compressor.setPreserveLineBreaks(!removeLineBreaks);
            compressor.setRemoveIntertagSpaces(removeIntertagSpaces);
            compressor.setRemoveQuotes(removeQuotes);
            compressor.setSimpleDoctype(simpleDoctype);
            compressor.setRemoveStyleAttributes(removeStyleAttributes);
            compressor.setRemoveLinkAttributes(removeLinkAttributes);
            compressor.setRemoveScriptAttributes(removeScriptAttributes);
            compressor.setRemoveFormAttributes(removeFormAttributes);
            compressor.setRemoveInputAttributes(removeInputAttributes);
            compressor.setSimpleBooleanAttributes(simpleBooleanAttributes);
            compressor.setRemoveJavaScriptProtocol(removeJavaScriptProtocol);
            compressor.setRemoveHttpProtocol(removeHttpProtocol);
            compressor.setRemoveHttpsProtocol(removeHttpsProtocol);
            compressor.setRemoveSurroundingSpaces(removeSurroundingSpacesTags);
        }

        private PvcStream CompressStream(PvcStream inputStream)
        {
            string html = new StreamReader(inputStream).ReadToEnd();
            var compressed = compressor.compress(html);
            Console.WriteLine(string.Format("Minified {0} from {1} to {2} bytes", inputStream.StreamName, html.Length, compressed.Length));
            return PvcUtil.StringToStream(compressed, inputStream.StreamName);
        }

        public override IEnumerable<PvcStream> Execute(IEnumerable<PvcStream> inputStreams)
        {
            if (this.concurrent)
            {
                var outputStreamBag = new ConcurrentBag<PvcStream>();
                Parallel.ForEach<PvcStream>(inputStreams, (inputStream) =>
                {
                    outputStreamBag.Add(this.CompressStream(inputStream));
                });
                return new List<PvcStream>(outputStreamBag);
            }
            else
            {
                var outputStreams = new List<PvcStream>();
                foreach (var inputStream in inputStreams)
                {
                    outputStreams.Add(this.CompressStream(inputStream));
                }
                return outputStreams;
            }
        }
    }
}
