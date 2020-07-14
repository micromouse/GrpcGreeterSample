using FileGrpcGreeterService;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GrpcGreeterService {
    public class GreeterService : Greeter.GreeterBase {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger) {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context) {
            _logger.LogWarning("����ִ��SayHello,{@request}", request);
            return Task.FromResult(new HelloReply {
                Message = "�����ڷ������˵Ļ�Ӧ:" + request.Name
            });
        }
    }
}
