namespace Assignment.Client.HttpClientWeb.V2;

public class HttpClientDelegatingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpClientDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.Headers.Add("x-requestid", Guid.NewGuid().ToString());
        var authorization = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
        if (!string.IsNullOrEmpty(authorization))
            request.Headers.Add("token", authorization.ToString());

        return await base.SendAsync(request, cancellationToken);
    }
}
