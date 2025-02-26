using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();
builder.Services.AddChatClient(new OllamaChatClient(new Uri("http://localhost:11434"), "llama3"));

var app = builder.Build();
var chatClient = app.Services.GetRequiredService<IChatClient>();

// Function to extract text from a PDF

List<string> ExtractTextFromPdf(string pdfFilePath)
{
    List<string> result = new();
    using (PdfReader pdfReader = new(pdfFilePath))
    using (PdfDocument pdfDocument = new(pdfReader))
    {
        for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
        {
            string text = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i), new SimpleTextExtractionStrategy());
            result.Add(text);
        }
    }
    return result;
}

// Process PDFs in the "posts" directory
var posts = Directory.GetFiles("posts").Take(5).ToArray();
foreach (var post in posts)
{
    string prompt =
        $$"""
         You will receive an input text and the desired output format.
         You need to analyze the text and produce the desired output format.
         You are not allowed to change code, text, or other references.

         # Desired response

         Only provide an RFC8259 compliant JSON response following this format without deviation.

         {
            "title": "summarize the content of the text",
            "summary": "Summarize the article in no more than 10 words"
         }

         # Article content:

         {{string.Join(" ", ExtractTextFromPdf(post))}}
         """; // Combining the extracted text into a single string

    // Request AI model to process the PDF content
    var chatCompletion = await chatClient.CompleteAsync(prompt);

    // Output the AI's response
    Console.WriteLine(chatCompletion.Message.Text);
    Console.WriteLine(Environment.NewLine);
}
