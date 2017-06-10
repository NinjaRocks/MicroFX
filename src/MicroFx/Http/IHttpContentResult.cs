namespace MicroFx.Http
{
    public interface IHttpContentResult<out T> : IHttpStatusResult
    {
       T Content { get;  }
    }
}