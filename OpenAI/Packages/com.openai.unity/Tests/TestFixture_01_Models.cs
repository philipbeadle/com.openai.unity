// Licensed under the MIT License. See LICENSE in the project root for license information.

using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenAI.Tests
{
    internal class TestFixture_01_Models : AbstractTestFixture
    {
        [Test]
        public async Task Test_1_GetModels()
        {
            Assert.IsNotNull(OpenAIClient.ModelsEndpoint);
            var results = await OpenAIClient.ModelsEndpoint.GetModelsAsync();
            Assert.IsNotNull(results);
            Assert.NotZero(results.Count);
        }

        [Test]
        public async Task Test_2_RetrieveModelDetails()
        {
            Assert.IsNotNull(OpenAIClient.ModelsEndpoint);
            var models = await OpenAIClient.ModelsEndpoint.GetModelsAsync();
            Assert.IsNotEmpty(models);
            Debug.Log($"Found {models.Count} models!");

            foreach (var model in models.OrderBy(model => model.Id))
            {
                Debug.Log($"{model.Id} | Owner: {model.OwnedBy} | Created: {model.CreatedAt}");

                try
                {
                    var result = await OpenAIClient.ModelsEndpoint.GetModelDetailsAsync(model.Id);
                    Assert.IsNotNull(result);
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"No Model details found for {model.Id}\n{e}");
                }
            }
        }
    }
}
