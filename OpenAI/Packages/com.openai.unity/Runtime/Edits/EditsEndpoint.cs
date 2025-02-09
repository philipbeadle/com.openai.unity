// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using OpenAI.Extensions;
using OpenAI.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Utilities.WebRequestRest;

namespace OpenAI.Edits
{
    /// <summary>
    /// Given a prompt and an instruction, the model will return an edited version of the prompt.
    /// <see href="https://platform.openai.com/docs/api-reference/edits"/>
    /// </summary>
    [Obsolete("Deprecated")]
    public sealed class EditsEndpoint : OpenAIBaseEndpoint
    {
        internal EditsEndpoint(OpenAIClient client) : base(client) { }

        /// <inheritdoc />
        protected override string Root => "edits";

        /// <summary>
        /// Creates a new edit for the provided input, instruction, and parameters
        /// </summary>
        /// <param name="input">The input text to use as a starting point for the edit.</param>
        /// <param name="instruction">The instruction that tells the model how to edit the prompt.</param>
        /// <param name="editCount">How many edits to generate for the input and instruction.</param>
        /// <param name="temperature">
        /// What sampling temperature to use. Higher values means the model will take more risks.
        /// Try 0.9 for more creative applications, and 0 (argmax sampling) for ones with a well-defined answer.
        /// We generally recommend altering this or top_p but not both.
        /// </param>
        /// <param name="topP">
        /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the
        /// results of the tokens with top_p probability mass.
        /// So 0.1 means only the tokens comprising the top 10% probability mass are considered.
        /// We generally recommend altering this or temperature but not both.
        /// </param>
        /// <param name="model">ID of the model to use. Defaults to text-davinci-edit-001.</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        /// <returns>The top edit result choice.</returns>
        public async Task<string> CreateEditAsync(
            string input,
            string instruction,
            int? editCount,
            double? temperature,
            double? topP,
            Model model = null,
            CancellationToken cancellationToken = default)
        {
            var request = new EditRequest(input, instruction, editCount, temperature, topP, model);
            var result = await CreateEditAsync(request, cancellationToken);
            return result.ToString();
        }

        /// <summary>
        /// Creates a new edit for the provided input, instruction, and parameters.
        /// </summary>
        /// <param name="request"><see cref="EditRequest"/>.</param>
        /// <param name="cancellationToken">Optional, <see cref="CancellationToken"/>.</param>
        /// <returns><see cref="EditResponse"/>.</returns>
        public async Task<EditResponse> CreateEditAsync(EditRequest request, CancellationToken cancellationToken = default)
        {
            var payload = JsonConvert.SerializeObject(request, OpenAIClient.JsonSerializationOptions);
            var response = await Rest.PostAsync(GetUrl(), payload, new RestParameters(client.DefaultRequestHeaders), cancellationToken);
            response.Validate(EnableDebug);
            return response.Deserialize<EditResponse>(client);
        }
    }
}
