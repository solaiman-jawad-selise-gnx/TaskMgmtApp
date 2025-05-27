using System.Net;

namespace Presentation.Middleware.ExceptionDTO;

public record ExceptionResponse(HttpStatusCode StatusCode, string Description);