// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace OpenAI.Models
{
    /// <summary>
    /// Represents a language model.<br/>
    /// <see href="https://platform.openai.com/docs/models/model-endpoint-compatability"/>
    /// </summary>
    [Preserve]
    public sealed class Model
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Model id.</param>
        /// <param name="ownedBy">Optional, owned by id.</param>
        [Preserve]
        public Model(string id, string ownedBy = null)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Missing the id of the specified model.");
            }

            Id = id;
            OwnedBy = ownedBy;
        }

        [Preserve]
        [JsonConstructor]
        public Model(
            [JsonProperty("id")] string id,
            [JsonProperty("object")] string @object,
            [JsonProperty("created")] int createdAtUnixTimeSeconds,
            [JsonProperty("owned_by")] string ownedBy,
            [JsonProperty("permission")] IReadOnlyList<Permission> permissions,
            [JsonProperty("root")] string root,
            [JsonProperty("parent")] string parent)
            : this(id)
        {
            Object = @object;
            OwnedBy = ownedBy;
            CreatedAtUnixTimeSeconds = createdAtUnixTimeSeconds;
            Permissions = permissions;
            Root = root;
            Parent = parent;
        }

        /// <summary>
        /// Allows a model to be implicitly cast to the string of its id.
        /// </summary>
        /// <param name="model">The <see cref="Model"/> to cast to a string.</param>
        public static implicit operator string(Model model) => model.Id;

        /// <summary>
        /// Allows a string to be implicitly cast as a <see cref="Model"/>
        /// </summary>
        public static implicit operator Model(string name) => new(name);

        /// <inheritdoc />
        public override string ToString() => Id;

        [Preserve]
        [JsonProperty("id")]
        public string Id { get; }

        [Preserve]
        [JsonProperty("object")]
        public string Object { get; }

        [Preserve]
        [JsonProperty("created")]
        public int CreatedAtUnixTimeSeconds { get; }

        [Preserve]
        [JsonIgnore]
        public DateTime CreatedAt => DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTimeSeconds).DateTime;

        [Preserve]
        [JsonProperty("owned_by")]
        public string OwnedBy { get; private set; }

        [Preserve]
        [JsonProperty("permission")]
        public IReadOnlyList<Permission> Permissions { get; }

        [Preserve]
        [JsonProperty("root")]
        public string Root { get; }

        [Preserve]
        [JsonProperty("parent")]
        public string Parent { get; }

        /// <summary>
        /// More capable than any GPT-3.5 model, able to do more complex tasks, and optimized for chat.
        /// Will be updated with our latest model iteration.
        /// </summary>
        public static Model GPT4 { get; } = new("gpt-4", "openai");
        public static Model GPT4_Turbo { get; } = new("gpt-4-turbo", "openai");

        /// <summary>
        /// Same capabilities as the base gpt-4 mode but with 4x the context length.
        /// Will be updated with our latest model iteration.  Tokens are 2x the price of gpt-4.
        /// </summary>
        public static Model GPT4_32K { get; } = new("gpt-4-32k", "openai");

        /// <summary>
        /// Because gpt-3.5-turbo performs at a similar capability to text-davinci-003 but at 10%
        /// the price per token, we recommend gpt-3.5-turbo for most use cases.
        /// </summary>
        public static Model GPT3_5_Turbo { get; } = new("gpt-3.5-turbo", "openai");

        /// <summary>
        /// Same capabilities as the base gpt-3.5-turbo mode but with 4x the context length.
        /// Tokens are 2x the price of gpt-3.5-turbo. Will be updated with our latest model iteration.
        /// </summary>
        public static Model GPT3_5_Turbo_16K { get; } = new("gpt-3.5-turbo-16k", "openai");

        /// <summary>
        /// The most powerful, largest engine available, although the speed is quite slow.<para/>
        /// Good at: Complex intent, cause and effect, summarization for audience
        /// </summary>
        public static Model Davinci { get; } = new("text-davinci-003", "openai");

        /// <summary>
        /// For edit requests.
        /// </summary>
        public static Model DavinciEdit { get; } = new("text-davinci-edit-001", "openai");

        /// <summary>
        /// The 2nd most powerful engine, a bit faster than <see cref="Davinci"/>, and a bit faster.<para/>
        /// Good at: Language translation, complex classification, text sentiment, summarization.
        /// </summary>
        public static Model Curie { get; } = new("text-curie-001", "openai");

        /// <summary>
        /// The 2nd fastest engine, a bit more powerful than <see cref="Ada"/>, and a bit slower.<para/>
        /// Good at: Moderate classification, semantic search classification
        /// </summary>
        public static Model Babbage { get; } = new("text-babbage-001", "openai");

        /// <summary>
        /// The smallest, fastest engine available, although the quality of results may be poor.<para/>
        /// Good at: Parsing text, simple classification, address correction, keywords
        /// </summary>
        public static Model Ada { get; } = new("text-ada-001", "openai");

        /// <summary>
        /// The default model for <see cref="Embeddings.EmbeddingsEndpoint"/>.
        /// </summary>
        public static Model Embedding_Ada_002 { get; } = new("text-embedding-ada-002", "openai");

        /// <summary>
        /// The default model for <see cref="Audio.AudioEndpoint"/>.
        /// </summary>
        public static Model Whisper1 { get; } = new("whisper-1", "openai");

        /// <summary>
        /// The default model for <see cref="Moderations.ModerationsEndpoint"/>.
        /// </summary>
        public static Model Moderation_Latest { get; } = new("text-moderation-latest", "openai");

        /// <summary>
        /// The default model for <see cref="Audio.SpeechRequest"/>s.
        /// </summary>
        public static Model TTS_1 { get; } = new("tts-1", "openai");

        public static Model TTS_1HD { get; } = new("tts-1-hd", "openai");

        /// <summary>
        /// The default model for <see cref="Images.ImagesEndpoint"/>.
        /// </summary>
        public static Model DallE_2 { get; } = new("dall-e-2", "openai");

        public static Model DallE_3 { get; } = new("dall-e-3", "openai");
    }
}
