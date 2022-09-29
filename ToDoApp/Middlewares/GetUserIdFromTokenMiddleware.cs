using System.Net.Http.Headers;

namespace ToDoApp.Middlewares
{
    public class GetUserIdFromTokenMiddleware
    {
        private readonly RequestDelegate _next;
        public static readonly object Key = new();

        public GetUserIdFromTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpcontext)
        {
            var url = "https://localhost:7190/api/UserRequest";
            using var client = new HttpClient();
            var msg = new HttpRequestMessage(HttpMethod.Get, url);

            string token = httpcontext.Request.Headers.Authorization.ToString().Split(" ")[1];
            Console.WriteLine(token);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var res = await client.SendAsync(msg);
            var content = await res.Content.ReadAsStringAsync();

            httpcontext.Items["UserId"] = content;

            Console.WriteLine(content);

            await _next.Invoke(httpcontext);
        }
    }
}
