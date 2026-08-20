using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace NotesApi.Tests;

public class NotesApiIntegrationTests : IClassFixture<NotesApiFactory>
{
    private readonly NotesApiFactory _factory;

    public NotesApiIntegrationTests(NotesApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetNotes_WithoutToken_ReturnsUnauthorized()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/notes");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetNotes_WithToken_ReturnsOk()
    {
        var client = _factory.CreateClient();

        var login = await client.PostAsJsonAsync(
            "/auth/login",
            new { username = "testuser", password = "TestPassword1!" });
        var loginBody = await login.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.NotNull(loginBody?.Token);

        var request = new HttpRequestMessage(HttpMethod.Get, "/notes");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginBody!.Token);

        var response = await client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Request_IncludesCorrelationIdHeader()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/");

        Assert.True(response.Headers.Contains("X-Correlation-ID"));
    }

    private sealed class LoginResponse
    {
        public string? Token { get; set; }
    }
}