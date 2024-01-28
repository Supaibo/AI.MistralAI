﻿using TRS.AI.MistralAI.ObjectModels.RequestModels;
using TRS.AI.MistralAI.ObjectModels.ResponseModels;

namespace TRS.AI.MistralAI.Interfaces;

/// <summary>
///     Creates an embedding vector representing the input text.
/// </summary>
public interface IEmbeddingService
{
    /// <summary>
    ///     Creates a new embedding for the provided input and parameters.
    /// </summary>
    /// <param name="createEmbeddingModel"></param>
    /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
    /// <returns></returns>
    Task<EmbeddingCreateResponse> CreateEmbedding(EmbeddingCreateRequest createEmbeddingModel, CancellationToken cancellationToken = default);
}
