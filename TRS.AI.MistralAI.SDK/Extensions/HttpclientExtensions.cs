using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using TRS.AI.MistralAI.ObjectModels.ResponseModels;

namespace TRS.AI.MistralAI.Extensions;

public static class HttpclientExtensions
{
    public static async Task<TResponse> PostAndReadAsAsync<TResponse>(this HttpClient client, string uri, object? requestModel, CancellationToken cancellationToken = default) where TResponse : BaseResponse, new()
    {
        var response = await client.PostAsJsonAsync(uri, requestModel, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        }, cancellationToken);
        return await HandleResponseContent<TResponse>(response, cancellationToken);
    }

    public static string? GetHeaderValue(this HttpResponseHeaders headers, string headerName)
    {
        if (string.IsNullOrEmpty(headerName))
        {
            return null;
        }

        return headers.Contains(headerName) ? headers.GetValues(headerName).FirstOrDefault() : null;
    }

    public static HttpResponseMessage PostAsStreamAsync(this HttpClient client, string uri, object requestModel, CancellationToken cancellationToken = default)
    {
        var settings = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };

        var content = JsonContent.Create(requestModel, null, settings);

        using var request = CreatePostEventStreamRequest(uri, content);
        try
        {
            return client.Send(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        }
        catch (PlatformNotSupportedException)
        {
            using var newRequest = CreatePostEventStreamRequest(uri, content);

            return SendRequestPreNet6(client, newRequest, cancellationToken);
        }
    }

    private static HttpResponseMessage SendRequestPreNet6(HttpClient client, HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var responseTask = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        var response = responseTask.GetAwaiter().GetResult();
        return response;
    }

    private static HttpRequestMessage CreatePostEventStreamRequest(string uri, HttpContent content)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Headers.Accept.Add(new("text/event-stream"));
        request.Content = content;

        return request;
    }

    private static async Task<TResponse> HandleResponseContent<TResponse>(this HttpResponseMessage response, CancellationToken cancellationToken) where TResponse : BaseResponse, new()
    {
        TResponse result;

        if (!response.Content.Headers.ContentType?.MediaType?.Equals("application/json", StringComparison.OrdinalIgnoreCase) ?? true)
        {
            result = new()
            {
                Error = new()
                {
                    MessageObject = await response.Content.ReadAsStringAsync(cancellationToken)
                }
            };
        }
        else
        {
            result = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken) ??
                     throw new InvalidOperationException();
        }

        result.HttpStatusCode = response.StatusCode;
        result.HeaderValues = new()
        {
            Date = response.Headers.Date,
            Connection = response.Headers.Connection?.ToString(),
            AccessControlAllowOrigin = response.Headers.GetHeaderValue("access-control-allow-origin"),
            CFCacheStatus = response.Headers.GetHeaderValue("cf-cache-status"),
            Server = response.Headers.Server?.ToString(),
            CF_RAY = response.Headers.GetHeaderValue("cf-ray"),
            AltSvc = response.Headers.GetHeaderValue("alt-svc"),
            All = response.Headers.ToDictionary(x => x.Key, x => x.Value.AsEnumerable()),
            RateLimits = new()
            {
                LimitMonths = response.Headers.GetHeaderValue("x-ratelimitbysize-limit-month"),
                LimitMinute = response.Headers.GetHeaderValue("x-ratelimitbysize-limit-minute"),
                RemainingMinute = response.Headers.GetHeaderValue("x-ratelimitbysize-remaining-minute"),
                Reset = response.Headers.GetHeaderValue("ratelimitbysize-reset"),
                RemainingMonths = response.Headers.GetHeaderValue("x-ratelimitbysize-remaining-month"),
                QueryCost = response.Headers.GetHeaderValue("ratelimitbysize-query-cost"),
                Limit = response.Headers.GetHeaderValue("ratelimitbysize-limit"),
                Remaining = response.Headers.GetHeaderValue("ratelimitbysize-remaining"),
            },
            Kong = new()
            {
                Latency = response.Headers.GetHeaderValue("x-kong-proxy-latency"),
                RequestId = response.Headers.GetHeaderValue("x-kong-request-id"),
                UpstreamLatency = response.Headers.GetHeaderValue("x-kong-upstream-latency")
            }
        };
        return result;
    }
}
