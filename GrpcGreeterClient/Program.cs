using FileGrpcGreeterService;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Library.GrpcGreeterService;
using LingYing.GrpcService;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GrpcGreeterClient {
    class Program {
        static async Task Main(string[] _) {
            Console.WriteLine("选择以下命令执行:\r\n1:执行文件Grpc服务\r\n2:执行引用库Grpc服务\r\n3.执行付款服务");
            switch (Console.ReadKey().Key) {
                case ConsoleKey.D1:
                    await ExecuteFileGrpcService();
                    break;
                case ConsoleKey.D2:
                    await ExecuteLibraryGrpcService();
                    break;
                case ConsoleKey.D3:
                    await ExecutePaymentService();
                    break;
                default:
                    Console.WriteLine($"错误的按键");
                    break;
            }
            Console.WriteLine("press any key to exist...");
            Console.Read();
        }

        private static async Task ExecuteFileGrpcService() {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting:" + reply.Message);
        }

        private static async Task ExecuteLibraryGrpcService() {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new LibraryGreeterService.LibraryGreeterServiceClient(channel);
            var reply = await client.SayHelloAsync(new LibraryHelloRequest { Name = "Library Greeter Client" });
            Console.WriteLine($"Library Greeting Reply:{reply.Message}");
        }

        private static async Task ExecutePaymentService() {
            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions {
                HttpHandler = new GrpcWebHandler(new HttpClientHandler())
            });
            var client = new PaymentService.PaymentServiceClient(channel);
            var request = new PaymentRequest {
                PaymentAmount = 300.45F,
                Remark = "张春花等用户的有线电视费"
            };
            request.Payer.AddRange(new[] { "张春花", "李大力" });
            var reply = await client.PaymentAsync(request);
            Console.WriteLine($"Library Greeting Reply:{reply.Message}");
        }
    }
}
