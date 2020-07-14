using Grpc.Net.Client;
using FileGrpcGreeterService;
using Library.GrpcGreeterService;
using System;
using System.Threading.Tasks;

namespace GrpcGreeterClient {
    class Program {
        static async Task Main(string[] args) {
            Console.WriteLine("选择以下命令执行:\r\n1:执行文件Grpc服务\r\n2:执行引用库Grpc服务");
            switch (Console.ReadKey().Key) {
                case ConsoleKey.D1:
                    //await ExecuteFileGrpcService();
                    break;
                case ConsoleKey.D2:
                    await ExecuteLibraryGrpcService();
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
            var channel = GrpcChannel.ForAddress("http://localhost:5001");
            var client = new LibraryGreeterService.LibraryGreeterServiceClient(channel);
            var reply = await client.SayHelloAsync(new LibraryHelloRequest { Name = "Library Greeter Client" });
            Console.WriteLine($"Library Greeting Reply:{reply.Message}");
        }
    }
}
