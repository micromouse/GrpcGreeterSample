using Grpc.Core;
using LingYing.GrpcService;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GrpcGreeterService.Services {
    /// <summary>
    /// 付款服务
    /// </summary>
    public class PaymentService : LingYing.GrpcService.PaymentService.PaymentServiceBase {
        private readonly ILogger<PaymentService> _logger;

        /// <summary>
        /// 初始化付款服务
        /// </summary>
        /// <param name="logger">日志器</param>
        public PaymentService(ILogger<PaymentService> logger) {
            _logger = logger;
        }

        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="request">付款请求</param>
        /// <param name="context">服务端调用上下文</param>
        /// <returns>付款响应</returns>
        public override Task<PaymentResponse> Payment(PaymentRequest request, ServerCallContext context) {
            _logger.LogWarning("收到用户付款请求:{@PaymentRequest}", request);
            if (request.PaymentAmount < 200) {
                throw new InvalidOperationException("付款金额不能低于200元");
            }

            return Task.FromResult(new PaymentResponse {
                Status = 1,
                Message = $"已收到{request.Payer}付款金额:{request.PaymentAmount},用户付款说明:{request.Remark}"
            });
        }
    }
}
