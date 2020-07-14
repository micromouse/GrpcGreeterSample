using Grpc.Core;
using Library.GrpcGreeterService;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Library.GrpcGreeterService.LibraryGreeter;

namespace GrpcGreeterService.Services {
    /// <summary>
    /// 引用类库Greeter服务
    /// </summary>
    public class LibraryGreeterService : LibraryGreeterBase {
        private readonly ILogger<LibraryGreeterService> _logger;

        /// <summary>
        /// 初始化引用类库Greeter服务
        /// </summary>
        public LibraryGreeterService(ILogger<LibraryGreeterService> logger) {
            _logger = logger;
        }

        /// <summary>
        /// 打个招呼
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="context">服务调用上下文</param>
        /// <returns>响应</returns>
        public override Task<LibraryHelloReply> SayHello(LibraryHelloRequest request, ServerCallContext context) {
            _logger.LogWarning("正在服务器端执行SayHello:{@Request}", request);
            return Task.FromResult(new LibraryHelloReply {
                Message = $"Hello {request.Name},现在时间是:{DateTime.Now:yyyy-MM-dd HH:mm:sss:ffff}"
            });
        }
    }
}
