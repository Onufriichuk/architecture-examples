using Exercise;

class RequestBuilder
{
    private HttpRequest request = new();

    public RequestBuilder Url(string url)
    {
        request.Url = url;
        return this;
    }

    public RequestBuilder Method(string method)
    {
        request.Method = method;
        return this;
    }

    public RequestBuilder Header(string key, string value)
    {
        request.Headers[key] = value;
        return this;
    }

    public RequestBuilder Query(string key, string value)
    {
        request.QueryParameters[key] = value;
        return this;
    }

    public RequestBuilder Body(string body)
    {
        request.Body = body;
        return this;
    }

    public HttpRequest Build()
    {
        if (!Uri.TryCreate(request.Url, UriKind.Absolute, out var url) ||
            (url.Scheme != "http" && url.Scheme != "https"))
            throw new Exception("Invalid URL");

        if (string.IsNullOrWhiteSpace(request.Method))
            throw new Exception("Invalid method");

        var result = request;
        request = new HttpRequest
        {
            Url = result.Url,
            Method = result.Method,
            Body = result.Body,
            Headers = new(result.Headers),
            QueryParameters = new(result.QueryParameters)
        };
        return result;
    }
}