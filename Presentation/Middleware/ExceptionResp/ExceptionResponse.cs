using System.Net;

namespace Presentation.Middleware.ExceptionResp;

public record ExceptionResponse(HttpStatusCode StatusCode, string Description);