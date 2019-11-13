
namespace SolToBoogieTest
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class CorralConfiguration
    {
        [JsonProperty]
        public readonly string TranslatorOptions;

        [JsonProperty]
        public readonly int RecursionBound;

        [JsonProperty]
        public readonly int K;

        [JsonProperty]
        public readonly string Main;

        [JsonProperty]
        public readonly string ExpectedResult;
    }
}
