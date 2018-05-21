using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Language.V1;
using static Google.Cloud.Language.V1.AnnotateTextRequest.Types;
using Google.Protobuf.Collections;
using Google.Apis.Services;
using Google.Apis.Drive.v3;

namespace GraduateWorkTest
{
    public class GoogleNLP_Test
    {

        public static string Usage = @"everything Remy stands in the cemetery.";
/*        
public static string Usage = @"Usage:
C:\> dotnet run command text
C:\> dotnet run command gs://bucketName/objectName
Where command is one of
    entities
    sentiment
    syntax
    entity-sentiment
    classify-text
    everything
";
*/

        // [START analyze_entities_from_file]
        private static void AnalyzeEntitiesFromFile(string gcsUri)
        {
            var client = LanguageServiceClient.Create();
            var response = client.AnalyzeEntities(new Document()
            {
                GcsContentUri = gcsUri,
                Type = Document.Types.Type.PlainText
            });
            WriteEntities(response.Entities);
        }
        // [END analyze_entities_from_file]

        // [START analyze_entities_from_string]
        private static void AnalyzeEntitiesFromText(string text)
        {
            var client = LanguageServiceClient.Create();
            var response = client.AnalyzeEntities(new Document()
            {
                Content = text,
                Type = Document.Types.Type.PlainText
            });
            WriteEntities(response.Entities);
        }

        // [START analyze_entities_from_file]
        private static void WriteEntities(IEnumerable<Entity> entities)
        {
            Console.WriteLine("Entities:");
            foreach (var entity in entities)
            {
                Console.WriteLine($"\tName: {entity.Name}");
                Console.WriteLine($"\tType: {entity.Type}");
                Console.WriteLine($"\tSalience: {entity.Salience}");
                Console.WriteLine("\tMentions:");
                foreach (var mention in entity.Mentions)
                    Console.WriteLine($"\t\t{mention.Text.BeginOffset}: {mention.Text.Content}");
                Console.WriteLine("\tMetadata:");
                foreach (var keyval in entity.Metadata)
                {
                    Console.WriteLine($"\t\t{keyval.Key}: {keyval.Value}");
                }
            }
        }
        // [END analyze_entities_from_file]
        // [END analyze_entities_from_string]

        // [START analyze_sentiment_from_file]
        private static void AnalyzeSentimentFromFile(string gcsUri)
        {
            var client = LanguageServiceClient.Create();
            var response = client.AnalyzeSentiment(new Document()
            {
                GcsContentUri = gcsUri,
                Type = Document.Types.Type.PlainText
            });
            WriteSentiment(response.DocumentSentiment, response.Sentences);
        }
        // [END analyze_sentiment_from_file]

        // [START analyze_sentiment_from_string]
        private static void AnalyzeSentimentFromText(string text)
        {
            var client = LanguageServiceClient.Create();
            var response = client.AnalyzeSentiment(new Document()
            {
                Content = text,
                Type = Document.Types.Type.PlainText
            });
            WriteSentiment(response.DocumentSentiment, response.Sentences);
        }

        // [START analyze_sentiment_from_file]
        private static void WriteSentiment(Sentiment sentiment,
            RepeatedField<Sentence> sentences)
        {
            Console.WriteLine("Overall document sentiment:");
            Console.WriteLine($"\tScore: {sentiment.Score}");
            Console.WriteLine($"\tMagnitude: {sentiment.Magnitude}");
            Console.WriteLine("Sentence level sentiment:");
            foreach (var sentence in sentences)
            {
                Console.WriteLine($"\t{sentence.Text.Content}: "
                    + $"({sentence.Sentiment.Score})");
            }
        }
        // [END analyze_sentiment_from_file]
        // [END analyze_sentiment_from_string]

        // [START analyze_syntax_from_file]
        private static void AnalyzeSyntaxFromFile(string gcsUri)
        {
            var client = LanguageServiceClient.Create();
            var response = client.AnnotateText(new Document()
            {
                GcsContentUri = gcsUri,
                Type = Document.Types.Type.PlainText
            },
            new Features() { ExtractSyntax = true });
            WriteSentences(response.Sentences, response.Tokens);
        }
        // [END analyze_syntax_from_file]

        // [START analyze_syntax_from_string]
        private static void AnalyzeSyntaxFromText(string text)
        {
            var client = LanguageServiceClient.Create();
            var response = client.AnnotateText(new Document()
            {
                Content = text,
                Type = Document.Types.Type.PlainText
            },
            new Features() { ExtractSyntax = true });
            WriteSentences(response.Sentences, response.Tokens);
        }

        // [START analyze_syntax_from_file]
        private static void WriteSentences(IEnumerable<Sentence> sentences,
            RepeatedField<Token> tokens)
        {
            Console.WriteLine("Sentences:");
            foreach (var sentence in sentences)
            {
                Console.WriteLine($"\t{sentence.Text.BeginOffset}: {sentence.Text.Content}");
            }
            Console.WriteLine("Tokens:");
            foreach (var token in tokens)
            {
                Console.WriteLine($"\t{token.PartOfSpeech.Tag} "
                    + $"{token.Text.Content}");
            }
        }
        // [END analyze_syntax_from_file]
        // [END analyze_syntax_from_string]

        // [START analyze_entity_sentiment_from_file]
        private static void AnalyzeEntitySentimentFromFile(string gcsUri)
        {
            var client = LanguageServiceClient.Create();
            var response = client.AnalyzeEntitySentiment(new Document()
            {
                GcsContentUri = gcsUri,
                Type = Document.Types.Type.PlainText
            });
            WriteEntitySentiment(response.Entities);
        }
        // [END analyze_entity_sentiment_from_file]

        // [START analyze_entity_sentiment_from_string]
        private static void AnalyzeEntitySentimentFromText(string text)
        {
            var client = LanguageServiceClient.Create();
            var response = client.AnalyzeEntitySentiment(new Document()
            {
                Content = text,
                Type = Document.Types.Type.PlainText
            });
            WriteEntitySentiment(response.Entities);
        }

        // [START analyze_entity_sentiment_from_file]
        private static void WriteEntitySentiment(IEnumerable<Entity> entities)
        {
            Console.WriteLine("Entity Sentiment:");
            foreach (var entity in entities)
            {
                Console.WriteLine($"\t{entity.Name} "
                    + $"({(int)(entity.Salience * 100)}%)");
                Console.WriteLine($"\t\tScore: {entity.Sentiment.Score}");
                Console.WriteLine($"\t\tMagnitude { entity.Sentiment.Magnitude}");
            }
        }
        // [END analyze_entity_sentiment_from_file]
        // [END analyze_entity_sentiment_from_string]

        // [START language_classify_file]
        private static void ClassifyTextFromFile(string gcsUri)
        {
            var client = LanguageServiceClient.Create();
            var response = client.ClassifyText(new Document()
            {
                GcsContentUri = gcsUri,
                Type = Document.Types.Type.PlainText
            });
            WriteCategories(response.Categories);
        }
        // [END language_classify_file]

        // [START language_classify_string]
        private static void ClassifyTextFromText(string text)
        {
            var client = LanguageServiceClient.Create();
            var response = client.ClassifyText(new Document()
            {
                Content = text,
                Type = Document.Types.Type.PlainText
            });
            WriteCategories(response.Categories);
        }

        // [START language_classify_file]
        private static void WriteCategories(IEnumerable<ClassificationCategory> categories)
        {
            Console.WriteLine("Categories:");
            foreach (var category in categories)
            {
                Console.WriteLine($"\tCategory: {category.Name}");
                Console.WriteLine($"\t\tConfidence: {category.Confidence}");
            }
        }
        // [END language_classify_string]
        // [END language_classify_file]

        private static void AnalyzeEverything(string text)
        {
            var client = LanguageServiceClient.Create();
            var response = client.AnnotateText(new Document()
            {
                Content = text,
                Type = Document.Types.Type.PlainText
            },
            new Features()
            {
                ExtractSyntax = true,
                ExtractDocumentSentiment = true,
                ExtractEntities = true,
                ExtractEntitySentiment = true,
                ClassifyText = true,
            });
            Console.WriteLine($"Language: {response.Language}");
            WriteSentiment(response.DocumentSentiment, response.Sentences);
            WriteSentences(response.Sentences, response.Tokens);
            WriteEntities(response.Entities);
            WriteEntitySentiment(response.Entities);
            WriteCategories(response.Categories);
        }

        public static void Main(string[] args)
        {

            Console.WriteLine("Enter the type of command: ");
            string command = Console.ReadLine();
            //Console.WriteLine("Enter the text: ");
            string text = "Later, it was said the man came from the north, from Ropers Gate. He came on foot, leading his laden horse by the bridle. It was late afternoon and the ropers’, saddlers’ and tanners’ stalls were already closed, the street empty. It was hot but the man had a black coat thrown over his shoulders. He drew attention to himself. He stopped in front of the tavern, called  “Old Narakort Inn”, stood there for a moment, listened to the hubbub of voices. As usual, at this hour, it was full of people. The stranger did not enter the “Old Narakort Inn”. He pulled his horse further down the street to another tavern, a smaller one, called “The Fox”. Not enjoying the best of reputations, it was almost empty.";
            string gcsUri = null;

            /*
            if (args.Length < 2)
            {
                Console.Write(Usage);
                Console.ReadKey();
                return;
            }
            */
            
            //string command = args[0].ToLower();
            //string text = string.Join(" ",
              //  new ArraySegment<string>(args, 1, args.Length - 1));
            //string gcsUri = args[1].ToLower().StartsWith("gs://") ? args[1] : null;

            switch (command)
            {
                case "entities":
                    if (null == gcsUri)
                        AnalyzeEntitiesFromText(text);
                    else
                        AnalyzeEntitiesFromFile(gcsUri);
                    Console.ReadLine();
                    break;

                case "syntax":
                    if (null == gcsUri)
                        AnalyzeSyntaxFromText(text);
                    else
                        AnalyzeSyntaxFromFile(gcsUri);
                    break;

                case "sentiment":
                    if (null == gcsUri)
                        AnalyzeSentimentFromText(text);
                    else
                        AnalyzeSentimentFromFile(gcsUri);
                    break;

                case "entity-sentiment":
                    if (null == gcsUri)
                        AnalyzeEntitySentimentFromText(text);
                    else
                        AnalyzeEntitySentimentFromFile(gcsUri);
                    break;

                case "classify-text":
                    if (null == gcsUri)
                        ClassifyTextFromText(text);
                    else
                        ClassifyTextFromFile(gcsUri);
                    break;

                case "everything":
                    AnalyzeEverything(text);
                    break;

                default:
                    Console.Write(Usage);
                    Console.ReadKey();
                    return;
            }
        }

    }
}
