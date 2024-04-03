using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace Transcript
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var speechConfig = SpeechConfig.FromSubscription("35add9acfb074df6bd549bd0c90ee325", "eastus");

            using (var recognizer = new SpeechRecognizer(speechConfig))
            {
                Console.WriteLine("Say something...");
                var result = await recognizer.RecognizeOnceAsync();
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    Console.WriteLine($"Recognized: {result.Text}");
                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine("No speech could be recognized.");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                    }
                }
            }
        }
    }
}
