using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;


namespace Tutorial37
{
    // This tutorial exposes how functional programming, particularly caching results from pure functions, aids memoization, as they always return the same output for same input.
    class Program
    {
        static void Main(string[] args)
        {
            // This is our input strings
            var phrase1 = "1. When you need answers for programming";
            var phrase2 = "2. with C# 5.0, this proactical and tightly";
            var phrase3 = "3. Focused book tells you exactly what you need";
            var phrase4 = "4. to know-without long introductions or bloated samples.";
            var phrase5 = "5. Easy to browse, it's ideal as a quick";
            var phrase6 = "6. reference or a guide to get you";
            var phrase7 = "7. rapidly up to speed if you already know Java, C++,";
            var phrase8 = "8. or an earlier version of C#";
            var phrase9 = "9. Dynamic Binding and C# 5.0's new";
            var phrase10 = "10.asynchonous functions";

            // collect them all in an array
            var phrases = new string[] { phrase1, phrase2, phrase3, phrase4, phrase5, phrase6, phrase7, phrase8, phrase9, phrase10 };

            var encryptedResultsCache = new Dictionary<string, Option<string>>();
            var decryptedResultsCache = new Dictionary<string, Option<string>>();
                        
            // Encrypt all the input phrases, and decrypt them

            for(int i = 1; i < phrases.Length-1; i++)
            {
                var phrase = phrases[i];
                              
                var maybeEncrypted = SimpleEncrypt(phrase);

                // This transformation only occurs if the encrypted result is a Some(encryptedResult) and not a None i.e invalid result
                SimpleEncrypt(phrase).Iter(encryptedString =>
                {                
                    encryptedResultsCache.TryAdd(phrase, maybeEncrypted);                          
                    decryptedResultsCache.TryAdd(encryptedString, SimpleDecrypt(encryptedString));                        
                });
            }

            
            // Now because we've cached decrypted results for phrases, when we see that encrypted phrase we can use the cached decrypted result for that encrypted phrase to get 
            // without having to run the SimpleDecrypt() function again. This is the same with all caching mechanism, which make the obvious seem transparent, however with a pure function, you know for certain that 
            // there is no chance that our cached decrypted result could be different from running a SimpleDecrypt() on the encrypted string we have - so we have double certainty that we
            // dont have to run the SimpleDecrypt() function. If SimpleEncrypt() or SimpleDecrypt() could sometimes return different outputs for the same input, then we'd have to cal SimpleDecrypt()
            // to return what the decrypted result is that/this time.

            // Note using Monads, Select, Bind() within your functions you're making it unlikely that you functions will ever throw exceptions becasue you're catering for both the expected and unexpected data 
            // by virtue of using Monads (which ensure that you need to ie. they contain both failure and success logic such as Some/None or Either left or right emedded into themselves so you can and indeed have to cater for them)
            // In catering for them by extracting their values using Match() or transforming via Select()/Bind()/Map(). 

            // Now use cache to decrypt known inputs against outputs produced by pure function SimpleEncrypt():
            
            var randomCacheEntry = encryptedResultsCache.ElementAt(new Random().Next(encryptedResultsCache.Count));
            var getDecrypted = from encrypted in randomCacheEntry.Value                               
                               from decrypted in decryptedResultsCache[encrypted] // No need to call SimpleDecrypt(encrypted)
                               select decrypted;                    
            Console.WriteLine($"The encrypted form for '{randomCacheEntry.Key}' is '{getDecrypted}' is");                     
        }   
        
        // Pure function, nothing that will jepordaise the consistency of same returned value given the same input.
        static Option<string> SimpleEncrypt(string phrase)
        {
            var result = new Option<string>();

            if(string.IsNullOrWhiteSpace(phrase))
                return Option<string>.None;

            char[] characters = phrase.ToArray();
            for(int i = 0;i<characters.Length;i++)
            {
                if(characters[i] == 'r')
                    return Option<string>.None;
                characters[i]++;
            }
            
            result = new string(characters);
            return result;
        }

        static Option<string> SimpleDecrypt(Option<string> phrase)
        {
            return phrase.Bind( phr => 
            {
                var result = new Option<string>();
                if(string.IsNullOrWhiteSpace(phr))
                return Option<string>.None;

                char[] characters = phr.ToArray();
                for(int i = 0;i<characters.Length;i++)
                {
                    characters[i]--;
                }
            
                result = new string(characters);
                return result;
            });
        }
    }      
}
